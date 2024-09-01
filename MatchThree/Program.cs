using System.Reflection;
using MatchThree.API.ExceptionHandlers;
using MatchThree.API.Middleware;
using MatchThree.API.Services;
using MatchThree.BL.Extensions;
using MatchThree.Repository.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using static MatchThree.API.SwaggerConfiguration;

namespace MatchThree.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices();
            var app = builder.Build();

            Configure();
            app.Run();

            void ConfigureServices()
            {
                builder.Services.AddDbContext<MatchThreeDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(MatchThreeDbContext))));

                builder.Services.AddHostedService<CalculateLeaderboardService>();
                builder.Services.AddHostedService<TopUpEnergyDrinksService>();
                builder.Services.AddDomainServices();

                var autoMapperProfileAssemblies =
                    new List<Assembly>
                    {
                        typeof(AutoMappingProfile).Assembly,
                        typeof(BL.AutoMappingProfile).Assembly
                    };
                builder.Services.AddAutoMapper(autoMapperProfileAssemblies);

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer(); //Swagger staff 
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "Match three API"
                        });
                    c.OperationFilter<AcceptLanguageHeaderParameter>();
                });

                builder.Services.AddExceptionHandler<NoDataFoundExceptionHandler>()
                    .AddExceptionHandler<NotEnoughBalanceExceptionHandler>()
                    .AddExceptionHandler<MaxLevelReachedExceptionHandler>()
                    .AddExceptionHandler<UpgradeConditionsExceptionHandler>()
                    .AddExceptionHandler<DefaultExceptionHandler>();
                
                builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
                builder.Services.Configure<RequestLocalizationOptions>(options =>
                {
                    string[] supportedCultures = ["en-US", "ru-RU"];
                    options.SetDefaultCulture(supportedCultures[0]);
                    options.AddSupportedCultures(supportedCultures);
                    options.AddSupportedUICultures(supportedCultures);
                });
            }

            void Configure()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider
                        .GetRequiredService<MatchThreeDbContext>();
                    dbContext.Database.Migrate();
                }

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "v1"));

                app.UseHttpsRedirection();
                //app.UseAuthorization();
                app.MapControllers();

                app.UseExceptionHandler(opt => { });

                app.UseMiddleware<LoggingMiddleware>();
                
                var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
                app.UseRequestLocalization(localizationOptions.Value);
            }
        }


    }
}
