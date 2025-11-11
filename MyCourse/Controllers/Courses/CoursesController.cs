using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Controllers.Courses
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Catalogo Corsi";
            List<CourseViewModel> courses = await courseService.GetCoursesAsync();
            return View(courses);
        }

        public async Task<IActionResult> Detail(string id) {
            ViewData["Title"] = $"Corso {id}";
            CourseDetailViewModel course = await courseService.GetCourseAsync(id);
            return View(course);
        }
    }
}
 