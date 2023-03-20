using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models.Responses;
using Birk.Client.Bestilling.Services.Interfaces;
using System.Text.Json;

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