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
    public class KodeverkServiceTests
    {
        private readonly KodeverkService _kodeverkService;
        private readonly Mock<IHttpService> _httpServiceMock;
        private readonly Logger<KodeverkService> _nullLogger;

        public KodeverkServiceTests()
        {
            _httpServiceMock = new Mock<IHttpService>();
            _nullLogger = new Logger<KodeverkService>(new NullLoggerFactory());
            _kodeverkService = new KodeverkService(_httpServiceMock.Object, _nullLogger);
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
            var result = await _kodeverkService.GetTypes();

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
            var result = await _kodeverkService.GetTypes();

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

            var expectedBarneverntjenestes = new List<SimplifiedBarneverntjenesteDto>()
            {
                new SimplifiedBarneverntjenesteDto() { EnhetsnavnOgBydelsnavn = "Tjeneste 1", Kommunenavns = new[] { "Kommune 1" } },
                new SimplifiedBarneverntjenesteDto() { EnhetsnavnOgBydelsnavn = "Tjeneste 2", Kommunenavns = new[] { "Kommune 2" } },
            };

            // !!: We expect SimplifiedBarneverntjenesteDto type from the service but during the process we get TempBarneverntjenesteDto type from KodeverkApi
            var tempBarneverntjenestes = new List<TempBarneverntjenesteDto>();
            for (int i = 0; i < expectedBarneverntjenestes.Count; i++)
            {
                tempBarneverntjenestes.Add(new TempBarneverntjenesteDto()
                {
                    EnhetsnavnOgBydelsnavn = expectedBarneverntjenestes[i].EnhetsnavnOgBydelsnavn,
                    Kommunenavn = expectedBarneverntjenestes[i].Kommunenavns[0]
                });
            }
            var tempBarneverntjenesteResponse = new HttpResult<List<TempBarneverntjenesteDto>>(true, tempBarneverntjenestes);
            _httpServiceMock.Setup(x => x.HttpGet<List<TempBarneverntjenesteDto>>("barneverntjenestes")).ReturnsAsync(tempBarneverntjenesteResponse);

            // Act
            await _kodeverkService.GetKommunesAndBarneverntjenestes();

            // Assert
            Assert.Equal(expectedKommunes.Select(k => k.Navn).ToArray(), _kodeverkService.GetKommunes());
            Assert.Equal(expectedBarneverntjenestes.Select(bt => bt.EnhetsnavnOgBydelsnavn).ToArray(), _kodeverkService.GetBarneverntjenestes());
        }

        [Fact]
        public async Task GetKommunesAndBarneverntjenestes_ShouldNotSetKomAndBarArrays_WhenApiResponseIsNotSuccessful()
        {
            // Arrange
            _httpServiceMock.Setup(s => s.HttpGet<List<SimplifiedKommuneDto>>("kommunes"))
                           .ReturnsAsync(new HttpResult<List<SimplifiedKommuneDto>>(false, null));

            // Act
            await _kodeverkService.GetKommunesAndBarneverntjenestes();

            // Assert
            string[] actualKommunes = _kodeverkService.GetKommunes();
            string[] actualBarneverntjenestes = _kodeverkService.GetBarneverntjenestes();

            Assert.NotNull(actualKommunes);
            Assert.NotNull(actualBarneverntjenestes);

            string[] expected = new[] { Language.NO["NoData"] };

            Assert.True(actualKommunes.SequenceEqual(expected));
            Assert.True(actualBarneverntjenestes.SequenceEqual(expected));
        }
    }
}