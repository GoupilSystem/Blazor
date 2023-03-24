namespace Birk.Client.Bestilling.Models.Dtos
{
    public class BarnOgPersonDto
    {
        public int Pk { get; set; }
        public int BarnTypeFk { get; set; } = 1;
        public string? BirkId { get; set; }
        public string RegAv { get; set; } = string.Empty;
        public DateTime RegDato { get; set; }
        public string? EndretAv { get; set; }
        public DateTime? EndretDato { get; set; }
        public string? Fornavn { get; set; }
        public string? Etternavn { get; set; }
        public int KjønnTypeFk { get; set; }
        public DateTime? Født { get; set; }
        public string Fødselsnummer { get; set; }
        public string Personnummer { get; set; }
    }
}
