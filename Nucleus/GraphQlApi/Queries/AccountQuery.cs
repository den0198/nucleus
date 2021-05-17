using System.Diagnostics.CodeAnalysis;
using HotChocolate.Types;
using Models.EntitiesDatabase;

namespace Nucleus.GraphQlApi.Queries
{
    [SuppressMessage("ReSharper", "CA1822")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    
    [ExtendObjectType(typeof(MainQuery))]
    public class AccountQuery
    {
        public AccountEntity Authorization () => new AccountEntity(); 
    }
}