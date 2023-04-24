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
        private SimplifiedBvtjenesteDto[] _bvtjenestes;

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
                return response.Data
                    .Where(bt => bt.GyldigTilDato == null || bt.GyldigTilDato >= DateTime.Now)
                    .Select(bt => bt.Verdi)
                    .ToArray();
            }
            return new[] { Language.NO["NoData"] };
        }

        public async Task GetKommunesAndBvtjenestes()
        {
            _logger.LogInformation("Entering {Method}", nameof(GetKommunesAndBvtjenestes));

            var kommuneResponse = await _httpService.HttpGet<List<SimplifiedKommuneDto>>("kommunes");
            if (kommuneResponse.IsSuccess)
            {
                _kommunes = kommuneResponse.Data.DistinctBy(k => k.Navn).OrderBy(k => k.Navn).ToArray();

                var barneverntjenesteResponse = await _httpService.HttpGet<List<TempBvtjenesteDto>>("barneverntjenestes");
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
                    _bvtjenestes = tjenestesWithDuplicatedNames
                        .GroupBy(t => t.EnhetsnavnOgBydelsnavn)
                        .Select(g => new SimplifiedBvtjenesteDto
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

        public int GetKommuneIndexByBvtjeneste(string selectedBvtjeneste)
        {
            if (_bvtjenestes == null)
            {
                return -1;
            }

            foreach (var barneverntjeneste in _bvtjenestes)
            {
                if (barneverntjeneste.EnhetsnavnOgBydelsnavn == selectedBvtjeneste)
                {
                    var kommuneNavn = barneverntjeneste.Kommunenavns[0];
                    var kommune = _kommunes.FirstOrDefault(k => k.Navn == kommuneNavn);
                    return Array.IndexOf(_kommunes, kommune);
                }
            }

            return -1;
        }

        public string[] GetBvtjenetes() => _bvtjenestes?.Select(bt => bt.EnhetsnavnOgBydelsnavn).ToArray() ?? new[] { Language.NO["NoData"] };

        public string[] GetBvtjenestesByKommunenavn(string kommunenavn) =>
            _bvtjenestes == null 
                ? new[] { Language.NO["NoData"] }
                : string.IsNullOrEmpty(kommunenavn) 
                    ? _bvtjenestes.Select(k => k.EnhetsnavnOgBydelsnavn).ToArray()
                    : _bvtjenestes.Where(k => k.Kommunenavns.Contains(kommunenavn)).Select(k => k.EnhetsnavnOgBydelsnavn).ToArray();
    }
}