using System.Diagnostics.CodeAnalysis;
using Components.Consists;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;

namespace Nucleus.GraphQlApi.Queries
{
    [SuppressMessage("ReSharper", "CA1822")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    
    [ExtendObjectType(typeof(MainQuery))]
    public class UserQuery
    {
        [Authorize]
        public string GetUser () => "gud"; 
    }
}