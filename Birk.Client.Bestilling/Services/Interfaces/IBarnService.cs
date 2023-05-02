using Birk.Client.Bestilling.Models.ViewModels;

namespace Birk.Client.Bestilling.Services.Interfaces
{
    public interface IBarnService
    {
        Task<BarnViewModel> GetBarnByFnr(string fnr);
    }
}
