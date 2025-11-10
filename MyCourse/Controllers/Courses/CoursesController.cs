using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Controllers.Courses
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Catalogo Corsi";
            CourseService courseService = new CourseService();
            List<CourseViewModel> courses = courseService.GetCourses();
            return View(courses);
        }

        public IActionResult Detail(string id) {
            ViewData["Title"] = $"Corso {id}";
            CourseService courseService = new CourseService();
            CourseDetailViewModel course = CourseService.GetCourse(id);
            return View(course);
        }
    }
}
