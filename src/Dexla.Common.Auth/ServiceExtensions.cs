using System.Security.Cryptography;
using System.Text;
using Dexla.Common.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Dexla.Common.Auth;

public static class ServiceExtensions
{
    public static IServiceCollection AddPropelAuth(this IServiceCollection services, IConfiguration configuration)
    {
        AuthSettings settings = UtilityExtensions.GetConfigurationSection<AuthSettings>(configuration);

        RSA rsa = RSA.Create();
        rsa.ImportFromPem(settings.Certificate);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidAlgorithms = new List<string> { "RS256" },
                    ValidIssuer = settings.Issuer,
                    IssuerSigningKey = new RsaSecurityKey(rsa),
                };
            })
            .AddJwtBearer("Onboarding", options =>
            {
                string jwtSecretKey = StandardAccessTokenRefreshTokenTest.JWT_TOKEN_SECRET_KEY;
                byte[] key = Encoding.UTF8.GetBytes(jwtSecretKey);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "dexla.ai",
                    ValidAudience = "dexla.ai",
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // the default for this setting is 5 minutes
                };
            })
            .AddJwtBearer("OnboardingRefresh", options =>
            {
                string jwtSecretKey = StandardAccessTokenRefreshTokenTest.JWT_TOKEN_SECRET_KEY;
                byte[] key = Encoding.UTF8.GetBytes(jwtSecretKey);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "dexla.ai",
                    ValidAudience = "dexla.ai",
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // the default for this setting is 5 minutes
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicy.RequiredAuthentication, policy =>
                policy
                    .RequireAuthenticatedUser()
                    .Requirements.Add(new RequiredAuthentication()));

            options.AddPolicy(AuthPolicy.Onboarding, policy =>
                policy
                    .RequireAuthenticatedUser()
                    .Requirements.Add(new OnboardingAuthentication()));
            
            options.AddPolicy(AuthPolicy.RequiredAuthenticationOrOnboarding, policy =>
            {
                policy
                    .RequireAuthenticatedUser()
                    .Requirements.Add(new RequiredAuthenticationOrOnboardingAuthentication());
            });

            options.FallbackPolicy = options.GetPolicy(AuthPolicy.RequiredAuthentication);
        });

        // Change to Singleton when working
        services.AddScoped<IUserTokenService, UserTokenService>();
        services.AddScoped<IAuthorizationHandler, RequiredAuthenticationHandler>();
        services.AddScoped<IAuthorizationHandler, OnboardingAuthenticationHandler>();
        services.AddScoped<IAuthorizationHandler, RequiredAuthenticationOrOnboardingAuthenticationHandler>();

        return services;
    }
}