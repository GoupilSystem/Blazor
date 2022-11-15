namespace Birk.Client.Bestilling.Utils.Constants
{
    public static class Language
    {
        public static readonly Dictionary<string, string> Eng = new()
        {
            // Main page
            { "BestillingsChoice", "Hva slags bestilling vil du registrere?" },
            { "BestillingsExplanation", "For at du skal få skjema og informasjon som er tilpasset bestillingstypen må du foreta et valg. Du kan klikke på flisene \r\nunder:" },

            { "CardNames", "Henvisning|Vedtak|Foreldre|Ema"},

            { "HenvisningTitle", "Henvisning"},
            { "HenvisningContent", "Henvisningen behandles av den inntakenheten som dekkere barnets omsorgskommune…." },
            
            { "VedtakTitle", "Bestilling pga vedtak"},
            { "VedtakContent", "Når et barn eller en ungdom står uten omsorg eller er i fare for å bli skadet i sitt eget hjem,"+
                "kan barneverntjenesten plassere barnet eller ungdommen i et beredskapshjem eller i en akuttinstitusjon." },
            
            { "ForeldreTitle", "Senter for foreldre og barn"},
            { "ForeldreContent", "Senter for foreldre og barn kan tilby utredning og hjelpetiltak for familier med sped- " +
                "og småbarn i risiko for omsorgssvikt. Det er foreldrene selv som ivaretar omsorgen for  barnet under oppholdet." },
            
            { "EmaTitle", "Plassering av ema"},
            { "EmaContent", "Omsorgssenter bla bla bla"},
            
            { "Card4Title", "Muntlig henvisning"},
            { "Card4Content", "Kan kun registreres dersom barnet ikke finnes fra før."},
            
            { "Card5Title", "Søknad om tilskudd"},
            { "Card6Title", "Søknad om annen bistand"},
            { "Card7Title", "Forespørsel om familieråd"},
            
            // Dialog
            { "CardNotImplemented", "Bare Henvisning kan brukes nå" },
            { "ApiAnswer", "Bestilling Api svarte med {0} bestillinger" },

            // Common
            { "Next", "Neste steg"},
            { "Back", "Avbryt"},
            { "Validate", "Fullfør registrering"},

            // Henvisning page

            // Logg panel
            { "Logget", "Du er logget inn som Kari Nordmann, og er i ferd med å registre ny henvisning for Ola Nordmann på 8 år (Gutt)" },
            { "Systemstatus", "Systemstatus:" },
            { "IngenFeilStatus", " Ingen kjente feil pr {0}" },
            { "MainTopPanelTitle", "Title of Main" },
            
            // Panels
            { "HenvisningPanelNames", "Kommune|Vedtak" },

            // Panel: Hvilken kommune er ansvarlig for meldingen?
            { "HenvisningKommuneStructure", "Question::0|Description::1|Label::1|Dropdown::1|Description::2|Label::2|Dropdown::2" },

            { "KommuneQuestion0", "Hvilken kommune er ansvarlig for meldingen?" },
            
            { "KommuneDescription1", "For at meldingen skal komme til riktig inntaksenhet må du velge hvilken kommune barnet bor i\r\neller kommune hvorfra meldingen er sendt." },
            { "KommuneLabel1", "Velg ansvarlig kommune" },
            { "KommuneDropdown1", "Oslo|Tønsberg|Lofoten|Drammen" },
            
            { "KommuneDescription2", "For at meldingen skal komme til riktig inntaksenhet må du velge hvilken kommune barnet bor i\r\neller kommune hvorfra meldingen er sendt." },
            { "KommuneLabel2", "Velg bydel" },
            { "KommuneDropdown2", "Gamle Oslo|Grønland|Tøyen|Sagene" },

            // Panel: Inneholder henvisningen også et gyldig vedtak? 
            { "HenvisningVedtakStructure", "Question::0|RadioGroup::0|Label::1|Label::2|Dropdown::2|Label::3|Dropdown::3|Label::4|TextBox::4" },

            { "VedtakQuestion0", "Inneholder henvisningen også et gyldig vedtak?" },
            { "VedtakRadioGroup0", "Ja|Nei" },
            
            { "VedtakLabel1", "Vedtaksdato" },
            
            { "VedtakLabel2", "Vedtaket er fattet av" },
            { "VedtakDropdown2", "Fosterhjemsplassering|Fylkesnemnd|Kommune|Domstol" },

            { "VedtakLabel3", "Vedtakshjemmel" },
            { "VedtakDropdown3", "§ 1-3 Opprettholdelse|§ 3-5 Varetekt og straffgjennomføring|§ 4-3 Undersøkelse|§ 4-4,5 Hjelpetiltak omsorg" },

            { "VedtakLabel4", "Skriv inn et kort resymé av vedtaket" },
            
            // Code generator
            {
                "MainLowPanelList",
                "<div class=\"main-low-panel-list\"><h3>List of the elements of the low panel of Main</h3>" +
                "<ol>" +
                "<li> 1st item </li>" +
                "<li> 2nd item </li>" +
                "<li> 3rd item </li>" +
                "</ol>" +
                "<p>NB! This list is actually kinda useless.</p></div>"
            },

            // Shared
            { "TestButtonContent", "Test" },
            { "BackToMainButtonContent", "Back" },
            { "BackToMainButtonComment", "Back to main page" },

            // EventCallback page
            { "EventCallbackTitle", "Title of the EventCallback page" },
            { "CaseId", "Case Id" },
            { "CaseIdPlaceholder", "Write an Id" },
            { "BackToMain", "Back" },

            // UrlLink page
            { "UrlLinkTitle", "Title of the url link page" },
            { "LinkBackToMain", "Back to main page using URL link" },
            { "CaseIdFromURL", "Case.Id passed from URL is {0}." },
            { "CaseIdFromStateContainer", "Case.Id passed from CaseStateContainer is {0}." },

            // JsCalls page
            { "JsCallsTitle", "Title of the JavaScript testing page" },
            { "TestAlertButtonContent", "Alert" },
            { "TestAlertButtonComment", "Test Alert() method" },

            // NavigationManager page
            { "NavigationTitle", "Title of the NavigationManager page" },
            { "CaseIdParameter", "CaseId passed from Parameter is {0}." },

            // Exceptions
            { "ExceptionFileIdNotFound", "File Id could not be found." },

            // User messages
            { "ModalTitle", "Title of modal" },
            { "ModalMainButton", "Confirm" },
            { "ModalSecondaryButton", "Cancel" },
            { "ModalContent", "Content of modal" },

            // Tests
            { "TestLanguage", "This is the first injected value: {0} and this is the second one: {1}." }

        };
    }
}