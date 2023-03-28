using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Services.Implementation;
using Birk.Client.Bestilling.Utils.Constants;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Birk.BestillingWeb.IntegrationTests
{
    public class BestillingServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpService _httpService;
        private readonly KodeverkService _bestillingService;
        private readonly WireMockServer _server;
        private readonly Logger<KodeverkService> _nullLogger;

        public BestillingServiceIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // Create the mock server
            _server = WireMockServer.Start();

            // Create an instance of the HttpService using the mock HttpClient and the mock BaseUrlConfiguration
            var httpClient = new HttpClient();
            // We will configure the HttpClient in HttpService to use the mock server URL
            var url = new Uri(_server.Urls[0]).ToString();
            var timeoutSeconds = 30;
            _httpService = new HttpService(httpClient, url, timeoutSeconds);

            _nullLogger = new Logger<KodeverkService>(new NullLoggerFactory());
            _bestillingService = new KodeverkService(_httpService, _nullLogger);
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

            // Using WireMock.Server package
            // Define a mock response for the HTTP request to /bestillingtypes
            _server.Given(Request.Create().WithPath("/bestillingtypes").UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(expectedData));

            // Act
            var result = await _bestillingService.GetTypes();

            // Assert
            Assert.Equal(expectedData.Select(bt => bt.Verdi).ToArray(), result);

            // Stop the mock server
            _server.Stop();
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

            var expectedBarneverntjenestes = new List<SimplifiedBvtjenesteDto>
            {
                new SimplifiedBvtjenesteDto { EnhetsnavnOgBydelsnavn = "Tjeneste 1", Kommunenavns = new[] { "Kommune 1" } },
                new SimplifiedBvtjenesteDto { EnhetsnavnOgBydelsnavn = "Tjeneste 2", Kommunenavns = new[] { "Kommune 2" } },
            };

            // Using WireMock.Server package
            // Define a mock response for the HTTP request to /kommunes and /barneverntjenestes
            _server.Given(Request.Create().WithPath("/kommunes").UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(expectedKommunes));

            _server.Given(Request.Create().WithPath("/barneverntjenestes").UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(expectedBarneverntjenestes));

            // Act
            await _bestillingService.GetKommunesAndBvtjenestes();

            // Assert
            // Check that the _kommunes and _barneverntjenestes fields are not null and have the expected data
            string[] actualKommunes = _bestillingService.GetKommunes();
            string[] actualBarneverntjenestes = _bestillingService.GetBvtjenetes();

            Assert.NotNull(actualKommunes);
            Assert.NotNull(actualBarneverntjenestes);

            Assert.True(actualKommunes.Length == expectedKommunes.Count);
            Assert.True(actualBarneverntjenestes.Length == expectedBarneverntjenestes.Count);

            Assert.True(actualKommunes[0] == expectedKommunes[0].Navn);

            Assert.True(actualBarneverntjenestes[0] == expectedBarneverntjenestes[0].EnhetsnavnOgBydelsnavn);

            // Stop the mock server
            _server.Stop();
        }

        [Fact]
        public async Task GetKommunesAndBarneverntjenestes_ReturnsInternalServerError_Test()
        {
            // Arrange
            // Using WireMock.Server package
            // Define a mock response for the HTTP request to /kommunes and /barneverntjenestes
            _server.Given(Request.Create().WithPath("/kommunes").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

            _server.Given(Request.Create().WithPath("/barneverntjenestes").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

            // Act
            await _bestillingService.GetKommunesAndBvtjenestes();

            // Assert
            // Check that the _kommunes and _barneverntjenestes fields are not null and have the expected data
            string[] actualKommunes = _bestillingService.GetKommunes();
            string[] actualBarneverntjenestes = _bestillingService.GetBvtjenetes();

            Assert.NotNull(actualKommunes);
            Assert.NotNull(actualBarneverntjenestes);

            string[] expected = new[] { Language.NO["NoData"] };

            Assert.True(actualKommunes.SequenceEqual(expected));
            Assert.True(actualBarneverntjenestes.SequenceEqual(expected));

            // Stop the mock server
            _server.Stop();
        }
    }

}
