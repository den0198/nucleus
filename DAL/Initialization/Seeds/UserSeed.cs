using System.Collections.Generic;
using Models.EntitiesDatabase;

namespace DAL.Initialization.Seeds
{
    public class UserSeed
    {
        public IEnumerable<UserDetailsEntity> Get() =>
            new UserDetailsEntity[]
            {
                new()
                {
                   FirstName = "SuperUser",
                   LastName = "SuperUser",
                   MiddleName = "SuperUser",
                   Age = 22
                }
            };
    }
}
