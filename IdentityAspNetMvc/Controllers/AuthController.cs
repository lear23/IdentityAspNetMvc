using IdentityAspNetMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAspNetMvc.Controllers;

public class AuthController : Controller
{

    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        return View();
    }


    [HttpPost]
    [Route("/signup")]
    public IActionResult SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {

        }
        return View(ModelState);
    }




    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {
        return View();
    }


    [HttpPost]
    [Route("/signin")]
    public IActionResult SignIn(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {

        }
        return View(ModelState);
    }




}
