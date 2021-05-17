using System.Diagnostics.CodeAnalysis;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Models.EntitiesDatabase;

namespace Nucleus.GraphQlApi.Queries
{
    [SuppressMessage("ReSharper", "CA1822")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    
    [ExtendObjectType(typeof(MainQuery))]
    
    public class UserQuery
    {
        [Authorize]
        public AccountEntity GetUser () => new AccountEntity(); 
    }
}