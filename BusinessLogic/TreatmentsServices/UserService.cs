using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Components.Consists;
using Microsoft.AspNetCore.Identity;
using Models.EntitiesDatabase;
using Models.Requests;
using Models.Responses;

namespace BusinessLogic.TreatmentsServices
{
    public class UserService
    {
        private readonly UserManager<AccountEntity> userManager;

        public UserService(UserManager<AccountEntity> userManager)
        {
            this.userManager = userManager;
        }
        
        public async Task<RegistryUserResponse> RegisterUser(RegistryUserRequest request)
        {
            var newAccount = (AccountEntity) request.Account;
            var newUser = (UserEntity) request.User;
            newAccount.User = newUser;

            var resultCreateUser = await userManager.CreateAsync(newAccount, request.Password);

            if (!resultCreateUser.Succeeded)
                throw new Exception("error register");

            var account = await userManager.FindByNameAsync(newAccount.UserName);
            
            await userManager.AddToRoleAsync(account, RolesConsists.USER);
            await userManager.AddClaimAsync(account, new Claim(ClaimTypes.Email, account.UserName));
            
            
            return new RegistryUserResponse
            {
                SignInResponse = new SignInResponse
                {
                    UserId = newAccount.User.Id
                }
            };
        }
    }
}