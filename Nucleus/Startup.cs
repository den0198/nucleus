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
            #region Infrostrucrure
            
            services.AddCors();
            services.AddControllers();
            services.AddEntityFrameworkConnectionString(configuration);
            services.AddLogger(configuration);
            services.AddAppGraphQl();
            services.AddAuth(configuration);
            
            #endregion
            
            services.AddTreatment();
        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Infrostructure

            app.UseStaticFiles();
            app.UseAppCors();
            app.UseLogger();
            app.UseException();
            app.UseAuth();
            app.UseAppRouting();    

            #endregion
        }
    }
}