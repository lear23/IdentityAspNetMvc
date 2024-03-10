using System.ComponentModel.DataAnnotations;

namespace IdentityAspNetMvc.ViewModels.Account
{
    public class AddressViewModel
    {
        [Display(Name = "Address", Prompt = "Enter your address")]
        [Required(ErrorMessage = "Address is required")]
        [DataType(DataType.Text)]
        public string Address1 { get; set; } = null!;

        [Display(Name = "Address ", Prompt = "Enter your second address")]
        [DataType(DataType.Text)]
        public string? Address2 { get; set; }

        [Display(Name = "PostalCode", Prompt = "Enter your PostalCode")]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "PostalCode is required")]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "City", Prompt = "Enter your city")]
        [Required(ErrorMessage = "City is required")]
        [DataType(DataType.Text)]
        public string City { get; set; } = null!;
    }
}
