using Birk.Client.Bestilling.Enums;
using Birk.Client.Bestilling.Models.Requests;
using Birk.Client.Bestilling.Models.Responses;
using Birk.Client.Bestilling.Models;
using Birk.Client.Bestilling.Services.Interfaces;
using Birk.Client.Bestilling.Utils.Mapper;

namespace Birk.Client.Bestilling.Services.Implementation
{
    public class BestillingService : IBestillingService
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<BestillingService> _logger;

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
    }
}
