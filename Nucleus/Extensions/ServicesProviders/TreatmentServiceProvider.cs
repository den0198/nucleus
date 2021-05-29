using BusinessLogic.TreatmentsServices;
using Microsoft.Extensions.DependencyInjection;

namespace Nucleus.Extensions.ServicesProviders
{
    public static class TreatmentServiceProvider
    {
        public static void AddTreatment(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
        }
    }
}