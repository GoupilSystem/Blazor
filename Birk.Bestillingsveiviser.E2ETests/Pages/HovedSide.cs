using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birk.Bestillingsveiviser.E2ETests.Pages
{
    internal class HovedSide
    {
        private IPage _page;
        private readonly ILocator _txtHenvisning;
        private readonly ILocator _btnNesteSteg;
        public HovedSide(IPage page)
        {
            _page = page;
            _txtHenvisning = _page.Locator("text=Henvisning");
            _btnNesteSteg = _page.Locator("text='Neste steg'");


        }

        public async Task ClickHenvisning() => await _txtHenvisning.First.ClickAsync();
        public async Task ClickNesteSteg() => await _btnNesteSteg.First.ClickAsync();

     
    }
}
