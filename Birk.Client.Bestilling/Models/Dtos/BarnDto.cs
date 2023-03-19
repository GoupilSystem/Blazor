namespace Birk.Client.Bestilling.Models.Dtos
{
    public class barnDto
    {
        public int pk { get; set; }
        public string endretAv { get; set; }
        public DateTime endretDato { get; set; }
        public object religionTypeFk { get; set; }
        public object trosamfunnFk { get; set; }
        public int hjemmespråkFk { get; set; }
        public int verdensregionTypeFk { get; set; }
        public int verdensdelTypeFk { get; set; }
        public int opprinnelseslandTypeFk { get; set; }
        public bool religionViktig { get; set; }
        public bool barnetBeherskerEngelsk { get; set; }
        public bool barnetTrengerTolk { get; set; }
        public bool foreldreBeherskerEngelsk { get; set; }
        public bool foreldreTrengerTolk { get; set; }
        public int barnTypeFk { get; set; }
        public object refOdanr { get; set; }
        public object erEnsligMindreårig { get; set; }
        public object agressonr { get; set; }
        public int sikkerhetsnivå { get; set; }
        public string religionMerknad { get; set; }
        public string birkId { get; set; }
        public int birkidÅr { get; set; }
        public int birkidSeq { get; set; }
        public object termin { get; set; }
        public int personFk { get; set; }
        public bool erArbeidssøker { get; set; }
        public object folkeGruppeTypeFk { get; set; }
        public bool etnisitetViktig { get; set; }
        public string etnisitetMerknad { get; set; }
        public object etnisitetTypeFk { get; set; }
        public string etnisitetAnnet { get; set; }
        public string religionAnnet { get; set; }
        public string hjemmespråkAnnet { get; set; }
        public string språkLandMerknad { get; set; }
        public object nasjonalitetTypeFk { get; set; }
        public object barnetsSpråkFk { get; set; }
        public string trosamfunnAnnet { get; set; }
    }
}
