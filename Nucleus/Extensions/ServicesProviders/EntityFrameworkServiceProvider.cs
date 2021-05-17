using DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.Extensions.ServicesProviders
{
    public static class EntityFrameworkServiceProvider
    {
        public static void AddEntityFrameworkConnectionString
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure();
                    });
            });
        }
    }
}