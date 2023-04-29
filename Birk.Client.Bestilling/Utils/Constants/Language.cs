﻿using System.Collections.ObjectModel;

namespace Birk.Client.Bestilling.Utils.Constants
{
    public static class Language
    {
        private static readonly Lazy<ReadOnlyDictionary<string, string>> _NO = new Lazy<ReadOnlyDictionary<string, string>>(() =>
        {
            var dictionary = new Dictionary<string, string>
            {
                // Top panel
                { "BirkId", "BIRK-ID: " },
                { "Kjønn", "Kjønn: " },
                { "Fødselsdato", "Fødselsdato: " },
                { "Alder", "Alder: " },
                
                // Panel: Velg bestillingstype
                { "BestillingstypeTitle", "Velg bestillingstype" },
            
                // Panel: Hvilken kommune er ansvarlig for bestillingen?
                { "KommuneTitle", "Hvilken kommune er ansvarlig for bestillingen?" },
                { "VelgKommuneLabel", "Velg ansvarlig kommune " },
                { "VelgBarnevernLabel", "Velg barneverntjeneste/bydel " },
                { "KontaktpersonLabel", "Kontaktperson " },
                { "KontaktpersonPlaceholder", "Skriv inn navn på kontaktperson" },
                { "TelefonLabel", "Telefon/e-post " },
                { "TelefonPlaceholder", "Skriv inn kontaktinfo" },
                { "KontaktlederLabel", "Nærmeste leder til kontaktperson" },
                { "KontaktlederPlaceholder", "Skriv inn navn på nærmeste leder" },

                // Panel: Hvem gjelder bestillingen?
                { "HvemGjelderTitle", "Hvem gjelder bestillingen?" },
                { "HvemGjelderLabel", "Søk opp barn eller mor til ufødt barn i Birk. For å søke, bruk enten" +
                    "<ul class=\"bullet-list\"><li>Fødselsnummer</li><li>D-nummer/DUF-nummer</li><li>Fornavn og etternavn</li><li>BirkId</li></ul>" },

                // Panel: Søk barn
                { "SøkBarnTitle", "Søk opp barnet i Birk" },
                { "SøkBarnLabel", "Søk opp barn eller mor til ufødt barn i Birk. Bruk fødselsnummer for å søke." +
                    "<br><b>Dersom barnet ikke er registrert fører søket deg til manuell registrering.</b>"},
                { "SøkBarnButton", "Søk" },
                { "FnrNotNumericValue", "OBS! Kun tall i dette feltet"},
                { "FnrNot11DigitsWarning", "OBS! Fødselsnummer skal ha 11 siffer"},
                { "PersonNotFoundWarning", "OBS! Det er ingen barn med dette fødselsnummeret i Birk-systemet"},
                { "UkjentFnr", "Ukjent fødselsnummer"},

                // Panel: Barnopplysning
                { "BarnInfoTitle", "Informasjon om barnet" },
                { "Fornavn", "Fornavn"},
                { "FornavnPlaceholder", "Skriv inn fornavn"},
                { "Etternavn", "Etternavn"},
                { "EtternavnPlaceholder", "Skriv inn etternavn"},
                { "Fødselsnummer", "Fødselsnummer" },
                { "FødselsnummerPlaceholder", "Skriv inn fødselsnummer" },
                { "BirkIdLabel", "BirkId"},
                { "BirkIdPlaceholder", "Ikke registrert i Birk"},
                { "UsikkertFnr", "Usikkert fødselsnummer"},
                { "Sikkerhetsnivå", "Sikkerhetsnivå" },
                { "SikkerhetnivåTypes", "Ordinært sikkerhetsnivå|Skjult adresse|Sperret adresse, fortrolig|Sperret adresse, strengt fortrolig" },
                { "InfoText", "Beskrivelse av hva du skal gjøre hvis det er skjerpet sikkerhetsnivå!" +
                    "<ul class=\"bullet-list\"><li>Melde inn i ephorte</li><li>Melde sikkerhetsbehov og hvem som skal ha tilgang</li></ul>" },
                
                // Merknad
                { "MerknadLabel", "Annen informasjon"},
                { "MerknadPlaceholder", "Skriv inn en merknad"},

                // Fullfør registrering
                { "Registrer", "Fullfør registrering"},

                // Icons as markup string
                { "RedStarTopSize8", "<i class=\"fas fa-star top size8\" style=\"color: red;\"></i>" }, // Red cannot pass as a class
                { "WarningSize8", "<i class=\"fas fa-exclamation-circle size8\"></i>" },

                // GUI
                { "NoData", "No data" },

                // HttpService problems
                { "HttpProblemTitle", "Error while fetching object(s) of type: {0}" },
                { "HttpProblemDetail", "An error occurred while making the {0} request: {1}" },

                // GUI component problems
                { "UnsupportedComponentType", "Unsupported component type: {0}" }

                //for UTVIKLING: POC data structure
                //{ "HenvisningVedtakStructure", "Question::0|RadioGroup::0|Label::1|Label::2|Dropdown::2|Label::3|Dropdown::3|Label::4|TextBox::4" },
                //{ "VedtakQuestion0", "Inneholder henvisningen også et gyldig vedtak?" },
                //{ "VedtakRadioGroup0", "Ja|Nei" },
                //{ "VedtakLabel1", "Vedtaksdato" },
                //{ "VedtakLabel2", "Vedtaket er fattet av" },
                //{ "VedtakDropdown2", "Fosterhjemsplassering|Fylkesnemnd|Kommune|Domstol" },
                //{ "VedtakLabel3", "Vedtakshjemmel" },
                //{ "VedtakDropdown3", "§ 1-3 Opprettholdelse|§ 3-5 Varetekt og straffgjennomføring|§ 4-3 Undersøkelse|§ 4-4,5 Hjelpetiltak omsorg" },
                //{ "VedtakLabel4", "Skriv inn et kort resymé av vedtaket" },
            };
            return new ReadOnlyDictionary<string, string>(dictionary);
        });

        public static ReadOnlyDictionary<string, string> NO => _NO.Value;
    }
}   