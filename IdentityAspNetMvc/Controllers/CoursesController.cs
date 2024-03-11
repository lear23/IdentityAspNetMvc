using Microsoft.AspNetCore.Mvc;

namespace IdentityAspNetMvc.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Courses()
        {
            return View();
        }
    }
}
