using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models.HttpResults;
using Birk.Client.Bestilling.Services.Implementation;
using Birk.Client.Bestilling.Services.Interfaces;
using Birk.Client.Bestilling.Utils.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;


namespace Birk.BestillingWeb.UnitTests
{
    public class BestillingServiceTests
    {
        private readonly BestillingService _bestillingService;
        private readonly Mock<IHttpService> _httpServiceMock;
        private readonly Logger<BestillingService> _nullLogger;

        public BestillingServiceTests()
        {
            _httpServiceMock = new Mock<IHttpService>();
            _nullLogger = new Logger<BestillingService>(new NullLoggerFactory());
            _bestillingService = new BestillingService(_httpServiceMock.Object, _nullLogger);
        }

        [Fact]
        public async Task GetTypes_ReturnsArrayOfStrings_WhenResponseIsSuccess_Test()
        {
            // Arrange
            var expectedData = new List<BestillingTypeDto>()
            {
                new BestillingTypeDto() { Verdi = "Type1" },
                new BestillingTypeDto() { Verdi = "Type2" },
            };
            var response = new HttpResult<List<BestillingTypeDto>>(true, expectedData);
            _httpServiceMock.Setup(x => x.HttpGet<List<BestillingTypeDto>>("bestillingtypes")).ReturnsAsync(response);

            // Act
            var result = await _bestillingService.GetTypes();

            // Assert
            Assert.Equal(expectedData.Select(bt => bt.Verdi).ToArray(), result);
        }

        [Fact]
        public async Task GetTypes_ReturnsArrayOfOneString_WhenResponseIsNotSuccess_Test()
        {
            // Arrange
            var response = new HttpResult<List<BestillingTypeDto>>(false, null);
            _httpServiceMock.Setup(x => x.HttpGet<List<BestillingTypeDto>>("bestillingtypes")).ReturnsAsync(response);

            // Act
            var result = await _bestillingService.GetTypes();

            // Assert
            Assert.Equal(new string[] { Language.NO["NoData"] }, result);
        }
    }
}