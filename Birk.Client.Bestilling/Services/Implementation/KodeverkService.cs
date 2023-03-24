using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Services.Interfaces;
using Birk.Client.Bestilling.Utils.Constants;

namespace Birk.Client.Bestilling.Services.Implementation
{
    public class KodeverkService : IKodeverkService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<KodeverkService> _logger;

        private SimplifiedKommuneDto[] _kommunes;
        private SimplifiedBarneverntjenesteDto[] _barneverntjenestes;

        public KodeverkService(IHttpService httpService, ILogger<KodeverkService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<string[]> GetTypes()
        {
            _logger.LogInformation("Entering {Method}", nameof(GetTypes));

            var response = await _httpService.HttpGet<List<BestillingTypeDto>>("bestillingtypes");
            if (response.IsSuccess)
            {
                return response.Data.Select(bt => bt.Verdi).ToArray();
            }
            return new[] { Language.NO["NoData"] };
        }

        public async Task GetKommunesAndBarneverntjenestes()
        {
            _logger.LogInformation("Entering {Method}", nameof(GetKommunesAndBarneverntjenestes));

            var kommuneResponse = await _httpService.HttpGet<List<SimplifiedKommuneDto>>("kommunes");
            if (kommuneResponse.IsSuccess)
            {
                _kommunes = kommuneResponse.Data.DistinctBy(k => k.Navn).OrderBy(k => k.Navn).ToArray();

                var barneverntjenesteResponse = await _httpService.HttpGet<List<TempBarneverntjenesteDto>>("barneverntjenestes");
                // We get from KodeverkApi a [] of TempBarneverntjenesteDto
                // with non unique string values EnhetsnavnOgBydelsnavnproperty and Kommunenavn
                // F.ex:
                // TempBarneverntjenesteDto1: EnhetsnavnOgBydelsnavn = 'Indre Namdal barnevern', KommuneNavn = 'Grong'
                // TempBarneverntjenesteDto2: EnhetsnavnOgBydelsnavn = 'Indre Namdal barnevern', KommuneNavn = 'Høylandet'
                // We need to convert this to an [] of SimplifiedBarneverntjenesteDto with unique value EnhetsnavnOgBydelsnavnproperty
                // and a string[] Kommunenavns
                // F.ex:
                // SimplifiedBarneverntjenesteDto: EnhetsnavnOgBydelsnavn = 'Indre Namdal barnevern', KommuneNavns = 'Grong', 'Høylandet'
                if (barneverntjenesteResponse.IsSuccess)
                {
                    var tjenestesWithDuplicatedNames = barneverntjenesteResponse.Data.ToArray();
                    _barneverntjenestes = tjenestesWithDuplicatedNames
                        .GroupBy(t => t.EnhetsnavnOgBydelsnavn)
                        .Select(g => new SimplifiedBarneverntjenesteDto
                        {
                            EnhetsnavnOgBydelsnavn = g.Key,
                            Kommunenavns = g.Select(s => s.Kommunenavn).OrderBy(k => k).ToArray()
                        })
                        .OrderBy(sb => sb.Kommunenavns[0])
                        .ToArray();
                }
            }
        }

        public string[] GetKommunes() => _kommunes?.Select(k => k.Navn).ToArray() ?? new[] { Language.NO["NoData"] };

        public string[] GetBarneverntjenestes() => _barneverntjenestes?.Select(bt => bt.EnhetsnavnOgBydelsnavn).ToArray() ?? new[] { Language.NO["NoData"] };

        public string[] GetBarneverntjenestesByKommunenavn(string kommunenavn) =>
            _barneverntjenestes == null 
                ? new[] { Language.NO["NoData"] }
                : string.IsNullOrEmpty(kommunenavn) 
                    ? _barneverntjenestes.Select(k => k.EnhetsnavnOgBydelsnavn).ToArray()
                    : _barneverntjenestes.Where(k => k.Kommunenavns.Contains(kommunenavn)).Select(k => k.EnhetsnavnOgBydelsnavn).ToArray();
    }
}