using Birk.BestillingWeb.FunctionalTests.Models;
using Birk.Client.Bestilling.Components;
using Birk.Client.Bestilling.Services.Interfaces;
using Bunit;
using Microsoft.AspNetCore.Components;
using Moq;
using MudBlazor;
using MudBlazor.Services;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birk.BestillingWeb.FunctionalTests
{
    public class MudAutocompleteTests
    {
        private readonly Mock<IKodeverkService> _serviceMock;
        private readonly List<string> _items;

        public MudAutocompleteTests()
        {
            _items = new List<string> { "item1", "item2", "item3" };
            _serviceMock = new Mock<IKodeverkService>();
            _serviceMock.Setup(s => s.GetTypes()).ReturnsAsync(_items.ToArray());
        }

        private async Task<IEnumerable<string>> Search(string searchText)
        {
            return new[] { "item1", "item2", "item3" };
        }

        [Fact]
        public async Task AutocompleteTest()
        {
            // Create a fake service that returns a list of items
            var items = new[] { "item1", "item2", "item3" };
            var service = new Mock<IKodeverkService>();
            service.Setup(x => x.GetTypes()).ReturnsAsync(items);

            // Render the MudAutocomplete component and wait for it to load
            using var ctx = new TestContext();
            ctx.Services.AddMudServices();

            ctx.JSInterop.SetupVoid("mudPopover.connect", _ => true);

            string CssClass = "buf-dropdown";

            //var comp = ctx.RenderComponent<TestComponent>(props => props
            //    .Add(p => p.Class, CssClass));

            var comp = ctx.RenderComponent<MudAutocomplete<string>>(props => props
                .Add(p => p.Class, CssClass)
                .Add(p => p.Value, "")
                .Add(p => p.SearchFunc, Search)
                .Add(p => p.ResetValueOnEmptyText, false)
                .Add(p => p.CoerceText, false)
                .Add(p => p.CoerceValue, false));

            await comp.InvokeAsync(() => { });

            // Ensure that the component renders the correct class
            var input = comp.Find("input");
            //input.Focus();
            //Assert.True(input.ClassList.Contains(CssClass));
            await Task.Delay(5000);
            // Ensure that the component renders the correct number of items
            await input.InputAsync(new ChangeEventArgs { Value = "i" });
            await Task.Delay(1000);
            var listContainer = comp.Find(".mud-overlay");

            // Check if the list container is visible
            var isVisible = listContainer.GetAttribute("style") == "";

            // Check if the list container contains the expected number of items
            var expectedItemCount = 1; // Replace with the expected number of items
            var itemList = listContainer.QuerySelectorAll(".mud-list-item");
            var actualItemCount = itemList.Length;

            // Assert that the list container is visible and contains the expected number of items
            Assert.True(isVisible);
            Assert.Equal(expectedItemCount, actualItemCount);

            var listbox = comp.FindAll("div.mud-listbox > div");
            foreach (var item in listbox)
            {
                Console.WriteLine(item.InnerHtml);
            }
            Assert.True(listbox.Count == 3);

            //// Ensure that the component filters items correctly
            //await input.ChangeAsync("item2");
            //await comp.WaitForNextRenderAsync();
            //listbox = comp.FindAll("div.mud-listbox > div");
            //listbox.Count.Should().Be(1);
            //listbox[0].TextContent.Should().Be("item2");
        }

        [Fact]
        public async Task AutocompleteTest1()
        {
            // Arrange
            // Create a fake service that returns a list of items
            var items = new[] { "item1", "item2", "item3" };
            var service = new Mock<IKodeverkService>();
            service.Setup(x => x.GetTypes()).ReturnsAsync(items);

            // Render the MudAutocomplete component and wait for it to load
            using var ctx = new TestContext();
            ctx.Services.AddMudServices();
            ctx.JSInterop.SetupVoid("mudPopover.connect", _ => true);
            
            var comp = ctx.RenderComponent<AutocompleteParent>();

            string CssClass = "buf-dropdown";

            var input = comp.Find("input");

            await Task.Delay(5000);
            // Ensure that the component renders the correct number of items
            await input.InputAsync(new ChangeEventArgs { Value = "i" });
            //await Task.Delay(1000);

            var menu = comp.Find(".mud-popover");

            var gh = 0;

            //// Assert
            //menu.ClassList.Should().NotContain("mud-popover-open");
            //ctx.WaitForAssertion(() => ctx.FindAll(".mud-list-item").Count.Should().Be(0));

            //// Act
            //await input.FocusAsync();
            //input.Input("S");

            //// Assert
            //ctx.WaitForAssertion(() => menu.ClassList.Should().Contain("mud-popover-open"));
            //ctx.WaitForAssertion(() => ctx.FindAll(".mud-list-item").Count.Should().BeGreaterThan(0));
            //ctx.Find(".mud-list-item").Click();

            //// Assert
            //menu.ClassList.Should().NotContain("mud-popover-open");
            //input.GetAttribute("value").Should().NotBeNullOrEmpty();
        }
    }


}
