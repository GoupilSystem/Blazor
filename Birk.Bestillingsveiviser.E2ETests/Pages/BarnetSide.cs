using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birk.Bestillingsveiviser.E2ETests.Pages
{
    internal class BarnetSide
    {
        private IPage _page;
        private readonly ILocator _lnkBarnet;
        private readonly ILocator _txtBarnetSide;
        private readonly ILocator _menuTitle;
        public BarnetSide(IPage page)
        {
            _page = page;
            _lnkBarnet = _page.Locator("text=Barnet");
            _txtBarnetSide = _page.Locator("text=Personopplysninger");
            _menuTitle = _page.Locator("text=Register bestilling");


        }

        public async Task<bool> IsApplicationRunning() => await _menuTitle.IsVisibleAsync();

        public async Task ClickBarnet() => await _lnkBarnet.First.ClickAsync();

        public async Task<bool> IsBarnetDetailsExists() => await _txtBarnetSide.IsVisibleAsync();
    }
}
