using Microsoft.AspNetCore.Builder;
using Nucleus.Middlewares;

namespace Nucleus.Extensions.MiddlewareProviders
{
    public static class LoggerMiddlewareProvider
    {
        public static void UseLogger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<LoggerRequestAndResponseMiddleware>();
        }
    }
}