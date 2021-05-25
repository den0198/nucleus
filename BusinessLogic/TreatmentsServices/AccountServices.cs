using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Handlers;
using DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.AppSettings;
using Models.Bases;
using Models.Requests;
using Models.Responses;

namespace BusinessLogic.TreatmentsServices
{
    public class AccountServices
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;
        
        private readonly AccountHandler handler;

        public AccountServices(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
            
            handler = new AccountHandler();
        }
        
        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            var account = await appDbContext.AccountEntities.SingleOrDefaultAsync(item =>
                item.Login == request.Login && item.Password == request.Password);

            if (account == null)
                throw new Exception("User not in system");

            var accountBase = (AccountBase) account;
            var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();
                
            var accessToken = handler.GetAccessToken(accountBase, authOptions);
            var refreshToken = handler.GetRefreshToken();

            account.RefreshToken = refreshToken;
            await appDbContext.SaveChangesAsync();

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
            

            var account = await appDbContext.AccountEntities
                .Where(_ => _.Login == accountLogin && _.RefreshToken == request.RefreshToken)
                .FirstOrDefaultAsync();
            
            if (account == null)
            {
                throw new Exception("User is not system");
            }
            
            var accountBase = (AccountBase) account;
            
            var accessToken = handler.GetAccessToken(accountBase, authOptions);
            var refreshToken = handler.GetRefreshToken();
            
            account.RefreshToken = refreshToken;
            await appDbContext.SaveChangesAsync();
                
            return new NewTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}