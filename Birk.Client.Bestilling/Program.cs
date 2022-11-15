using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Birk.Client.Bestilling;
using MudBlazor.Services;
using Birk.Client.Bestilling.Models.Configuration;
using Birk.Client.Bestilling.Services;
using Birk.Client.Bestilling.Services.Implementation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
var baseUrlConfig = configSection.Get<BaseUrlConfiguration>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseUrlConfig.ApiBase)
});

builder.Services.AddScoped<HttpService>();
builder.Services.AddScoped<BestillingService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();