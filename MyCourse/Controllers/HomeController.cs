using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModel;
using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(CacheProfileName = "Home")]
        public async Task<IActionResult> Index([FromServices] ICachedCourseService courseService)
        {
            List<CourseViewModel> bestRatingCourses = await courseService.GetBestRatingCourses();
            List<CourseViewModel> mostRecentCourses = await courseService.GetMostRecentCourses();

            HomeViewModel model = new HomeViewModel
            {
                BestRatingCourse = bestRatingCourses,
                MostRecentCourses = mostRecentCourses
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
