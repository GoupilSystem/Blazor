using Birk.Client.Bestilling.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Birk.BestillingWeb.IntegrationTests
{
    public class HttpServiceIntegrationTests
    {
        private readonly HttpService _httpService;
        private readonly WireMockServer _server;
        
        public HttpServiceIntegrationTests()
        {
            // Create the mock server
            _server = WireMockServer.Start();

            // Create an instance of the HttpService using the mock HttpClient and the mock BaseUrlConfiguration
            var httpClient = new HttpClient();
            // We will configure the HttpClient in HttpService to use the mock server URL
            var url = new Uri(_server.Urls[0]).ToString();
            var timeoutSeconds = 30;
            _httpService = new HttpService(httpClient, url, timeoutSeconds);
        }

        [Fact]
        public async Task HttpGet_Success_ReturnsExpectedData_Test()
        {
            // Arrange
            var expectedData = new { Name = "John", Age = 30 };
            var uri = "/api/users/1";

            // Using WireMock.Server package
            // Define a mock response for the HTTP request to /api/users/1
            _server.Given(Request.Create().WithPath(uri).UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(expectedData));
            // Act
            var result = await _httpService.HttpGet<dynamic>(uri);

            // Assert
            Assert.True(result.IsSuccess);
            // Result.Data is a JsonElement object, we retrieve the values by using the GetProperty method
            // with the property name as a string argument.
            Assert.Equal(expectedData.Name, result.Data.GetProperty("Name").GetString());
            Assert.Equal(expectedData.Age, result.Data.GetProperty("Age").GetInt32());

            // Stop the mock server
            _server.Stop();
        }

        [Fact]
        public async Task HttpGet_Unsuccessful_ReturnsProblemDetails()
        {
            // Arrange
            var uri = "/api/users/2";
            var problemDetail = "The requested resource was not found.";

            // Define a mock response for the HTTP request to /api/users/1
            _server.Given(Request.Create().WithPath(uri).UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.NotFound)
                .WithBodyAsJson(new ProblemDetails
                {
                    Type = "https://example.com/problem",
                    Title = "Resource not found",
                    Detail = problemDetail
                }));

            // Act
            var result = await _httpService.HttpGet<ProblemDetails>(uri);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.ProblemDetails);
            Assert.Equal((int)HttpStatusCode.NotFound, result.ProblemDetails.Status);
            Assert.True(result.ProblemDetails.Detail.Contains(problemDetail));

            // Stop the mock server
            _server.Stop();
        }


    }

}
