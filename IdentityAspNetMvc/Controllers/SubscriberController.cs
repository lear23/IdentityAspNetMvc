using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApi.Dtos;

namespace IdentityAspNetMvc.Controllers;

public class SubscriberController : Controller
{
    public IActionResult Subscriber()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Subscriber(SubscriberDto model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                using var http = new HttpClient();
                var json = JsonConvert.SerializeObject(model);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.PostAsync($"https://localhost:7022/api/Subscriber?email={model.Email}", content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";


                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ViewData["Status"] = "AlreadyExists";
                }
            }
            catch
            {
                ViewData["Status"] = "ConnectionFailed";
            }
        }
        else
        {
            ViewData["status"] = "Invalid";
        }
        ModelState.Clear();
        return View();
    }


}
