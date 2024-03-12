using System.ComponentModel.DataAnnotations;

namespace IdentityAspNetMvc.ViewModels.Account
{
    public class AccountDetailsViewModel
    {
        public ProfileInfoViewModel ProfileInfo { get; set; } = null!;
        public BasicInfoViewModel BasicInfo { get; set; } = null!;
        public AddressViewModel AddressModel { get; set; } = null!;
        public ChangePasswordViewModel ChangePassword { get; set; } = null!;


    }
}
