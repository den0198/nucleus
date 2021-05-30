using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models.AppSettings;
using Models.Bases;
using Models.EntitiesDatabase;
using Models.Requests;
using Models.Responses;

namespace BusinessLogic.TreatmentsServices
{
    public class AuthService
    {
        private readonly UserManager<AccountEntity> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AuthOptions authOptions;
        
        private readonly AuthHandler handler;

        public AuthService(UserManager<AccountEntity> userManager,
            RoleManager<IdentityRole> roleManager, IOptions<AuthOptions> authOptions)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authOptions = authOptions.Value;

            handler = new AuthHandler();
        }
        
        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            var account = await userManager.FindByNameAsync(request.Login);

            if (account == null)
                throw new Exception("User not in system");
            
            if (!await userManager.CheckPasswordAsync(account, request.Password))
                throw new Exception("Login or Password is not correct");

            var tokenBase = await getTokenBase(account);

            return new SignInResponse
            {
                UserId = account.User.Id,
                AccessToken = tokenBase.AccessToken,
                RefreshToken = tokenBase.RefreshToken
            };
        }

        public async Task<NewTokenResponse> NewToken(NewTokenRequest request)
        {
            var accountInfo = handler.GetAccountInfoByOldToken(request.AccessToken, authOptions);

            if (accountInfo == null)
                throw new Exception("Access token is invalid");
            
            var accountLogin = accountInfo.Claims
                .Where(_ => _.Type.Contains("email"))
                .Select(_ => _.Value)
                .FirstOrDefault();
            
            var account = await userManager.FindByNameAsync(accountLogin);
            if (account == null)
                throw new Exception("User is not system");
            
            var isTokenValid = await userManager.VerifyUserTokenAsync(account,
                authOptions.Audience, "RefreshToken", request.RefreshToken);
            if (!isTokenValid)
                throw new Exception("Refresh token is invalid");
            
            var tokenBase = await getTokenBase(account);
                
            return new NewTokenResponse
            {
                AccessToken = tokenBase.AccessToken,
                RefreshToken = tokenBase.RefreshToken
            };
        }
        

        private async Task<TokenBase> getTokenBase(AccountEntity account)
        {
            var accountRoles = await userManager.GetRolesAsync(account);

            var claims = await userManager.GetClaimsAsync(account);
            foreach (var accountRole in accountRoles)
            {
                var role = await roleManager.FindByNameAsync(accountRole); 
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            
            var accessToken = handler.GetAccessToken(claims, authOptions);
            var refreshToken = await userManager.GenerateUserTokenAsync(account, authOptions.Audience,
                "RefreshToken");

            await userManager.RemoveAuthenticationTokenAsync(account, 
                authOptions.Audience, "RefreshToken");
            await userManager.SetAuthenticationTokenAsync(account, 
                authOptions.Audience, "RefreshToken", refreshToken);

            return new TokenBase
            {
                AccessToken = accessToken,
                RefreshToken =  refreshToken
            };
        }
    }
}