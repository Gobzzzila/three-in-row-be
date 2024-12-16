using System.Reflection;
using System.Text;
using MatchThree.API.Authentication;
using MatchThree.API.Authentication.Interfaces;
using MatchThree.API.Authentication.Policies;
using MatchThree.API.ExceptionHandlers;
using MatchThree.API.Services;
using MatchThree.BL.Extensions;
using MatchThree.Domain.Interfaces;
using MatchThree.Domain.Settings;
using MatchThree.Repository.MSSQL;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
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
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .CreateLogger(); 
            
                builder.Host.UseSerilog(); 
                
                builder.Services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
#if DEBUG
                        var jwtKey = builder.Configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Key)}"];
#else
                        var jwtKey = Environment.GetEnvironmentVariable("jwtKey");
#endif
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Issuer)}"],
                            ValidAudience = builder.Configuration[$"{nameof(JwtSettings)}:{nameof(JwtSettings.Audience)}"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
                        };
                        options.MapInboundClaims = false;

                        options.Events = new JwtBearerEvents
                        {
                            OnChallenge = context =>
                            {
                                if (context.AuthenticateFailure is not SecurityTokenExpiredException)
                                    return Task.CompletedTask;
                                
                                var handler = context.HttpContext.RequestServices.GetRequiredService<IJwtTokenExpiredHandler>(); 
                                handler.HandleAsync(context.HttpContext);
                                context.HandleResponse();
                        
                                return Task.CompletedTask;
                            }
                        };
                    });
                
                builder.Services.AddAuthorization(options =>
                {
                    options.AddPolicy(AuthenticationConstants.UserIdPolicy, policy =>
                    {
                        policy.Requirements.Add(new UserIdRequirement());
                    });
                });
                
#if DEBUG
                var connectionString = builder.Configuration.GetConnectionString(nameof(MatchThreeDbContext));
#else
                var connectionString = Environment.GetEnvironmentVariable("connectionString");
#endif
                builder.Services.AddDbContext<MatchThreeDbContext>(options =>
                    options.UseSqlServer(connectionString,
                        optionsBuilder =>
                        {
                            optionsBuilder.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(10),
                                errorNumbersToAdd: null);
                        }));

                builder.Services.AddHostedService<CalculateLeaderboardService>();
                builder.Services.AddHostedService<TopUpEnergyDrinksService>();
                builder.Services.AddSingleton<ITelegramBotService, TelegramBotService>();
                builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
                builder.Services.AddSingleton<IAuthorizationHandler, UserIdHandler>();
                builder.Services.AddSingleton<IJwtTokenExpiredHandler, JwtTokenExpiredHandler>();
                builder.Services.AddDomainServices();
                
                builder.Services.Configure<JwtSettings>(options => 
                { 
                    builder.Configuration.GetSection(nameof(JwtSettings)).Bind(options); 
#if RELEASE 
                    options.Key = Environment.GetEnvironmentVariable(EnvConstants.JwtKeyEnvName)!; 
#endif 
                });

                builder.Services.Configure<TelegramSettings>(options => 
                { 
                    builder.Configuration.GetSection(nameof(TelegramSettings)).Bind(options); 
#if RELEASE 
                    options.BotToken = Environment.GetEnvironmentVariable(EnvConstants.BotTokenEnvName)!; 
                    options.HelperBotToken = Environment.GetEnvironmentVariable(EnvConstants.HelperBotTokenEnvName)!; 
#endif 
                });
                
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
                    c.EnableAnnotations();
                    
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Enter your JWT token in the format: {token}"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                });

                builder.Services.AddExceptionHandler<ValidationExceptionHandler>()
                    .AddExceptionHandler<NotEnoughBalanceExceptionHandler>()
                    .AddExceptionHandler<MaxLevelReachedExceptionHandler>()
                    .AddExceptionHandler<UpgradeConditionsExceptionHandler>()
                    .AddExceptionHandler<NoDataFoundExceptionHandler>()
                    .AddExceptionHandler<DefaultExceptionHandler>();
                
                builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
                builder.Services.Configure<RequestLocalizationOptions>(options =>
                {
                    string[] supportedCultures = ["en-US", "ru-RU"];
                    options.SetDefaultCulture(supportedCultures[0]);
                    options.AddSupportedCultures(supportedCultures);
                    options.AddSupportedUICultures(supportedCultures);
                });
                
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder =>
                        {
#if DEBUG
                            builder.WithOrigins("https://cryptofe-75961.web.app")
                                .AllowAnyHeader()
                                .AllowAnyMethod();    
#else
                            builder.WithOrigins("https://pingwin-be08f.web.app")
                                .AllowAnyHeader()
                                .AllowAnyMethod();        
#endif
                            builder.WithOrigins("http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });
            }

            void Configure()
            {
                app.UseCors("AllowSpecificOrigin");
                
                var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
                app.UseRequestLocalization(localizationOptions.Value);
                
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider
                        .GetRequiredService<MatchThreeDbContext>();
                    dbContext.Database.Migrate();
                }

#if DEBUG
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        "v1");
                    
                    c.DisplayOperationId();
                    c.DisplayRequestDuration();
                });
#endif

                app.UseAuthentication(); 
                app.UseAuthorization();
                
                app.UseHttpsRedirection();

                app.MapControllers();

                app.UseExceptionHandler(_ => { });

                // app.UseMiddleware<LoggingMiddleware>();
                
                app.Services.GetRequiredService<ITelegramBotService>();
            }
        }


    }
}
