using System.Text.Json;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Interfaces.Energy;
using MatchThree.Domain.Models;
using MatchThree.Domain.Settings;
using MatchThree.Shared.Constants;
using MatchThree.Shared.Enums;
using MatchThree.Shared.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace MatchThree.API.Services;

public class TelegramBotService : ITelegramBotService, IDisposable
{
    private readonly TelegramBotClient _bot;
    
    private readonly IStringLocalizer<Localization> _localizer;
    private readonly IServiceScope _scope;
    private readonly ILogger<TelegramBotService> _logger;
    
    private bool _disposed;
    private readonly ITransactionService _transactionService;
    private readonly IUpdateEnergyService _updateEnergyService;
    private readonly IReadEnergyService _readEnergyService;

    public TelegramBotService(IStringLocalizer<Localization> localizer,
        IServiceProvider serviceProvider,
        IOptions<TelegramSettings> options,
        ILogger<TelegramBotService> logger)
    {
        _bot = new TelegramBotClient(options.Value.BotToken);
        _bot.OnError += OnError;
        _bot.OnMessage += OnMessage;
        _bot.OnUpdate += OnUpdate;
        
        _localizer = localizer;
        _logger = logger;
        _scope = serviceProvider.CreateScope();
        var provider = _scope.ServiceProvider;
        _transactionService = provider.GetRequiredService<ITransactionService>();
        _updateEnergyService = provider.GetRequiredService<IUpdateEnergyService>();
        _readEnergyService = provider.GetRequiredService<IReadEnergyService>();
    }
    
    public async Task<string> CreateInvoiceLink(TelegramPayloadEntity payloadEntity)
    {
        var upgradeInfo = payloadEntity.UpgradeType.GetUpgradeInfo();
        var price = 0;
        if (payloadEntity.UpgradeType == UpgradeTypes.EnergyDrink)
            price = EnergyConstants.EnergyDrinkPrice;
        
        var invoiceLink = await _bot.CreateInvoiceLinkAsync(
            title: _localizer[upgradeInfo!.HeaderTextId],
            description: _localizer[upgradeInfo!.DescriptionTextId],
            payload: JsonSerializer.Serialize(payloadEntity),
            providerToken: null,
            currency: "XTR",
            prices: new List<LabeledPrice>
            {
                new()
                {
                    Amount = price,
                    Label = "Price"
                }
            });
        
        return invoiceLink;
    }
    
    private Task OnError(Exception exception, HandleErrorSource source)
    {
        _logger.LogError($"Telegram error: {exception}");
        
        return Task.CompletedTask;
    }

    private async Task OnMessage(Message msg, UpdateType type)
    {
        if (msg.Text == "/start")
            await _bot.SendTextMessageAsync(msg.Chat, "Welcome to the club buddy!");
        
        if (msg.SuccessfulPayment is not null )
        {
            var payload = JsonSerializer.Deserialize<TelegramPayloadEntity>(msg.SuccessfulPayment.InvoicePayload);
            if (payload is { UpgradeType: UpgradeTypes.EnergyDrink })
            {
                await _updateEnergyService.PurchaseEnergyDrinkAsync(payload.UserId);
                await _transactionService.Commit();
                _transactionService.CleanChangeTracker();
            }

            _logger.LogInformation($"Telegram successful payment: User {msg.Chat} paid for {msg.SuccessfulPayment.InvoicePayload}. " +
                                   $"TelegramPaymentChargeId: {msg.SuccessfulPayment.TelegramPaymentChargeId}");
        }
    }
    
    private async Task OnUpdate(Update update)
    {
        switch (update)
        {
            case { PreCheckoutQuery: { } preCheckoutQuery }:
                await ValidatePreCheckoutQuery(preCheckoutQuery);
                break;
        };
    }

    private async Task ValidatePreCheckoutQuery(PreCheckoutQuery preCheckoutQuery)
    {
        var payload = JsonSerializer.Deserialize<TelegramPayloadEntity>(preCheckoutQuery.InvoicePayload);
        if (payload is { UpgradeType: UpgradeTypes.EnergyDrink, UserId: > 0 })
        {
            var energyEntity = await _readEnergyService.GetByUserIdAsync(payload.UserId);
            _transactionService.CleanChangeTracker();
            if (energyEntity.PurchasableEnergyDrinkAmount > 0)
                await _bot.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id);
        }
        
        await _bot.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id, "Invalid data");
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        
        if (disposing)
            _scope.Dispose();

        _disposed = true;
    }

    ~TelegramBotService()
    {
        Dispose(false);
    }
}