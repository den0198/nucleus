using DAL.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.EntitiesDatabase;

namespace Nucleus.Extensions.ServicesProviders
{
    public static class EntityFrameworkServiceProvider
    {
        public static void AddEntityFramework
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            
            addConnectionString(services, connectionString);
            addIdentity(services);
        }

        private static void addConnectionString(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        private static void addIdentity(IServiceCollection services)
        {
            services.AddIdentity<AccountEntity, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}