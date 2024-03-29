
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace SiliconWebApp.Controllers
{
    public class SiteSettingsController : Controller
    {
        public IActionResult ChangeTheme(string mode)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60),
            };
            Response.Cookies.Append("ThemeMode", mode, option);

            return Ok();
        }

        [HttpPost]

        public IActionResult CookieConsent()
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(5),
                HttpOnly = true,
                Secure = true
            };
            Response.Cookies.Append("CookieConsent","true" , option);
            return Ok();    
        }

    }   


}

