using Birk.Bestillingsveiviser.E2ETests.Configuration;
using Birk.Bestillingsveiviser.E2ETests.Pages;
using SpecFlowProject.Drivers;
using System;
using TechTalk.SpecFlow;

namespace Birk.Bestillingsveiviser.E2ETests.StepDefinitions
{
    [Binding]
    public class HenvisningSideStepDefinitions
    {
        private readonly Driver _driver;
        private readonly HovedSide _mainPage;
        private readonly HenvisningSide _henvisningside;
        private readonly TestConfiguration _testConfiguration;

        public HenvisningSideStepDefinitions(Driver driver)
        {
            _driver = driver;
            _mainPage = new HovedSide(_driver.Page);
            _testConfiguration = new TestConfiguration();
            _henvisningside = new HenvisningSide(_driver.Page);

        }
        [Given(@"en bruker åpner Bestillingsveiviser")]
        public void GivenEnBrukerApnerBestillingsveiviser()
        {
            _driver.Page.GotoAsync(_testConfiguration.GetUrl());
        }

        [When(@"brukeren velger Henvisning flis")]
        public async Task WhenBrukerenVelgerHenvisningFlis()
        {
            await _mainPage.ClickHenvisning();
        }

        [When(@"klikker på Neste steg knapp")]
        public async Task WhenKlikkerPaNesteStegKnapp()
        {
            await _mainPage.ClickNesteSteg();
        }

        [Then(@"lander applikasjonen på Henvisning side")]
        public async Task ThenLanderApplikasjonenPaHenvisningSide()
        {
            
            var isExist = await _henvisningside.IsHenvisningDetailsExists();
            isExist.Should().Be(true);            
        }
        [When(@"brukeren velger kommune og bydel")]
        public async Task WhenBrukerenVelgerKommuneOgBydel()
        {
            await _henvisningside.ChooseKommune();
        }

        [When(@"klikker på Fullfør registrering")]
        public async Task WhenKlikkerPaFullforRegistrering()
        {
            await _henvisningside.ClickFullfoer();
        }

        [Then(@"får brukeren antall bestillinger")]
        public async Task ThenFarBrukerenAntallBestillinger()
        {
            
        }

    }
}
