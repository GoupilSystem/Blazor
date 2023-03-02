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

            var configSection = config.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
            var baseUrlConfig = configSection.Get<BaseUrlConfiguration>();
            var httpTimeoutSeconds = config.GetValue<int>("HttpTimeoutSeconds");

            services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(baseUrlConfig.KodeverkApiBase),
                Timeout = TimeSpan.FromSeconds(httpTimeoutSeconds)
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
