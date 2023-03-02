using Birk.Client.Bestilling.Models.Configuration;
using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Services.Implementation;
using Birk.Client.Bestilling.Services.Interfaces;
using Birk.Client.Bestilling.Utils.Constants;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Birk.BestillingWeb.IntegrationTests
{
    public class BestillingServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly HttpService _httpService;
        private readonly BestillingService _bestillingService;
        private readonly Logger<BestillingService> _nullLogger;

        public BestillingServiceIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // Create an HttpClient that can be used to send requests to the web app
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var baseUrlConfiguration = config.GetSection(BaseUrlConfiguration.CONFIG_NAME).Get<BaseUrlConfiguration>();
            var url = baseUrlConfiguration.KodeverkApiBase;
            _httpClient = new HttpClient { BaseAddress = new Uri(url) };
            _httpService = new HttpService(_httpClient, Options.Create(baseUrlConfiguration));
            _nullLogger = new Logger<BestillingService>(new NullLoggerFactory());
            _bestillingService = new BestillingService(_httpService, _nullLogger);
        }

        [Fact]
        public async Task GetTypes_ReturnsExpectedData_Test()
        {
            // Arrange
            var expectedData = new List<BestillingTypeDto>
            {
                new BestillingTypeDto { Pk = 1, Verdi = "Type 1"},
                new BestillingTypeDto { Pk = 2, Verdi = "Type 2"}
            };

            // Create the mock server
            var server = WireMockServer.Start();

            // Using WireMock.Server package
            // Define a mock response for the HTTP request to /bestillingtypes
            server.Given(Request.Create().WithPath("/bestillingtypes").UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(expectedData));

            // Configure the HttpClient to use the mock server URL
            _httpClient.BaseAddress = new Uri(server.Urls[0]);

            // Act
            var result = await _bestillingService.GetTypes();

            // Assert
            Assert.Equal(expectedData.Select(bt => bt.Verdi).ToArray(), result);

            // Stop the mock server
            server.Stop();
        }

        [Fact]
        public async Task GetKommunesAndBarneverntjenestes_ReturnsExpectedData_Test()
        {
            // Arrange
            var expectedKommunes = new List<SimplifiedKommuneDto>
            {
                new SimplifiedKommuneDto { Navn = "Kommune 1"},
                new SimplifiedKommuneDto { Navn = "Kommune 2"}
            };

            var expectedBarneverntjenestes = new List<SimplifiedBarneverntjenesteDto>
            {
                new SimplifiedBarneverntjenesteDto { EnhetsnavnOgBydelsnavn = "Tjeneste 1", Kommunenavn = "Kommune 1"},
                new SimplifiedBarneverntjenesteDto { EnhetsnavnOgBydelsnavn = "Tjeneste 2", Kommunenavn = "Kommune 2"},
            };

            // Create the mock server
            var server = WireMockServer.Start();

            // Using WireMock.Server package
            // Define a mock response for the HTTP request to /kommunes and /barneverntjenestes
            server.Given(Request.Create().WithPath("/kommunes").UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(expectedKommunes));

            server.Given(Request.Create().WithPath("/barneverntjenestes").UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(expectedBarneverntjenestes));

            // Configure the HttpClient to use the mock server URL
            _httpClient.BaseAddress = new Uri(server.Urls[0]);

            // Act
            await _bestillingService.GetKommunesAndBarneverntjenestes();

            // Assert
            // Check that the _kommunes and _barneverntjenestes fields are not null and have the expected data
            string[] actualKommunes = _bestillingService.GetKommunes();
            string[] actualBarneverntjenestes = _bestillingService.GetBarneverntjenestes();

            Assert.NotNull(actualKommunes);
            Assert.NotNull(actualBarneverntjenestes);

            Assert.True(actualKommunes.Length == expectedKommunes.Count);
            Assert.True(actualBarneverntjenestes.Length == expectedBarneverntjenestes.Count);

            Assert.True(actualKommunes[0] == expectedKommunes[0].Navn);

            Assert.True(actualBarneverntjenestes[0] == expectedBarneverntjenestes[0].EnhetsnavnOgBydelsnavn);

            // Stop the mock server
            server.Stop();
        }

        [Fact]
        public async Task GetKommunesAndBarneverntjenestes_ReturnsInternalServerError_Test()
        {
            // Arrange
            // Create the mock server
            var server = WireMockServer.Start();

            // Using WireMock.Server package
            // Define a mock response for the HTTP request to /kommunes and /barneverntjenestes
            server.Given(Request.Create().WithPath("/kommunes").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

            server.Given(Request.Create().WithPath("/barneverntjenestes").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

            // Configure the HttpClient to use the mock server URL
            _httpClient.BaseAddress = new Uri(server.Urls[0]);

            // Act
            await _bestillingService.GetKommunesAndBarneverntjenestes();

            // Assert
            // Check that the _kommunes and _barneverntjenestes fields are not null and have the expected data
            string[] actualKommunes = _bestillingService.GetKommunes();
            string[] actualBarneverntjenestes = _bestillingService.GetBarneverntjenestes();

            Assert.NotNull(actualKommunes);
            Assert.NotNull(actualBarneverntjenestes);

            string[] expected = new[] { Language.NO["NoData"] };

            Assert.True(actualKommunes.SequenceEqual(expected));
            Assert.True(actualBarneverntjenestes.SequenceEqual(expected));

            // Stop the mock server
            server.Stop();
        }
    }

}
