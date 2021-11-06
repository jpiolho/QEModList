using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using QEModList.Core.Services;

namespace QEModList.Core
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SourcesListLoader>();
            services.AddSingleton<AddonsRepository>();

            services.AddHostedService(provider => provider.GetRequiredService<AddonsRepository>());

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
