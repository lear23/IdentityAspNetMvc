namespace IdentityAspNetMvc.ViewModels.Account;

public class AccountSecurityViewModel
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
    public ProfileInfoViewModel? ProfileInfo { get; set; }

}
