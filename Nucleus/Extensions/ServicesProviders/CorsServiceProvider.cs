using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.Extensions.ServicesProviders
{
    public static class CorsServiceProvider
    {
        public static void AddAppCors(this IServiceCollection services)
        {
            services.AddCors();
        }
        
    }
}