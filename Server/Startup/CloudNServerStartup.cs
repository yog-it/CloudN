using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using YogIT.Module.CloudN.Repository;
using YogIT.Module.CloudN.Services;

namespace YogIT.Module.CloudN.Startup
{
    public class CloudNServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICloudNService, ServerCloudNService>();
            services.AddDbContextFactory<CloudNContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
