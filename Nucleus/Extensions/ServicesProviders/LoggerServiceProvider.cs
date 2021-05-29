using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NucLog;

namespace Nucleus.Extensions.ServicesProviders
{
    public static class LoggerServiceProvider
    {
        public static void AddLogger(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton(_ => new Logger(configuration
                .GetSection("Logging").GetSection("NameFolder").Value));
    }
}