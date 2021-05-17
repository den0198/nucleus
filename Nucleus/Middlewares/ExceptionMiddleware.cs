using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NucLog;

namespace Nucleus.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly Logger logger;

        public ExceptionMiddleware(RequestDelegate requestDelegate, Logger logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate.Invoke(httpContext);
            }
            catch (Exception error)
            {
                logger.LogWarning(error.Message);
            }
        }
    }
}