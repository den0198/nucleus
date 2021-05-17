using Microsoft.AspNetCore.Builder;

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