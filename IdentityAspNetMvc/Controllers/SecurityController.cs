using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Infrastructure.Entities;
using IdentityAspNetMvc.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;

namespace IdentityAspNetMvc.Controllers
{
    [Authorize]
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
                return RedirectToAction("Login", "Account"); // Redirige a la página de inicio de sesión si el usuario no está autenticado
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

        //[HttpPost]
        //[Route("/Security")]
        //public async Task<IActionResult> Security(ChangePasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return RedirectToAction("Login", "Account"); // Redirige a la página de inicio de sesión si el usuario no está autenticado
        //    }

        //    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        // La contraseña se cambió con éxito, puedes redirigir a una página de éxito o a la misma página con un mensaje de éxito
        //        return RedirectToAction("Security"); // Redirige a la vista de seguridad con un mensaje de éxito si lo deseas
        //    }
        //    else
        //    {
        //        // Si la actualización de la contraseña falla, agregamos los errores al ModelState para mostrarlos en la vista
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //        return View(model);
        //    }
        //}

    }
}







//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using System.Threading.Tasks;
//using Infrastructure.Entities;
//using IdentityAspNetMvc.ViewModels.Account; // Asegúrate de importar el espacio de nombres adecuado para UserEntity

//namespace IdentityAspNetMvc.Controllers
//{
//    public class SecurityController : Controller
//    {
//        private readonly UserManager<UserEntity> _userManager;

//        public SecurityController(UserManager<UserEntity> userManager)
//        {
//            _userManager = userManager;
//        }

//        [HttpGet]
//        [Route("/Security")]
//        public async Task<IActionResult> Security()
//        {
//            // Obtener el usuario actual
//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return RedirectToAction("Login", "Account"); // Redirigir a la página de inicio de sesión si el usuario no está autenticado
//            }

//            // Crear un objeto AccountDetailsViewModel y asignarle el perfil del usuario
//            var viewModel = new AccountDetailsViewModel
//            {
//                ProfileInfo = new ProfileInfoViewModel
//                {
//                    UserName = user.UserName,
//                    Email = user.Email
//                }
//            };

//            return View(viewModel);
//        }

//        [HttpPost]
//        [Route("/Security")]
//        public async Task<IActionResult> Security(ChangePasswordViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return RedirectToAction("Login", "Account"); // Redirige a la página de inicio de sesión si el usuario no está autenticado
//            }

//            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
//            if (result.Succeeded)
//            {

//                return RedirectToAction("Details");
//            }
//            else
//            {
//                // Si la actualización de la contraseña falla, agregamos los errores al ModelState para mostrarlos en la vista
//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//                return View(model);
//            }
//        }

//    }
//}





//using Microsoft.AspNetCore.Mvc;

//namespace IdentityAspNetMvc.Controllers
//{
//    public class SecurityController : Controller
//    {

//        [Route("/Security")]
//        public IActionResult Security()

//        {
//            return View();
//        }
//    }
//}
