using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NucLog;

namespace Nucleus.Middlewares
{
    public class LoggerRequestAndResponseMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly Logger logger;

        public LoggerRequestAndResponseMiddleware(RequestDelegate requestDelegate, Logger logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            await requestDelegate.Invoke(httpContext);
        }
    }
}