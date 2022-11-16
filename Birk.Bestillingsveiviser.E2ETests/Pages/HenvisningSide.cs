using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birk.Bestillingsveiviser.E2ETests.Pages
{
    internal class HenvisningSide
    {
        private IPage _page;
        private readonly ILocator _comboBoxKommune;
        private readonly ILocator _comboBoxVedtakshjemmel;
        private readonly ILocator _userHenvisningSide;
        private readonly ILocator _btnFullfoer;
        public HenvisningSide(IPage page)
        {
            _page = page;
            _comboBoxKommune = _page.Locator(".mud-input-adornment > .mud-icon-root");
            _comboBoxVedtakshjemmel = _page.Locator("div:nth-child(7) > div > .mud-input-control > .mud-input-control-input-container > .mud-input > .mud-input-adornment > .mud-icon-root");
            _userHenvisningSide = _page.Locator("text='HENVISNING'");
            _btnFullfoer = _page.Locator("text=Fullfør");


        }

        public async Task ChooseKommune() {
            await _comboBoxKommune.First.ClickAsync();
            await _page.GetByText("Oslo").ClickAsync();

        }

        public async Task ChooseVedtakshjemmel()
        {
            await _comboBoxVedtakshjemmel.Nth(1).ClickAsync();
            await _page.GetByText("§ 1-3 Opprettholdelse").ClickAsync();
        }
        public async Task ClickFullfoer() => await _btnFullfoer.First.ClickAsync();
        public async Task<bool> IsHenvisningDetailsExists() => await _userHenvisningSide.IsVisibleAsync();
    }
}
