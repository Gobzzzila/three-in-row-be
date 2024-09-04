using System.Reflection;
using System.Text;
using MatchThree.API.Authentication;
using MatchThree.API.Authentication.Policies;
using MatchThree.API.ExceptionHandlers;
using MatchThree.API.Middleware;
using MatchThree.API.Services;
using MatchThree.BL.Extensions;
using MatchThree.Domain.Interfaces;
using MatchThree.Repository.MSSQL;
using MatchThree.Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
                builder.Services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                        };
                        options.MapInboundClaims = false;
                    });
                builder.Services.AddAuthorization(options =>
                {
                    options.AddPolicy(AuthenticationConstants.UserIdPolicy, policy =>
                    {
                        policy.Requirements.Add(new UserIdRequirement());
                    });
                });
                
                builder.Services.AddDbContext<MatchThreeDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(MatchThreeDbContext))));

                builder.Services.AddHostedService<CalculateLeaderboardService>();
                builder.Services.AddHostedService<TopUpEnergyDrinksService>();
                builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
                builder.Services.AddSingleton<IAuthorizationHandler, UserIdHandler>();
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

                app.UseAuthentication(); 
                app.UseAuthorization();
                
                app.UseHttpsRedirection();

                app.MapControllers();

                app.UseExceptionHandler(_ => { });

                app.UseMiddleware<LoggingMiddleware>();
                
                var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
                app.UseRequestLocalization(localizationOptions.Value);
            }
        }


    }
}
