using System.Threading.Tasks;
using Xunit;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Birk.Client.Bestilling.Services.Interfaces;
using Moq;
using Birk.Client.Bestilling.Pages;
using MudBlazor.Services;
using MudBlazor;

namespace Birk.BestillingWeb.FunctionalTests
{
    public class BestillingTests : TestContext
    {
        [Fact]
        public void TestMethod()
        {
            // Create an instance of the test context
            using var ctx = new TestContext();

            // Register the required services for MudBlazor
            ctx.Services.AddSingleton<IDialogService, DialogService>();

            // Set up the handler for the mudPopover.connect method
            ctx.JSInterop.SetupVoid("mudPopover.connect", _ => true);

            // Perform your test actions
            // ...
        }

        [Fact]
        public async Task ShouldInitializeGUI()
        {
            // Arrange
            var bestillingServiceMock = new Mock<IKodeverkService>();
            bestillingServiceMock.Setup(s => s.GetTypes())
                                 .ReturnsAsync(new[] { "Type1", "Type2" });

            Services.AddSingleton(bestillingServiceMock.Object);
            Services.AddMudServices();
            Services.AddLogging();

            // Here, we first render the Bestilling component and call its InitializeGUI() method.
            // We then use the FindComponent() method to locate the DropdownComponent instance in the rendered output.
            // We can then use FindAll() to locate the individual<option> elements and
            // extract their inner HTML text using the InnerHtml property.
            var cut = RenderComponent<Bestilling>();

            // Act
            await cut.Instance.InitializeDropdownsAsync();

            // Assert
            Assert.Equal(new[] { "Type1", "Type2" }, cut.Instance.BestillingsTypes);
        }
    }
}
