using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Writers;

namespace Nucleus.Extensions.MiddlewareProviders
{
    public static class AuthMiddlewareProvider
    {
        public static void UseAuth(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();
        }
    }
}