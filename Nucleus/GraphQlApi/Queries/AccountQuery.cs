using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BusinessLogic.TreatmentsServices;
using HotChocolate;
using HotChocolate.Types;
using Models.Requests;
using Models.Responses;

namespace Nucleus.GraphQlApi.Queries
{
    [SuppressMessage("ReSharper", "CA1822")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    
    [ExtendObjectType(typeof(MainQuery))]
    public class AccountQuery
    {
        public async Task<NewTokenResponse> NewToken ([Service] AccountServices service, 
            NewTokenRequest fullToken) =>
                await service.NewToken(fullToken);
    }
}