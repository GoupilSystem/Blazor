using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models.Responses;
using Birk.Client.Bestilling.Services.Interfaces;

namespace Birk.Client.Bestilling.Services.Implementation
{
    public class BarnService : IBarnService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<BarnService> _logger;

        private SimplifiedKommuneDto[] _kommunes;
        private SimplifiedBarneverntjenesteDto[] _barneverntjenestes;

        public BarnService(IHttpService httpService, ILogger<BarnService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }
        
        public async Task<BarnOgPersonDto> GetBarnByFnr(string fnr)
        {
            _logger.LogInformation("Entering {Method}", nameof(GetBarnByFnr));

            var response = await _httpService.HttpGet<GetBarnOgPersonByFnrResponse>($"BarnOgPersonByFnr/{fnr}");
            if (response.IsSuccess)
            {
                return response.Data.barnOgPersonDto;
            }
            return new BarnOgPersonDto();
        }
    }
}