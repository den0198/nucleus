using System.Collections.Generic;
using Components.Consists;
using Microsoft.AspNetCore.Identity;

namespace DAL.Initialization.Seeds
{
    public class RoleSeeds
    {
        public IEnumerable<IdentityRole> Get() =>
            new IdentityRole[]
            {
                new()
                {
                    Name = RolesConsists.USER
                },
                new()
                {
                    Name = RolesConsists.ADMIN
                },
                new()
                {
                    Name = RolesConsists.GUEST
                }
            };
    }
}