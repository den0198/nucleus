using DAL.EntityFramework;
using DAL.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.Extensions.MiddlewareProviders
{
    public static class DatabaseMiddlewareProvider
    {
        public static void InitializationDatabase(this IApplicationBuilder applicationBuilder)
        {
            addAutoMigration(applicationBuilder);
            addSeedData(applicationBuilder);
        }

        private static void addSeedData(IApplicationBuilder applicationBuilder)
        {
            InitData.InitialData(applicationBuilder);
        }

        private static void addAutoMigration(IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder
                .ApplicationServices
                .GetService<IServiceScopeFactory>()
                ?.CreateScope();
            
            scope?.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
        }
    }
}