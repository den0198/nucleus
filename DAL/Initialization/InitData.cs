using System;
using DAL.EntityFramework;
using DAL.Initialization.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Models.EntitiesDatabase;

namespace DAL.Initialization
{
    public static class InitData
    {
        public static async void InitialData(IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder
                .ApplicationServices
                .GetService<IServiceScopeFactory>()
                ?.CreateScope();

            var context = scope?.ServiceProvider.GetRequiredService<AppDbContext>();

            if (context == null)
                throw new Exception("context is null");
            
            #region Initials

            context.AccountEntities.AddRange(new AccountSeed().Get());
                
            #endregion

            await context.SaveChangesAsync();

        }
    }
}