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
