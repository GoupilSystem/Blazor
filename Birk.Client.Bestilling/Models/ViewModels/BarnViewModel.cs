using Birk.Client.Bestilling.Models.Dtos;

namespace Birk.Client.Bestilling.Models.ViewModels
{
    public class BarnViewModel : BarnOgPersonDto
    {
        public bool ExistsInBirk { get; set; }
    }
}
