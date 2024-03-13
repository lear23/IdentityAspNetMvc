using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Infrastructure.Entities;
using IdentityAspNetMvc.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;

namespace IdentityAspNetMvc.Controllers
{
    [Authorize]
    public class SecurityController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;



        [HttpGet]
        [Route("/Security")]
        public async Task<IActionResult> Security()
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
        [HttpPost]
        [Route("/Security")]
        public async Task<IActionResult> Security(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword!, model.NewPassword!);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("Security");
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

                await _signInManager.RefreshSignInAsync(user);
                return View(viewModel);
            }

            return View("SecurityConfirmation");
        }



        //[HttpPost]
        //[Route("/Security")]
        //public async Task<IActionResult> Security(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        if (user == null)
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword!, model.NewPassword!);
        //        if (!result.Succeeded)
        //        {


        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //            return View("Security");
        //        }
        //        await _signInManager.RefreshSignInAsync(user);
        //        return View(model);

        //    }


        //    return View("SecurityConfirmation");
        //}



        //    [HttpPost]
        //    [Route("/Security")]
        //    public async Task<IActionResult> Security(ChangePasswordViewModel model)
        //    {
        //        if (string.IsNullOrEmpty(model.OldPassword))
        //        {
        //            ViewData["ErrorMessage"] = "Current password is required.";
        //            return View();
        //        }

        //        var user = await _userManager.GetUserAsync(User);
        //        if (user == null)
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        //        if (result.Succeeded)
        //        {

        //            return View("SecurityConfirmation");
        //        }
        //        else
        //        {

        //            ViewData["ErrorMessage"] = "Failed to change password.";
        //            return View("Security");
        //        }
        //    }


    }
}

