using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApi.Dtos;

namespace IdentityAspNetMvc.Controllers;

public class HomeController : Controller
{
    public IActionResult Home()
    {
        return View();
    }

    public IActionResult Error404(int statusCode)
    {
        return View();
    }



}
