using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyExpensesAPI.Infrastructure;
using MyExpensesAPI.Models;
using System;
using System.Text;

namespace MyExpensesAPI.Configurations
{
    public static class JwtConfiguration
    {
        private static void JwtAuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var jwtTokenConfig = configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                // для аутентификации требуется HTTPS
                config.RequireHttpsMetadata = true;
                // сохраняет токен текущем HttpContext, чтобы мы могли получить его
                // используя метод await HttpContext.GetTokenAsync(“Bearer”, “access_token”)или что-то подобное
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.SecretKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });
        }

        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.JwtAuthConfig(configuration);
            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddHostedService<JwtRefreshTokenCache>();

            return services;
        }
    }
}
