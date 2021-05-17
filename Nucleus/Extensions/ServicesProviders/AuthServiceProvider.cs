using Components.HelpersClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Models.AppSettings;

namespace Nucleus.Extensions.ServicesProviders
{
    public static class AuthServiceProvider
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();
            
            addAuthentication(services, authOptions);
        }

        private static void addAuthentication(IServiceCollection services, AuthOptions authOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = authOptions.Issuer,
                        ValidateIssuer = true,
                        ValidAudience = authOptions.Audience,
                        ValidateAudience = true,
                        IssuerSigningKey = AuthHelper.GetIssuerSigningKey(authOptions.Key),
                        ValidateIssuerSigningKey = true
                    };
                });
        }
    }
}