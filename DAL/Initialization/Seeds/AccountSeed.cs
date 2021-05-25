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
                    Login = "test1",
                    Password = "admin",
                    Role = RolesConsists.ADMIN
                },
                
                new()
                {
                    Login  = "test2",
                    Password = "user",
                    Role = RolesConsists.USER
                        
                },
                
                new()
                {
                    Login = "test3",
                    Password = "guest",
                    Role = RolesConsists.GUEST
                }
            };
    }
}