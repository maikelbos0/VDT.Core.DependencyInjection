using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VDT.Core.DependencyInjection.Decorators;
using VDT.Core.Demo.Decorators;

namespace VDT.Core.Demo {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<LogDecorator>();
            services.AddScoped<IDemo, Demo>(options => options.AddAttributeDecorators());
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
