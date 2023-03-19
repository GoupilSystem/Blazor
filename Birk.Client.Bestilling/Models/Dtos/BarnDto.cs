namespace Birk.Client.Bestilling.Models.Dtos
{
    public class BarnDto
    {
        public int Pk { get; set; }

        public string? EndretAv { get; set; }
        public DateTime? EndretDato { get; set; }
        public int? ReligionTypeFk { get; set; }
        public int? TrosamfunnFk { get; set; }
        public int? HjemmespråkFk { get; set; }
        public int? VerdensregionTypeFk { get; set; }
        public int? VerdensdelTypeFk { get; set; }
        public int? OpprinnelseslandTypeFk { get; set; }
        public bool ReligionViktig { get; set; }
        public bool BarnetBeherskerEngelsk { get; set; }
        public bool BarnetTrengerTolk { get; set; }
        public bool ForeldreBeherskerEngelsk { get; set; }
        public bool ForeldreTrengerTolk { get; set; }
        public int BarnTypeFk { get; set; } = 1;
        public bool Invandrerbakgrunn { get; set; }
        public int? RefOdanr { get; set; }
        public bool? ErEnsligMindreårig { get; set; }
        public string? Agressonr { get; set; }
        public int Sikkerhetsnivå { get; set; }
        public string? ReligionMerknad { get; set; }
        public string? BirkId { get; set; }
        public int? BirkidÅr { get; set; }
        public int? BirkidSeq { get; set; }
        public DateTime? Termin { get; set; }
        public int PersonFk { get; set; }
        public bool ErArbeidssøker { get; set; }
        public int? FolkeGruppeTypeFk { get; set; }
        public bool EtnisitetViktig { get; set; }
        public string? EtnisitetMerknad { get; set; }
        public int? EtnisitetTypeFk { get; set; }
        public string? EtnisitetAnnet { get; set; }
        public string? ReligionAnnet { get; set; }
        public string? HjemmespråkAnnet { get; set; }
        public string? SpråkLandMerknad { get; set; }
        public int? NasjonalitetTypeFk { get; set; }
        public int? BarnetsSpråkFk { get; set; }
        public string? TrosamfunnAnnet { get; set; }
    }
}
