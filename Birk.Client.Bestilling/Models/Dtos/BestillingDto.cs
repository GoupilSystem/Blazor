namespace Birk.Client.Bestilling.Models.Dtos
{
    [Serializable]
    public class BestillingDto
    {
        public int Pk { get; set; }
        public int? AkuttTilbudDager { get; set; }
        public int? AkuttTilbudMinutter { get; set; }
        public int? AkuttTilbudTimer { get; set; }
        public int AnsvarligKommuneFk { get; set; }
        public int BarnFk { get; set; }
        public DateTime? BekreftelseSendtDato { get; set; }
        public BestillingDtoTypes.BestilingStatusType BestillingStatusType { get; set; }
        public int BestilingStatusTypeFk { get; set; }
        public List<BestillingDtoTypes.BestillingNBistand> BestillingNBistands { get; set; }
        public List<BestillingDtoTypes.BestillingNMedvirkningType> BestillingNMedvirkningTypes { get; set; }
        public List<BestillingDtoTypes.BestillingNÅrsakType> BestillingNÅrsakTypes { get; set; }
        public int? BestillingsAnsvarlingFk { get; set; }
        public BestillingDtoTypes.BestillingType BestillingType { get; set; }
        public int BestillingTypeFk { get; set; }
        public DateTime? BistandKommuneOppstart { get; set; }
        public string? BistandMerknad { get; set; }
        public int? BydelFk { get; set; }
        public string? EndretAv { get; set; }
        public DateTime? EndretDato { get; set; }
        public int? EnhetBarnevernstjenesteFk { get; set; }
        public bool ErSlettet { get; set; }
        public DateTime? ErSlettetDato { get; set; }
        public string? KommunalSaksbehandler { get; set; }
        public string? KontaktpersonLeder { get; set; }
        public int? RefSoknadNr { get; set; }
        public string? RegAv { get; set; }
        public DateTime RegDato { get; set; }
        public string? StatusEndretAv { get; set; }
        public DateTime? StatusEndretDato { get; set; }
        public bool TiltaksplanForeligger { get; set; }
        public bool? VedtakForeligger { get; set; }
        public DateTime? ØnsketBistandFra { get; set; }
        public DateTime? ØnsketBistandTil { get; set; }
    }

    namespace BestillingDtoTypes
    {
        [Serializable]
        public partial class BestilingStatusType
        {
            public string? Beskrivelse { get; set; }
            public int Pk { get; set; }
            public string? EndretAv { get; set; }
            public DateTime? EndretDato { get; set; }
            public DateTime GyldigFraDato { get; set; }
            public DateTime? GyldigTilDato { get; set; }
            public int? MerknadTypeFk { get; set; }
            public string RegAv { get; set; }
            public DateTime RegDato { get; set; }
            public int? Rekkefølge { get; set; }
            public string Verdi { get; set; }
        }

        [Serializable]
        public partial class BestillingNBistand
        {
            public int? BestillingFk { get; set; }
            public int Pk { get; set; }
            public DateTime? BistandKommuneOppstartDato { get; set; }
            public string? BistandMerknad { get; set; }
            public BestillingNBistandTypes.BistandType? BistandType { get; set; }
            public int? BistandTypeFk { get; set; }
            public string? EndretAv { get; set; }
            public DateTime? EndretDato { get; set; }
            public bool? ErOppstartBekreftet { get; set; }
            public string RegAv { get; set; }
            public DateTime RegDato { get; set; }
        }

        namespace BestillingNBistandTypes
        {
            [Serializable]
            public partial class BistandType
            {
                public string? Beskrivelse { get; set; }
                public int Pk { get; set; }
                public string? EndretAv { get; set; }
                public DateTime? EndretDato { get; set; }
                public DateTime GyldigFraDato { get; set; }
                public DateTime? GyldigTilDato { get; set; }
                public string RegAv { get; set; }
                public DateTime RegDato { get; set; }
                public int? Rekkefølge { get; set; }
                public int? TjenesteKategoriTypeFk { get; set; }
                public string Verdi { get; set; }
            }
        }

        [Serializable]
        public partial class BestillingNMedvirkningType
        {
            public int BestillingFk { get; set; }
            public int Pk { get; set; }
            public string? EndretAv { get; set; }
            public DateTime? EndretDato { get; set; }
            public int MedvirkningTypeFk { get; set; }
            public string RegAv { get; set; }
            public DateTime RegDato { get; set; }
        }

        [Serializable]
        public partial class BestillingNÅrsakType
        {
            public int? BestillingFk { get; set; }
            public int Pk { get; set; }
            public string? EndretAv { get; set; }
            public DateTime? EndretDato { get; set; }
            public string RegAv { get; set; }
            public DateTime RegDato { get; set; }
            public BestillingNÅrsakTypeTypes.ÅrsakType? ÅrsakType { get; set; }
            public int? ÅrsakTypeFk { get; set; }
        }

        namespace BestillingNÅrsakTypeTypes
        {
            [Serializable]
            public partial class ÅrsakType
            {
                public string? Beskrivelse { get; set; }
                public string? EndretAv { get; set; }
                public DateTime? EndretDato { get; set; }
                public DateTime GyldigFraDato { get; set; }
                public DateTime? GyldigTilDato { get; set; }
                public string RegAv { get; set; }
                public DateTime RegDato { get; set; }
                public int? Rekkefølge { get; set; }
                public string Verdi { get; set; }
                public int Pk { get; set; }
            }
        }

        [Serializable]
        public partial class BestillingType
        {
            public string? Beskrivelse { get; set; }
            public int Pk { get; set; }
            public string? EndretAv { get; set; }
            public DateTime? EndretDato { get; set; }
            public DateTime GyldigFraDato { get; set; }
            public DateTime? GyldigTilDato { get; set; }
            public string? RegAv { get; set; }
            public DateTime? RegDato { get; set; }
            public int? Rekkefølge { get; set; }
            public string Verdi { get; set; }
        }
    }

}




