using Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace IdentityAspNetMvc.ViewModels;

public class SignUpViewModel
{
    [Display(Name = "First Name", Prompt = "Enter your first name", Order = 0)]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Enter your last name", Order = 1)]
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email address is required")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Your email address is invalid")]
    public string Email { get; set; } = null!;
    

    [Display(Name = "Password", Prompt = "Enter your password", Order = 3)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Invalid password must be a strong password")]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 4)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(Password), ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "I agree to the Terms & Concitions", Order = 5)]
    [Required(ErrorMessage = "You have to accept the Terms & Conditions")]
    [CheckBoxRequired(ErrorMessage = "You have to accept the Terms & Conditions to proceed,")]
    public bool TermsConditions { get; set; } = false;

}
