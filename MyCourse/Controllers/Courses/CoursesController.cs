using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.InputModels;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModel.Courses;
using System.Data;

namespace MyCourse.Controllers.Courses
{
    public class CoursesController : Controller
    {
        private readonly ICachedCourseService courseService;

        public CoursesController(ICachedCourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index(CourseListInputModel model)
        {
            ViewData["Title"] = "Catalogo Corsi";
            ListViewModel<CourseViewModel> courses = await courseService.GetCoursesAsync(model);

            CourseListViewModel viewModel = new CourseListViewModel
            {
                Courses = courses,
                Input = model
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id) {
            ViewData["Title"] = $"Corso {id}";
            CourseDetailViewModel course = await courseService.GetCourseAsync(id);
            return View(course);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Nuovo Corso";
            CourseCreateInputModel inputModel = new CourseCreateInputModel();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInputModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            CourseDetailViewModel course = await courseService.CreateCourseAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
 