using Birk.BestillingWeb.UnitTests.Models;
using Birk.Client.Bestilling.Models.Configuration;
using Birk.Client.Bestilling.Services.Implementation;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;


namespace Birk.BestillingWeb.UnitTests
{
    public class HttpServiceTests
    {
        private readonly HttpService _httpService;
        private readonly HttpClient _httpClient;

        public HttpServiceTests()
        {
            _httpClient = new HttpClient();
            _httpService = new HttpService(_httpClient, null);
        }

        [Fact]
        public async Task HttpGet_ReturnsSuccessResult_WhenResponseIsSuccess_Test()
        {
            // Arrange
            var uri = "https://example.com";
            var responseContent = "{\"id\":1,\"name\":\"Test\"}";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent)
            };
            // Cant mock HttpClient.GetAsync() direcly since this is not a virtual class.
            // GetAsync() uses SendAsync(), so we are using HttpMessageHandler to mock the HttpClient.SendAsync method.
            // Then, we create a new HttpClient instance using the mocked HttpMessageHandler.
            // Finally, we create an instance of the HttpService class using the mocked HttpClient instance and test the HttpGet method.
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);
            var httpService = new HttpService(httpClient, Options.Create(new BaseUrlConfiguration()));

            // Act
            var result = await httpService.HttpGet<HttpServiceDataTest>(uri);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("Test", result.Data.Name);
        }

        [Fact]
        public async Task HttpGet_ReturnsUnsuccessfulResult_WhenResponseIsNotSuccess_Test()
        {
            // Arrange
            var uri = "https://example.com";
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);
            var httpService = new HttpService(httpClient, Options.Create(new BaseUrlConfiguration()));

            // Act
            var result = await httpService.HttpGet<HttpServiceDataTest>(uri);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.NotNull(result.ProblemDetails);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.ProblemDetails.Status);
        }
    }
}