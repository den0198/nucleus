using System;
using System.Security.Claims;
using DAL.EntityFramework;
using DAL.Initialization.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
            var roleManager = scope?.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (context == null)
                throw new Exception("context is null");

            if (roleManager == null)
                throw new Exception("context roles is null");
            
            #region Initials

            var rolesSeeds = new RoleSeeds().Get();
            foreach (var identityRole in rolesSeeds)
            {
                if (await roleManager.FindByNameAsync(identityRole.Name) != null) 
                    continue;
                
                await roleManager.CreateAsync(identityRole);
                await roleManager.AddClaimAsync(identityRole,new Claim(ClaimTypes.Role,identityRole.Name));
            }
            
            #endregion

            await context.SaveChangesAsync();

        }
        
    }
}