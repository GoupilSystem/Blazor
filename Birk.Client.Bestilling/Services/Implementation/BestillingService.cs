using Birk.Client.Bestilling.Enums;
using Birk.Client.Bestilling.Models;
using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models.Requests;
using Birk.Client.Bestilling.Models.Responses;
using Birk.Client.Bestilling.Services.Interfaces;
using Birk.Client.Bestilling.Utils.Constants;
using Birk.Client.Bestilling.Utils.Mapper;
using MudBlazor.Charts;
using System.Net.Http;
using System;
using Birk.Client.Bestilling.Models.HttpResults;
using System.Text.Json;

namespace Birk.Client.Bestilling.Services.Implementation
{
    public class BestillingService : IBestillingService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<BestillingService> _logger;

        private SimplifiedKommuneDto[] _kommunes;
        private SimplifiedBarneverntjenesteDto[] _barneverntjenestes;

        public BestillingService(IHttpService httpService, ILogger<BestillingService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<BestillingItem> Create(CreateBestillingItemRequest createBestillingItemRequest)
        {
            var response = await _httpService.HttpPost<CreateBestillingItemResponse>("catalog-items", createBestillingItemRequest);
            return response.Data?.BestillingItem;
        }

        public async Task<BestillingItem> Edit(BestillingItem bestillingItem)
        {
            var response = await _httpService.HttpPut<EditBestillingItemResponse>("catalog-items", bestillingItem);
            return response.Data?.BestillingItem;
        }

        public async Task<DeleteStatus> Delete(int bestillingItemId)
        {
            var response = await _httpService.HttpDelete<DeleteBestillingItemResponse>("catalog-items", bestillingItemId);
            return response.Data.Status;
        }

        public async Task<BestillingItem> GetById(int id)
        {
            //Could be an extra servce required
            //var brandListTask = _brandService.List();
            var itemGetTask = _httpService.HttpGet<EditBestillingItemResponse>($"catalog-items/{id}");
            // Then we need WhenAll
            await Task.WhenAll(itemGetTask);
            //var brands = brandListTask.Result;
            var bestillingItem = itemGetTask.Result.Data.BestillingItem;
            //bestillingItem.CatalogBrand = brands.FirstOrDefault(b => b.Id == bestillingItem.CatalogBrandId)?.Name;
            return bestillingItem;
        }

        public async Task<List<BestillingItem>> List()
        {
            _logger.LogInformation("Fetching bestillings from API.");

            List<BestillingItem> bestillingItemList = new();

            var response = await _httpService.HttpGet<BestillingListResponse>("Bestillings");
            var bestillings = response.Data.Bestillings;
            foreach (var bestilling in bestillings)
            {
                bestillingItemList.Add(BestillingMapper.ToItem(bestilling));
            }
            return bestillingItemList;
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
                            Kommunenavns = g.Select(s => s.Kommunenavn).ToArray()
                        })
                        .ToArray();
                }
            }
        }

        public string[] GetKommunes() => _kommunes?.Select(k => k.Navn).ToArray() ?? new[] { Language.NO["NoData"] };

        public string[] GetBarneverntjenestes() => _barneverntjenestes?.Select(bt => bt.EnhetsnavnOgBydelsnavn).ToArray() ?? new[] { Language.NO["NoData"] };

        public string[] GetBarneverntjenestesByKommunenavn(string kommunenavn) =>
            _barneverntjenestes == null ? new[] { Language.NO["NoData"] }
                : string.IsNullOrEmpty(kommunenavn) ? Array.Empty<string>()
                    : _barneverntjenestes.Where(k => k.Kommunenavns.Contains(kommunenavn)).Select(k => k.EnhetsnavnOgBydelsnavn).ToArray();

        public async Task<BarnDto> GetBarnByFnr(string fnr)
        {
            HttpClient client = new();
            var result = await client.GetAsync($"https://localhost:7040/BarnByFnr/{fnr}");
            var jsonResponse = await result.Content.ReadAsStringAsync();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            var response = JsonSerializer.Deserialize<GetBarnByFnrResponse>(jsonResponse, options);

            var barn = response.barnDto;

            //var response = await _httpService.HttpGet<GetBarnByFnrResponse>($"https://localhost:7040/BarnByFnr/{fnr}");
            return barn;
        }
    }
}