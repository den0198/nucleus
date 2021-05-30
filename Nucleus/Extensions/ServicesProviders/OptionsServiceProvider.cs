using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.AppSettings;

namespace Nucleus.Extensions.ServicesProviders
{
    public static class OptionsServiceProvider
    {
        public static void RegisterAllOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));
        }
    }
}