using DAL.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.AppSettings;
using Models.EntitiesDatabase;

namespace Nucleus.Extensions.ServicesProviders
{
    public static class EntityFrameworkServiceProvider
    {
        public static void AddEntityFramework
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            var authAudience = configuration.GetSection("AuthOptions").Get<AuthOptions>().Audience;
            var passwordOptions = configuration.GetSection(nameof(PasswordOptions))?.Get<PasswordOptions>();
            
            addConnectionString(services, connectionString);
            addIdentity(services, authAudience, passwordOptions);
        }

        private static void addConnectionString(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private static void addIdentity(IServiceCollection services, string authAudience, PasswordOptions passwordOptions)
        {
            services.AddIdentityCore<AccountEntity>(options =>
                {
                    options.Password = passwordOptions ?? options.Password;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddTokenProvider(authAudience, typeof(DataProtectorTokenProvider<AccountEntity>));
        }
    }
}