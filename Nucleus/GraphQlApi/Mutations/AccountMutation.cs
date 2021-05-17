using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BusinessLogic.TreatmentsServices;
using HotChocolate;
using HotChocolate.Types;
using Models.Requests;
using Models.Responses;

namespace Nucleus.GraphQlApi.Mutations
{
    [SuppressMessage("ReSharper", "CA1822")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    
    [ExtendObjectType(typeof(MainMutation))
]
    public class AccountMutation
    {
        public async Task<SignInResponse> SignIn([Service] AccountServices service,
            SignInRequest request)
        {
            return await service.SignIn(request); 
        }
             
    }
}