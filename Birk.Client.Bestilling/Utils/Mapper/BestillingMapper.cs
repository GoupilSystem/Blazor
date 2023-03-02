using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models;

namespace Birk.Client.Bestilling.Utils.Mapper
{
    public static class BestillingMapper
    {
        public static BestillingItem ToItem(this BestillingDto dto)
        {
            return new BestillingItem
            {
                //PK ??
                AkuttTilbudDager = dto.AkuttTilbudDager,
                AkuttTilbudMinutter = dto.AkuttTilbudMinutter,
                AkuttTilbudTimer = dto.AkuttTilbudTimer,
                AnsvarligKommuneFk = dto.AnsvarligKommuneFk,
                BarnFk = dto.BarnFk,
                BekreftelseSendtDato = dto.BekreftelseSendtDato,
                BestilingStatusTypeFk = dto.BestilingStatusTypeFk,
                BestillingsAnsvarlingFk = dto.BestillingsAnsvarlingFk,
                BestillingTypeFk = dto.BestillingTypeFk,
                BistandKommuneOppstart = dto.BistandKommuneOppstart,
                BistandMerknad = dto.BistandMerknad,
                BydelFk = dto.BydelFk,
                EndretAv = dto.EndretAv,
                EndretDato = dto.EndretDato,
                EnhetBarnevernstjenesteFk = dto.EnhetBarnevernstjenesteFk,
                ErSlettet = dto.ErSlettet,
                ErSlettetDato = dto.ErSlettetDato,
                KommunalSaksbehandler = dto.KommunalSaksbehandler,
                KontaktpersonLeder = dto.KontaktpersonLeder,
                RefSoknadNr = dto.RefSoknadNr,
                RegAv = dto.RegAv,
                RegDato = dto.RegDato,
                StatusEndretAv = dto.StatusEndretAv,
                StatusEndretDato = dto.StatusEndretDato,
                TiltaksplanForeligger = dto.TiltaksplanForeligger,
                VedtakForeligger = dto.VedtakForeligger,
                ØnsketBistandFra = dto.ØnsketBistandFra,
                ØnsketBistandTil = dto.ØnsketBistandTil
            };
        }
    }
}
