using Microsoft.AspNetCore.Builder;

namespace Nucleus.Extensions.MiddlewareProviders
{
    public static class RoutingMiddlewareProvider
    {
        public static void UseAppRouting(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL();
                });
        }
    }
}