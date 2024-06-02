using System.Text;
using EyeC.Application.Common.Interfaces;
using EyeC.Domain.Constants;
using EyeC.Infrastructure.Authentication;
using EyeC.Infrastructure.Data;
using EyeC.Infrastructure.Data.Interceptors;
using EyeC.Infrastructure.Identity;
using EyeC.Infrastructure.IOServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EyeC-Dev");
        var mySqlServerVersion = ServerVersion.AutoDetect(connectionString);
        var jwtSecret = configuration["Jwt:Secret"] ?? "";
        var jwtIssuer = configuration["Jwt:Issuer"] ?? "";


        Guard.Against.Null(connectionString, message: "Connection string 'EyeC.*' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseMySql(connectionString, mySqlServerVersion);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                .AddJwtBearer(jwt =>
                {
                    jwt.MapInboundClaims = false;
                    jwt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
                        RequireSignedTokens = true,
                        ValidIssuer = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    };
                });

        services.AddAuthorizationBuilder();

        services.AddControllers();

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IAuthenticationService>(x => ActivatorUtilities.CreateInstance<AuthenticationService>(x, jwtSecret, jwtIssuer));
        services.AddScoped<IImageService, ImageService>();

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator, Roles.Member)));

        return services;
    }
}
