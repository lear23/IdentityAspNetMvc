using System.ComponentModel.DataAnnotations;

namespace IdentityAspNetMvc.ViewModels.Account
{
    public class AccountDetailsViewModel
    {
        public BasicInfoViewModel BasicInfo { get; set; } = null!;
        public AddressViewModel AddressModel { get; set; } = null!;
    }
}
