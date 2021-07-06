using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.Handlers;
using Components.Consists;
using Microsoft.AspNetCore.Identity;
using Models.Bases;
using Models.EntitiesDatabase;
using Models.Requests;
using Models.Responses;

namespace BusinessLogic.TreatmentsServices
{
    public class UserService
    {
        private readonly UserManager<AccountEntity> userManager;
        private readonly UserHandler handler;

        public UserService(UserManager<AccountEntity> userManager)
        {
            this.userManager = userManager;
            handler = new UserHandler();
        }
        
        public async Task<ResponseBase<RegisterUserResponse>> RegisterUser(RegistryUserRequest request)
        {
            var response = new ResponseBase<RegisterUserResponse>
            {
                Result = ResponseResultConsists.OK,
                ErrorList = null
            };

            var newAccount = handler.GetAccount(request.Account);
            var newUser =  handler.GetUser(request.User);
            newAccount.UserDetails = newUser;

            var resultCreateUser = await userManager.CreateAsync(newAccount, request.Account.Password);

            if (!resultCreateUser.Succeeded)
            {
                response.Result = ResponseResultConsists.ERROR;
                response.Data = null;
                response.ErrorList = new List<string>();

                resultCreateUser.Errors.ToList()
                    .ForEach(error => response.ErrorList.Add(error.Description));

                return response;
            }

            var account = await userManager.FindByNameAsync(newAccount.UserName);
            
            await userManager.AddToRoleAsync(account, RolesConsists.USER);
            await userManager.AddClaimAsync(account, new Claim(ClaimTypes.Email, account.UserName));

            response.Data = new RegisterUserResponse
            {
               AccountId = account.Id
            };

            return response;
        }
    }
}