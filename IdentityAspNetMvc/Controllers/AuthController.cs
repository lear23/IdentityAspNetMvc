using IdentityAspNetMvc.ViewModels;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityAspNetMvc.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    #region SignuP

    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        if(_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");

        return View();
    }


    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Email);
            if (exists)
            {
                ModelState.AddModelError("AlreadyExists", "User with the same email already exists");
                ViewData["ErrorMessage"] = "User with the same email already exists";
                return View(viewModel);
            }

            var userEntity = new UserEntity
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                UserName = viewModel.Email
            };

            var result = await _userManager.CreateAsync(userEntity, viewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");
            }
        }
        return View(viewModel);
    }

    #endregion

    #region SignIn

    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");

        return View();
    }


    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Details", "Account");
            }
        }

        ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
        ViewData["ErrorMessage"] = "Incorrect email or password";
        return View(viewModel);
    }
    #endregion


    #region SignOut
    [HttpGet]
    [Route("/signOut")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Home", "Home");
    }
    #endregion

    #region External Account | Facebook


    [HttpGet]

    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallBack"));
        return new ChallengeResult( "Facebook", authProps);  
    }

    [HttpGet]
    public async Task<IActionResult> FacebookCallBack()
    {
       var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            var userEntity = new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                 
            };
            var user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }  
            if(user != null)
            {
                if(user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email) 
                {

                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;

                    await _userManager.UpdateAsync(user);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                    return RedirectToAction("Details", "Account");   
            }
         
        }
        ModelState.AddModelError("InvalidFacebookAutentication", "danger | Incorrect facebook autentication.");
        ViewData["StatusMessage"] = "danger | Incorrect facebook autentication.";
        return RedirectToAction("SignIn", "Auth");
    }


    #endregion



    #region External Account | Google

    [HttpGet]
    public IActionResult Google()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallBack"));
        return new ChallengeResult("Google", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallBack()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            var userEntity = new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
            };

            var user = await _userManager.FindByEmailAsync(userEntity.Email!);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email!);
                }
            }

            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName!;
                    user.LastName = userEntity.LastName!;
                    user.Email = userEntity.Email;

                    await _userManager.UpdateAsync(user);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);

                if (HttpContext.User != null)
                    return RedirectToAction("Details", "Account");
            }
        }

        ModelState.AddModelError("InvalidGoogleAuthentication", "danger | Incorrect Google authentication.");
        ViewData["StatusMessage"] = "danger | Incorrect Google authentication.";
        return RedirectToAction("SignIn", "Auth");
    }


    #endregion

}
