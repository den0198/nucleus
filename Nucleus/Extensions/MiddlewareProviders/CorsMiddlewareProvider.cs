using Microsoft.AspNetCore.Builder;

namespace Nucleus.Extensions.MiddlewareProviders
{
    public static class CorsMiddlewareProvider
    {
        public static void UseAppCors(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.
                UseCors(corsPolicyBuilder => 
                    corsPolicyBuilder.AllowAnyMethod().AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials());
        }
    }
}