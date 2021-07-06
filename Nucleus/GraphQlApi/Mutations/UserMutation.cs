using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using BusinessLogic.TreatmentsServices;
using HotChocolate;
using HotChocolate.Types;
using Models.Bases;
using Models.Requests;
using Models.Responses;

namespace Nucleus.GraphQlApi.Mutations
{
    [SuppressMessage("ReSharper", @"CA1822")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]

    [ExtendObjectType(typeof(MainMutation))]
    public class UserMutation
    {
         public async Task<ResponseBase<RegisterUserResponse>> RegisterUser([Service] UserService service,
            RegistryUserRequest user) =>
                await service.RegisterUser(user);
    }
}