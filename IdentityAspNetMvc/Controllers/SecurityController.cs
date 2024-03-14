using IdentityAspNetMvc.ViewModels.Account;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAspNetMvc.Controllers;

public class SecurityController : Controller
{

    private readonly UserManager<UserEntity> _userManager;

    public SecurityController(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
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


}
