using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birk.BestillingWeb.FunctionalTests
{
    public partial class AutocompleteParent : ComponentBase
    {
        private string _selectedState;

        private readonly List<string> _states = new()
    {
        "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado",
        "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho",
        "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana",
        "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota",
        "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada",
        "New Hampshire", "New Jersey", "New Mexico", "New York",
        "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon",
        "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota",
        "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington",
        "West Virginia", "Wisconsin", "Wyoming"
    };

        private void OnStateChanged(string value)
        {
            _selectedState = value;
        }

        protected override void OnInitialized()
        {
            _selectedState = "Alabama";
            base.OnInitialized();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "autocomplete-parent");

            builder.OpenComponent<MudAutocomplete<string>>(2);
            builder.AddAttribute(3, "Label", "Select a state");
            builder.AddAttribute(4, "Placeholder", "Type to search...");
            builder.AddAttribute(5, "Items", _states);
            builder.AddAttribute(6, "ValueChanged", EventCallback.Factory.Create<string>(this, OnStateChanged));
            builder.CloseComponent();

            builder.CloseElement();
        }
    }
}
