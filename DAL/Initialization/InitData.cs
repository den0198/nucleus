using System;
using System.Linq;
using DAL.EntityFramework;
using DAL.Initialization.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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

            /*var accountSeed = new AccountSeed().Get();
            if(!context.AccountEntities.Any(obj => obj.Login == accountSeed.FirstOrDefault().Login))
                context.AccountEntities.AddRange(accountSeed);*/
                
            #endregion

            await context.SaveChangesAsync();

        }
        
    }
}