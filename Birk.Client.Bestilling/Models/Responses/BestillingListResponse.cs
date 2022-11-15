using Birk.Client.Bestilling.Models.Dtos;

namespace Birk.Client.Bestilling.Models.Responses
{
    public class BestillingListResponse
    {
        public List<BestillingDto> Bestillings { get; set; } = new();
    }
}
