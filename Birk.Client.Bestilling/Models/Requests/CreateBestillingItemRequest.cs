using System.ComponentModel.DataAnnotations;

namespace Birk.Client.Bestilling.Models.Requests
{
    public class CreateBestillingItemRequest
    {
        [Required(ErrorMessage = "The Name field is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Description field is required")]
        public string Description { get; set; } = string.Empty;

        // decimal(18,2)
        [Range(0.01, 1000)]
        public decimal RequiredDecimalNumber{ get; set; } = 0;

        public string RegularString { get; set; } = string.Empty;

    }
}