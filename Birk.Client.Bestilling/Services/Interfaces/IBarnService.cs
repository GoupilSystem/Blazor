using Birk.Client.Bestilling.Models.Dtos;

namespace Birk.Client.Bestilling.Services.Interfaces
{
    public interface IBarnService
    {
        Task<BarnDto> GetBarnByFnr(string fnr);
    }
}
