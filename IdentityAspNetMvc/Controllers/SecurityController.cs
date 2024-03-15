using IdentityAspNetMvc.ViewModels.Account;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAspNetMvc.Controllers;

public class SecurityController : Controller
{

    private readonly UserManager<UserEntity> _userManager;
    private readonly AddressServices _addressServices;
    private readonly SignInManager<UserEntity> _signInManager;

    public SecurityController(UserManager<UserEntity> userManager, AddressServices addressServices, SignInManager<UserEntity> signInManager)
    {
        _userManager = userManager;
        _addressServices = addressServices;
        _signInManager = signInManager;
    }

    [HttpGet]
    [Route("/Security")]
    public async Task<IActionResult> Security()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var viewModel = new AccountSecurityViewModel
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
    public async Task<IActionResult> Security(AccountSecurityViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Si el modelo no es válido, vuelve a mostrar la vista con los errores de validación.
            return View(model);
        }

        // Aquí iría la lógica para cambiar la contraseña del usuario.
        // Por ejemplo:
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }


        return RedirectToAction("SecurityConfirmation");
    }

    [HttpGet]
    [Route("/SecurityConfirmation")] 
    public IActionResult SecurityConfirmation()
    {
        return View(); 
    }


    [HttpPost]
    [Route("/account/delete")]
    public async Task<IActionResult> Delete()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Ta bort användarens adress om den finns
        var address = await _addressServices.GetAddressAsync(user.Id);
        if (address != null)
        {
            await _addressServices.DeleteAddressAsync(user.Id);
        }

        // Ta bort användaren
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            // Misslyckades med att ta bort användaren, hantera felet här
            return RedirectToAction("DeleteFailure");
        }

        ;
        return RedirectToAction("DeleteConfirmation");
    }




    [HttpGet]
    [Route("/Deletefirmation")]
    public async Task<ActionResult> DeleteConfirmation()
    {
        await _signInManager.SignOutAsync();
        return View();
    }


    [HttpGet]
    [Route("/DeleteFailure")]
    public IActionResult DeleteFailure()
    {
        return View();
    }



}

