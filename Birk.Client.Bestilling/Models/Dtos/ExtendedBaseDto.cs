namespace Birk.Client.Bestilling.Models.Dtos
{
    public class ExtendedBaseDto : BaseDto
    {
        public string? Verdi { get; set; }
        public string? Beskrivelse { get; set; }
        public int? Rekkefølge { get; set; }
        public DateTime? GyldigFraDato { get; set; }
        public DateTime? GyldigTilDato { get; set; }
    }
}
