using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models.Responses;
using Birk.Client.Bestilling.Models.ViewModels;
using Birk.Client.Bestilling.Services.Interfaces;
using Birk.Client.Bestilling.Utils.Mapper;

namespace Birk.Client.Bestilling.Services.Implementation
{
    public class BarnService : IBarnService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<BarnService> _logger;

        private SimplifiedKommuneDto[] _kommunes;
        private SimplifiedBvtjenesteDto[] _barneverntjenestes;

        public BarnService(IHttpService httpService, ILogger<BarnService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }
        
        public async Task<BarnViewModel> GetBarnByFnr(string fnr)
        {
            _logger.LogInformation("Entering {Method}", nameof(GetBarnByFnr));

            var response = await _httpService.HttpGet<GetBarnByFnrResponse>($"BarnOgPersonByFnr/{fnr}");
            if (response.IsSuccess)
            {
                return BarnMapper.ToBarnViewModel(response.Data.barnOgPersonDto, true);
            }
            return null;
        }
    }
}