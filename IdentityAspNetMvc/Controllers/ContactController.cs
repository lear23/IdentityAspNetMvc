using Microsoft.AspNetCore.Mvc;

namespace IdentityAspNetMvc.Controllers
{


    [Route("/contact")]
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
