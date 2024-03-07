using System.ComponentModel.DataAnnotations;

namespace IdentityAspNetMvc.ViewModels.Account
{
    public class AddressViewModel
    {
        [Display(Name = "Address", Prompt = "Enter your address", Order = 0)]
        [Required(ErrorMessage = "Address is required")]
        public string Address1 { get; set; } = null!;

        [Display(Name = "Address ", Prompt = "Enter your last name", Order = 1)]
        public string? Address2 { get; set; }

        [Display(Name = "PostalCode", Prompt = "Enter your PostalCode", Order = 2)]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "PostalCode is required")]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = null!;
    }
}
