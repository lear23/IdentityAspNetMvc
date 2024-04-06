using IdentityAspNetMvc.ViewModels.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityAspNetMvc.Controllers;

[Authorize]

public class CoursesController(HttpClient httpClient) : Controller
{

    private readonly HttpClient _http = httpClient;

    public async Task<IActionResult> Courses()
    {
        var viewModel = new CourseViewModel();

        var response = await _http.GetAsync("https://localhost:7022/api/course");     

        viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;

        return View(viewModel);
    }
}
