using Birk.Client.Bestilling.Enums;
using Birk.Client.Bestilling.Models.Requests;
using Birk.Client.Bestilling.Models;

namespace Birk.Client.Bestilling.Services.Interfaces
{
    public interface IBestillingService
    {
        Task<BestillingItem> Create(CreateBestillingItemRequest createBestillingItemRequest);
        Task<BestillingItem> Edit(BestillingItem bestillingItem);
        Task<DeleteStatus> Delete(int bestillingItemId);
        Task<BestillingItem> GetById(int id);
        Task<List<BestillingItem>> List();
    }
}
