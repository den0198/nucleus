using System.Collections.Generic;
using Models.Bases;

namespace DAL.Initialization.Seeds
{
    public class AccountSeeds
    {
        public IEnumerable<RegisterUserBase> Get() =>
            new RegisterUserBase[]
            {
               new()
               {
                   Login = "SuperUser",
                   Password = "qwe123QWE!@#"
               }
            };
    }
}