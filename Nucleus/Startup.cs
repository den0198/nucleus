using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Extensions.MiddlewareProviders;
using Nucleus.Extensions.ServicesProviders;

namespace Nucleus
{
    public class Startup
    {

        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppCors();
            services.AddEntityFrameworkConnectionString(configuration);
            services.AddLogger(configuration);
            services.AddControllers();
            
            services.AddAuth(configuration);
            services.AddAppGraphQl();
            services.AddTreatment();
            
        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseAppCors();
            app.UseLogger();
            app.UseException();
            app.UseAuth();
            app.UseAppRouting();    
        }
    }
}