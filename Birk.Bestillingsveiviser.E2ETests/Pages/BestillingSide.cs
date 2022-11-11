using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birk.Bestillingsveiviser.E2ETests.Pages
{
    internal class BestillingSide
    {
        private IPage _page;
        private readonly ILocator _lnkBestilling;
        private readonly ILocator _txtBestillingSide;
        public BestillingSide(IPage page)
        {
            _page = page;
            _lnkBestilling = _page.Locator("text=Bestilling");
            _txtBestillingSide = _page.Locator("text='Henvisning'");


        }

        public async Task ClickBestilling() => await _lnkBestilling.First.ClickAsync();

        public async Task<bool> IsBestillingDetailsExists() => await _txtBestillingSide.IsVisibleAsync();
    }
}
