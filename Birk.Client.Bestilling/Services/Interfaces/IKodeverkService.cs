using Birk.Client.Bestilling.Enums;
using Birk.Client.Bestilling.Models;
using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models.Requests;

namespace Birk.Client.Bestilling.Services.Interfaces
{
    public interface IKodeverkService
    {
        Task<string[]> GetTypes();
        Task GetKommunesAndBarneverntjenestes();
        string[] GetKommunes(); 
        string[] GetBarneverntjenestes();
        string[] GetBarneverntjenestesByKommunenavn(string kommunenavn);
    }
}
