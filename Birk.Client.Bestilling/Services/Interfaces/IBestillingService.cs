using Birk.Client.Bestilling.Enums;
using Birk.Client.Bestilling.Models;
using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models.Requests;

namespace Birk.Client.Bestilling.Services.Interfaces
{
    public interface IBestillingService
    {
        Task<BestillingItem> Create(CreateBestillingItemRequest createBestillingItemRequest);
        Task<BestillingItem> Edit(BestillingItem bestillingItem);
        Task<DeleteStatus> Delete(int bestillingItemId);
        Task<BestillingItem> GetById(int id);
        Task<List<BestillingItem>> List();
        Task<string[]> GetTypes();
        Task GetKommunesAndBarneverntjenestes();
        Task<BarnDto> GetBarnByFnr(string fnr);
        string[] GetKommunes(); 
        string[] GetBarneverntjenestes();
        string[] GetBarneverntjenestesByKommunenavn(string kommunenavn);
    }
}
