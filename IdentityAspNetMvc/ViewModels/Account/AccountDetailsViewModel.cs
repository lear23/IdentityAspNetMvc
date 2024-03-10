using System.ComponentModel.DataAnnotations;

namespace IdentityAspNetMvc.ViewModels.Account
{
    public class AccountDetailsViewModel
    {
        public ProfileInfoViewModel? ProfileInfo { get; set; }
        public BasicInfoViewModel? BasicInfo { get; set; }
        public AddressViewModel? AddressModel { get; set; } 
    }
}
