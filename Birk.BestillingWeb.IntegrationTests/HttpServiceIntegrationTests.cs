using Birk.Client.Bestilling.Models.Configuration;
using Birk.Client.Bestilling.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Birk.BestillingWeb.IntegrationTests
{
    public class HttpServiceIntegrationTests
    {
        private readonly HttpService _httpService;

        public HttpServiceIntegrationTests()
        {
            var httpClient = new HttpClient();
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var baseUrlConfiguration = config.GetSection(BaseUrlConfiguration.CONFIG_NAME).Get<BaseUrlConfiguration>();
            var kodeverkApiBaseUrl = Options.Create(baseUrlConfiguration).Value.KodeverkApiBase;
            var timeoutSeconds = 30;
            _httpService = new HttpService(httpClient, kodeverkApiBaseUrl, timeoutSeconds);
        }

        [Fact]
        public async Task HttpGet_Success_ReturnsExpectedData_Test()
        {
            // Arrange
            var expectedData = new { Name = "John", Age = 30 };
            var uri = "/api/users/1";

            // Create the mock server
            var server = WireMockServer.Start();

            // Using WireMock.Server package
            // Define a mock response for the HTTP request to /api/users/1
            server.Given(Request.Create().WithPath(uri).UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(expectedData));

            // Configure the HttpClient to use the mock server URL
            var httpClient = new HttpClient { BaseAddress = new Uri(server.Urls[0]) };

            // Create an instance of the HttpService using the mock HttpClient and the mock BaseUrlConfiguration
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var baseUrlConfiguration = config.GetSection(BaseUrlConfiguration.CONFIG_NAME).Get<BaseUrlConfiguration>();
            var kodeverkApiBaseUrl = Options.Create(baseUrlConfiguration).Value.KodeverkApiBase;
            var timeoutSeconds = 30;
            var httpService = new HttpService(httpClient, kodeverkApiBaseUrl, timeoutSeconds);

            // Act
            var result = await httpService.HttpGet<dynamic>(uri);

            // Assert
            Assert.True(result.IsSuccess);
            // Result.Data is a JsonElement object, we retrieve the values by using the GetProperty method
            // with the property name as a string argument.
            Assert.Equal(expectedData.Name, result.Data.GetProperty("Name").GetString());
            Assert.Equal(expectedData.Age, result.Data.GetProperty("Age").GetInt32());

            // Stop the mock server
            server.Stop();
        }

        [Fact]
        public async Task HttpGet_Unsuccessful_ReturnsProblemDetails()
        {
            // Arrange
            var uri = "/api/users/2";
            var problemDetail = "The requested resource was not found.";

            // Create the mock server
            var server = WireMockServer.Start();

            // Define a mock response for the HTTP request to /api/users/1
            server.Given(Request.Create().WithPath(uri).UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.NotFound)
                    .WithBodyAsJson(new ProblemDetails
                    {
                        Type = "https://example.com/problem",
                        Title = "Resource not found",
                        Detail = problemDetail
                    }));

            // Configure the HttpClient to use the mock server URL
            var httpClient = new HttpClient { BaseAddress = new Uri(server.Urls[0]) };

            // Create an instance of the HttpService using the mock HttpClient and the mock BaseUrlConfiguration
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var baseUrlConfiguration = config.GetSection(BaseUrlConfiguration.CONFIG_NAME).Get<BaseUrlConfiguration>();
            var kodeverkApiBaseUrl = Options.Create(baseUrlConfiguration).Value.KodeverkApiBase;
            var timeoutSeconds = 30;
            var httpService = new HttpService(httpClient, kodeverkApiBaseUrl, timeoutSeconds);

            // Act
            var result = await httpService.HttpGet<ProblemDetails>(uri);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.ProblemDetails);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ProblemDetails.Status);
            Assert.True(result.ProblemDetails.Detail.Contains(problemDetail));

            // Stop the mock server
            server.Stop();
        }


    }

}
