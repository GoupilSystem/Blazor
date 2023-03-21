using Birk.Client.Bestilling.Models.Dtos;

namespace Birk.Client.Bestilling.Services.Interfaces
{
    public interface IBarnService
    {
        Task<BarnOgPersonDto> GetBarnByFnr(string fnr);
    }
}
