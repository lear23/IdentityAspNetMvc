using IdentityAspNetMvc.ViewModels.Account;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAspNetMvc.Controllers;

public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;




    [HttpGet]
    [Route("/account/details")]
    public IActionResult Details()
    {
        var viewModel = new AccountDetailsViewModel();
        
        return View(viewModel);
    }
}
