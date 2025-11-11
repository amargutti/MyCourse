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

        public IActionResult Index()
        {
            ViewData["Title"] = "Catalogo Corsi";
            List<CourseViewModel> courses = courseService.GetCourses();
            return View(courses);
        }

        public IActionResult Detail(string id) {
            ViewData["Title"] = $"Corso {id}";
            CourseDetailViewModel course = courseService.GetCourse(id);
            return View(course);
        }
    }
}
 