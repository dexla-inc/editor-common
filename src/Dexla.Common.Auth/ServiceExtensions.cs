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
            .AddJwtBearer("Test", options =>
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
            .AddJwtBearer("TestRefresh", options =>
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
            options.AddPolicy("RequiredAuthentication", policy => 
                policy.RequireAuthenticatedUser()
                    .Requirements.Add(new RequiredAuthentication()));

            options.AddPolicy("Test", policy => 
                policy.Requirements.Add(new TestAuthentication()));

            options.DefaultPolicy = options.GetPolicy("RequiredAuthentication");
        });
        
        services
            .AddScoped<IAuthorizationHandler, RequiredAuthenticationHandler>()
            .AddScoped<IUserTokenService, UserTokenService>();

        return services;
    }

    public static IEndpointConventionBuilder RequirePolicies(this IEndpointConventionBuilder app)
    {
        app
            .RequireAuthorization(p => p.AddRequirements(new RequiredAuthentication()));

        return app;
    }
}