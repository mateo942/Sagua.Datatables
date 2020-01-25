using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sagua.Table.Abstractions;
using Sagua.Table.Helpers;

namespace Sagua.Datatable.Example
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(cfg =>
            {
                cfg.AddConsole();
                cfg.SetMinimumLevel(LogLevel.Trace);
            });

            services.AddScoped<IThemeProvider, ThemeProvider>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
