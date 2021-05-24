using System.Collections.Generic;
using Models.EntitiesDatabase;

namespace DAL.Initialization.Seeds
{
    public class AccountSeed
    {
        public IEnumerable<AccountEntity> Get() =>
            new AccountEntity[]
            {
                new()
                {
                    Login = "admin",
                    Password = "admin",
                    Role = "admin"
                },
                
                new()
                {
                    Login  = "user",
                    Password = "user",
                    Role = "user"
                },
                
                new()
                {
                    Login = "guest",
                    Password = "guest",
                    Role = "guest"
                }
            };
    }
}