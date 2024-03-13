using System.ComponentModel.DataAnnotations;

namespace IdentityAspNetMvc.ViewModels.Account;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "The current password is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "Current Password")]
    
    public string OldPassword { get; set; } = null!;

    [Required(ErrorMessage = "The new password is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Invalid password must be a strong password")]
    public string NewPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm New Password")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmNewPassword { get; set; } = null!;
}
