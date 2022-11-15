
using Birk.Bestillingsveiviser.E2ETests.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.Drivers
{
    public class Driver : IDisposable

    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;
        private readonly TestConfiguration _testConfiguration;
        private readonly bool headlessValue;
        private readonly int slowMoValue;
        public Driver()
        {
            _testConfiguration = new TestConfiguration();
            headlessValue = _testConfiguration.GetHeadlessValue();
            slowMoValue = _testConfiguration.GetSlowMoValue();
            _page = InitializePlaywrite();

        }
        public IPage Page => _page.Result;  
        

        public void Dispose()
        {
            _browser?.CloseAsync();
        }

        private async Task<IPage> InitializePlaywrite()
        {         
            var playwright = await Playwright.CreateAsync();
            /*
            _browser = await playwright.Chromium.LaunchAsync();
            
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headlessValue,
                SlowMo = slowMoValue
            });
            */
            _browser = await playwright.Firefox.LaunchAsync();

            _browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headlessValue,
                SlowMo = slowMoValue
            });

            return await _browser.NewPageAsync();

        }
        //driver pattern to drive your automation code



    }
}
