using System;
using System.Linq;
using System.Security.Claims;
using DAL.EntityFramework;
using DAL.Initialization.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
            var roleManager = scope?.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope?.ServiceProvider.GetRequiredService<UserManager<AccountEntity>>();

            if (context == null)
                throw new Exception("context is null");

            if (roleManager == null)
                throw new Exception("context roles is null");

            if (userManager == null)
                throw new Exception("context users is null");

            #region InitialsDataBase 

            #region Roles

            var rolesSeeds = new RoleSeeds().Get().ToList();

            foreach (var identityRole in rolesSeeds)
            {
                if (await roleManager.FindByNameAsync(identityRole.Name) != null)
                    continue;

                await roleManager.CreateAsync(identityRole);
                await roleManager.AddClaimAsync(identityRole, new Claim(ClaimTypes.Role, identityRole.Name));
            }

            #endregion

            #region Accounts

            var accountSeeds = new AccountSeeds().Get().ToList();
            var usersSeeds = new UserSeed().Get().ToList();

            foreach (var account in accountSeeds)
            {
                if (await userManager.FindByNameAsync(account.Login) != null)
                    continue;

                var accountEntity = new AccountEntity()
                {
                    UserName = account.Login,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                    User =  usersSeeds.FirstOrDefault(user => user.FirstName == account.Login)
                };
                await userManager.CreateAsync(accountEntity, account.Password);

                foreach (var role in rolesSeeds)
                {
                    await userManager.AddToRoleAsync(accountEntity, role.Name);
                }

                await userManager.AddClaimAsync(accountEntity,
                    new Claim(ClaimTypes.Email, accountEntity.UserName));
            }

            #endregion

            #endregion

            await context.SaveChangesAsync();

        }
        
    }
}