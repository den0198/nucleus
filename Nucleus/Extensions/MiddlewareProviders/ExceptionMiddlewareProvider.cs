using Microsoft.AspNetCore.Builder;
using Nucleus.Middlewares;

namespace Nucleus.Extensions.MiddlewareProviders
{
    public static class ExceptionMiddlewareProvider
    {
        public static void UseException
            (this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}