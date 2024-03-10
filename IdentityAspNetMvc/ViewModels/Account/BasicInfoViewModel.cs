using System.ComponentModel.DataAnnotations;

namespace IdentityAspNetMvc.ViewModels.Account
{
    public class BasicInfoViewModel
    {

        public string UserId { get; set; } = null!;

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name", Prompt = "Enter your first name", Order = 0)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name", Prompt = "Enter your last name", Order = 1)]
        [Required(ErrorMessage = "Last name is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = null!;

        [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required")]
        
        public string Email { get; set; } = null!;

        [Display(Name = "Phone (Optional)", Prompt = "Enter your phone ", Order = 3)]
        [DataType(DataType.PhoneNumber)]
       
        public string? Phone { get; set; } 

        [Display(Name = "Bio (optional) ", Prompt = "Add a short bio... ", Order = 4)]
        [DataType(DataType.MultilineText)]
        public string? Biography { get; set; }
    }
}
