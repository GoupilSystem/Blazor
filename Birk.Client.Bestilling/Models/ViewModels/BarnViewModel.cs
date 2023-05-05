using Birk.Client.Bestilling.Enums;
using Birk.Client.Bestilling.Models.Dtos;

namespace Birk.Client.Bestilling.Models.ViewModels
{
    public class BarnViewModel : BarnOgPersonDto
    {
        public SøkStatus SøkStatus { get; set; }
    }
}
