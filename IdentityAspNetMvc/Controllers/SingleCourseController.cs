//using IdentityAspNetMvc.ViewModels.Courses;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace IdentityAspNetMvc.Controllers
//{
//    public class SingleCourseController(HttpClient http) : Controller
//    {
//        private readonly HttpClient _http = http;

//        public async Task<IActionResult> SingleCourse()
//        {

//            var viewModel = new CourseViewModel();

//            var response = await _http.GetAsync("https://localhost:7022/api/course");

//            viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync())!;

//            return View(viewModel);
//        }
//    }
//}
//using IdentityAspNetMvc.ViewModels.Courses;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace IdentityAspNetMvc.Controllers
//{
//    //[Authorize]
//    public class SingleCourseController : Controller
//    {
//        private readonly HttpClient _http;

//        public SingleCourseController(HttpClient httpClient)
//        {
//            _http = httpClient;
//        }

//        public async Task<IActionResult> SingleCourse(string courseId)
//        {
//            var viewModel = new CourseViewModel();

//            var response = await _http.GetAsync($"https://localhost:7022/api/course/{courseId}");

//            if (response.IsSuccessStatusCode)
//            {
//                var course = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync());
//                viewModel.Courses = new List<CourseModel> { course };
//                return View(viewModel);
//            }
//            else
//            {

//                return View("Error");
//            }
//        }

//    }
//}


using IdentityAspNetMvc.ViewModels.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityAspNetMvc.Controllers
{
    //[Authorize]
    public class SingleCourseController : Controller
    {
        private readonly HttpClient _http;

        public SingleCourseController(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<IActionResult> SingleCourse(string courseId)
        {
            var viewModel = new CourseViewModel();

            var response = await _http.GetAsync($"https://localhost:7022/api/course/{courseId}");

            if (response.IsSuccessStatusCode)
            {
                var course = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync());
                viewModel.Courses = new List<CourseModel> { course }; // Skapa en list med en kurs
                return View(viewModel);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
