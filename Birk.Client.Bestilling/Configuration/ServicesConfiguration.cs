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

            services.AddTransient<IBarnService>(provider =>
            {
                var baseUrl = baseUrlConfig.BarnApiBase;
                var httpClient = new HttpClient();
                return new BarnService(new HttpService(httpClient, baseUrl, httpTimeoutSeconds), provider.GetRequiredService<ILogger<BarnService>>());
            });

            services.AddTransient<IBestillingService>(provider =>
            {
                var baseUrl = baseUrlConfig.BestillingApiBase;
                var httpClient = new HttpClient();
                return new BestillingService(new HttpService(httpClient, baseUrl, httpTimeoutSeconds), provider.GetRequiredService<ILogger<BestillingService>>());
            });

            services.AddTransient<IKodeverkService>(provider =>
            {
                var baseUrl = baseUrlConfig.KodeverkApiBase;
                var httpClient = new HttpClient();
                return new KodeverkService(new HttpService(httpClient, baseUrl, httpTimeoutSeconds), provider.GetRequiredService<ILogger<KodeverkService>>());
            });

            services.AddMudServices();

            services.AddHttpContextAccessor();
                
            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("Custom-correlation-ID", Guid.NewGuid().ToString());
            });
        }
    }
}
