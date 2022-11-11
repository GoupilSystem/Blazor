using Birk.Bestillingsveiviser.E2ETests.Pages;
using SpecFlowProject.Drivers;
using System;
using TechTalk.SpecFlow;

namespace Birk.Bestillingsveiviser.E2ETests.StepDefinitions
{
    [Binding]
    public class BestillingSideStepDefinitions
    {
        private readonly Driver _driver;
        private readonly BestillingSide _mainPage;
       

        public BestillingSideStepDefinitions(Driver driver)
        {
            _driver = driver;
            _mainPage = new BestillingSide(_driver.Page);

        }

        [Given(@"en bruker åpner applikasjonen")]
        public void GivenEnBrukerApnerApplikasjonen()
        {
            _driver.Page.GotoAsync("http://localhost:88/");
        }

        [When(@"brukeren klikker på Bestilling link")]
        public async Task WhenBrukerenKlikkerPaBestillingLink()
        {
            await _mainPage.ClickBestilling();
        }

        [Then(@"lander applikasjonen på Bestilling side")]
        public async Task ThenLanderApplikasjonenPaBestillingSide()
        {
            var isExist = await _mainPage.IsBestillingDetailsExists();
            isExist.Should().Be(true);
        }


    }
}
