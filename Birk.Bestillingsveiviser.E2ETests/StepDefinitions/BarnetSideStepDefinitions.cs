using Birk.Bestillingsveiviser.E2ETests.Configuration;
using Birk.Bestillingsveiviser.E2ETests.Pages;
using SpecFlowProject.Drivers;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace Birk.Bestillingsveiviser.E2ETests.StepDefinitions
{
    [Binding]
    public class BarnetSideStepDefinitions
    {
        private readonly BarnetSide _barnPage;
        private readonly Driver _driver;
        private readonly TestConfiguration _testConfiguration;


        public BarnetSideStepDefinitions(Driver driver)
        {
            _driver = driver;
            _barnPage = new BarnetSide(_driver.Page);
            _testConfiguration = new TestConfiguration();
            
        }
        [Given(@"applikasjonen er oppe")]
        public void GivenApplikasjonenErOppe()
        {            
            _driver.Page.GotoAsync(_testConfiguration.GetUrl());

        }

        [When(@"brukeren klikker på Barnet link")]
        public async Task WhenBrukerenKlikkerPaBarnetLink()
        {
            await _barnPage.ClickBarnet();
        }

        [Then(@"lander applikasjonen på Barnet side")]
        public async Task ThenLanderApplikasjonenPaBarnetSide()
        {
            var isExist = await _barnPage.IsBarnetDetailsExists();
            isExist.Should().Be(true);

        }
    }
}
