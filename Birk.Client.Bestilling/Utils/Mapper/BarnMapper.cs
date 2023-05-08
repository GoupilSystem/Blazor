using Birk.Client.Bestilling.Enums;
using Birk.Client.Bestilling.Models.Dtos;
using Birk.Client.Bestilling.Models.ViewModels;

namespace Birk.Client.Bestilling.Utils.Mapper
{
    public static class BarnMapper
    {
        public static BarnViewModel ToBarnViewModel(this BarnOgPersonDto dto)
        {
            return new BarnViewModel
            {
                Pk = dto.Pk,
                BarnTypeFk = dto.BarnTypeFk,
                BirkId = dto.BirkId,
                RegAv = dto.RegAv,
                RegDato = dto.RegDato,
                EndretAv = dto.EndretAv,
                EndretDato = dto.EndretDato,
                Fornavn = dto.Fornavn,
                Etternavn = dto.Etternavn,
                Kjønn = dto.KjønnTypeFk,
                Født = dto.Født,
                Fødselsnummer = dto.Fødselsnummer,
                Personnummer = dto.Personnummer
            };
        }
    }
}