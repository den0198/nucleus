using System.Collections.Generic;
using Components.Consists;
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
                    Role = RolesConsists.ADMIN
                },
                
                new()
                {
                    Login  = "user",
                    Password = "user",
                    Role = RolesConsists.USER
                        
                },
                
                new()
                {
                    Login = "guest",
                    Password = "guest",
                    Role = RolesConsists.GUEST
                }
            };
    }
}