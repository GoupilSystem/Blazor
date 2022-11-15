using Birk.Client.Bestilling.Models.Dtos.BestillingDtoTypes;

namespace Birk.Client.Bestilling.Models
{
    public class BestillingItem
    {
        public int? AkuttTilbudDager { get; set; }
        public int? AkuttTilbudMinutter { get; set; }
        public int? AkuttTilbudTimer { get; set; }
        public int AnsvarligKommuneFk { get; set; }
        public int BarnFk { get; set; }
        public DateTime? BekreftelseSendtDato { get; set; }
        public int BestilingStatusTypeFk { get; set; }

        public int? BestillingsAnsvarlingFk { get; set; }
        public int BestillingTypeFk { get; set; }
        public DateTime? BistandKommuneOppstart { get; set; }
        public string? BistandMerknad { get; set; }
        public int? BydelFk { get; set; }
        public string? EndretAv { get; set; }
        public DateTime? EndretDato { get; set; }
        public int? EnhetBarnevernstjenesteFk { get; set; }
        public System.Boolean ErSlettet { get; set; }
        public DateTime? ErSlettetDato { get; set; }
        public string? Klan { get; set; }
        public string? KommunalSaksbehandler { get; set; }
        public string? KontaktpersonLeder { get; set; }
        public int? RefSoknadNr { get; set; }
        public string? RegAv { get; set; }
        public System.DateTime RegDato { get; set; }
        public string? StatusEndretAv { get; set; }
        public DateTime? StatusEndretDato { get; set; }
        public System.Boolean TiltaksplanForeligger { get; set; }
        public bool? VedtakForeligger { get; set; }
        public DateTime? ØnsketBistandFra { get; set; }
        public DateTime? ØnsketBistandTil { get; set; }
    }
}
