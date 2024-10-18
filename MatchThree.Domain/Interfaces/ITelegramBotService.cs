using MatchThree.Domain.Models;

namespace MatchThree.Domain.Interfaces;

public interface ITelegramBotService
{
    Task<string> CreateInvoiceLink(TelegramPayloadEntity payloadEntity);
}