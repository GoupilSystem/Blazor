using Birk.Client.Bestilling.Data;
using Birk.Client.Bestilling.Models.Configuration;
using Birk.Client.Bestilling.Services.Implementation;
using MudBlazor.Services;
using Birk.Client.Bestilling.Services.Interfaces;

namespace Birk.Client.Bestilling.Configuration
{
    public static class ServicesConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration config)
        {
            // Add services to the container.
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            var configSection = config.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
            var baseUrlConfig = configSection.Get<BaseUrlConfiguration>();

            services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(baseUrlConfig.KodeverkApiBase)
            });

            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IBestillingService, BestillingService>();

            services.AddMudServices();

            services.AddHttpContextAccessor();

            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("Custom-correlation-ID", "800");
            });
        }
    }
}
