using System;
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
            var tokenBase = handler.GetSignInResponse(accountBase, authOptions);

            return new SignInResponse
            {
                Token = tokenBase.Token
            };
        }
    }
}