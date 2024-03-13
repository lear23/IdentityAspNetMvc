using IdentityAspNetMvc.ViewModels.Account;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAspNetMvc.Controllers
{
    public class SavedCourses : Controller
    {
        private readonly UserManager<UserEntity> _userManager;

        public SavedCourses(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Courses()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new AccountDetailsViewModel
            {
                ProfileInfo = new ProfileInfoViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!
                }
            };

            return View(viewModel);
        }
    }
}
