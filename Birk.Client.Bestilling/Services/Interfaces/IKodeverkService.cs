namespace Birk.Client.Bestilling.Services.Interfaces
{
    public interface IKodeverkService
    {
        Task<string[]> GetTypes();
        Task GetKommunesAndBvtjenestes();
        string[] GetKommunes();
        int GetKommuneIndexByBvtjeneste(string selectedBvtjeneste);
        string[] GetBvtjenetes();
        string[] GetBvtjenestesByKommunenavn(string kommunenavn);
    }
}
