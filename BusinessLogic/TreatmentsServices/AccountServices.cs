using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Models.AppSettings;
using Models.Bases;
using Models.EntitiesDatabase;
using Models.Requests;
using Models.Responses;

namespace BusinessLogic.TreatmentsServices
{
    public class AccountServices
    {
        private readonly UserManager<AccountEntity> userManager;
        private readonly IConfiguration configuration;
        
        private readonly AccountHandler handler;

        public AccountServices(UserManager<AccountEntity> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;

            handler = new AccountHandler();
        }
        
        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            var account = await userManager.FindByNameAsync(request.Login);

            if (account == null)
                throw new Exception("User not in system");

            if (!await userManager.CheckPasswordAsync(account, request.Password))
                throw new Exception("Login or Password is not correct");
                
            var accountBase = (AccountBase) account;
            var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();
                
            var accessToken = handler.GetAccessToken(accountBase, authOptions);
            var refreshToken = handler.GetRefreshToken();

            await userManager.RemoveAuthenticationTokenAsync(account, 
                authOptions.Audience, "RefreshToken");
            await userManager.SetAuthenticationTokenAsync(account, 
                authOptions.Audience, "RefreshToken", refreshToken);

            return new SignInResponse
            {
                UserId = account.UserId,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<NewTokenResponse> NewToken(NewTokenRequest request)
        {
            var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();
            var accountInfo = handler.GetAccountInfoByOldToken(request.AccessToken, authOptions);

            if (accountInfo == null)
                throw new Exception("Access token is invalid");
            
            var accountLogin = accountInfo.Claims
                .Where(_ => _.Type.Contains("email"))
                .Select(_ => _.Value)
                .FirstOrDefault();


            var account = await userManager.FindByNameAsync(accountLogin);

            if (!await userManager.VerifyUserTokenAsync(account, 
                authOptions.Audience, "RefreshToken", request.RefreshToken))
                throw new Exception("Refresh token is invalid");
            
            if (account == null)
                throw new Exception("User is not system");
            
            
            var accountBase = (AccountBase) account;
            
            var accessToken = handler.GetAccessToken(accountBase, authOptions);
            var refreshToken = handler.GetRefreshToken();
            
            await userManager.RemoveAuthenticationTokenAsync(account, 
                authOptions.Audience, "RefreshToken");
            await userManager.SetAuthenticationTokenAsync(account, 
                authOptions.Audience, "RefreshToken", refreshToken);
                
            return new NewTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}