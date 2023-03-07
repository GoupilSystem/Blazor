using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models.HttpResults;
using Birk.Client.Bestilling.Services.Implementation;
using Birk.Client.Bestilling.Services.Interfaces;
using Birk.Client.Bestilling.Utils.Constants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Net;

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
            Assert.Equal(new[] { Language.NO["NoData"] }, result);
        }

        [Fact]
        public async Task GetKommunesAndBarneverntjenestes_WhenResponsesAreSuccess_PopulatesKomAndBarFields()
        {
            // Arrange
            var expectedKommunes = new List<SimplifiedKommuneDto>()
            {
                new SimplifiedKommuneDto() { Navn = "Kommune1" },
                new SimplifiedKommuneDto() {  Navn = "Kommune2" },
            };
            var kommuneResponse = new HttpResult<List<SimplifiedKommuneDto>>(true, expectedKommunes);
            _httpServiceMock.Setup(x => x.HttpGet<List<SimplifiedKommuneDto>>("kommunes")).ReturnsAsync(kommuneResponse);

            var expectedBarneverntjenestes = new List<TempBarneverntjenesteDto>()
            {
                new TempBarneverntjenesteDto() { EnhetsnavnOgBydelsnavn = "Tjeneste 1", Kommunenavn = "Kommune 1" },
                new TempBarneverntjenesteDto() { EnhetsnavnOgBydelsnavn = "Tjeneste 2", Kommunenavn = "Kommune 2" },
            };
            var barneverntjenesteResponse = new HttpResult<List<TempBarneverntjenesteDto>>(true, expectedBarneverntjenestes);
            _httpServiceMock.Setup(x => x.HttpGet<List<TempBarneverntjenesteDto>>("barneverntjenestes")).ReturnsAsync(barneverntjenesteResponse);

            // Act
            await _bestillingService.GetKommunesAndBarneverntjenestes();

            // Assert
            Assert.Equal(expectedKommunes.Select(k => k.Navn).ToArray(), _bestillingService.GetKommunes());
            Assert.Equal(expectedBarneverntjenestes.Select(bt => bt.EnhetsnavnOgBydelsnavn).ToArray(), _bestillingService.GetBarneverntjenestes());
        }

        [Fact]
        public async Task GetKommunesAndBarneverntjenestes_ShouldNotSetKomAndBarArrays_WhenApiResponseIsNotSuccessful()
        {
            // Arrange
            _httpServiceMock.Setup(s => s.HttpGet<List<SimplifiedKommuneDto>>("kommunes"))
                           .ReturnsAsync(new HttpResult<List<SimplifiedKommuneDto>>(false, null));

            // Act
            await _bestillingService.GetKommunesAndBarneverntjenestes();

            // Assert
            string[] actualKommunes = _bestillingService.GetKommunes();
            string[] actualBarneverntjenestes = _bestillingService.GetBarneverntjenestes();

            Assert.NotNull(actualKommunes);
            Assert.NotNull(actualBarneverntjenestes);

            string[] expected = new[] { Language.NO["NoData"] };

            Assert.True(actualKommunes.SequenceEqual(expected));
            Assert.True(actualBarneverntjenestes.SequenceEqual(expected));
        }
    }
}