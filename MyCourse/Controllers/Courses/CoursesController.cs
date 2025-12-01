using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Exceptions;
using MyCourse.Models.InputModels;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModel.Courses;
using System.Data;
using System.Threading.Tasks;

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
            if(ModelState.IsValid)
            {
                try
                {
                    CourseDetailViewModel course = await courseService.CreateCourseAsync(model);
                    return RedirectToAction(nameof(Index));

                } catch (CourseTitleUnavailableException)
                {
                    ModelState.AddModelError(nameof(CourseDetailViewModel.Title), "Questo titolo esiste già");
                }
            }
                ViewData["Title"] = "Nuovo Corso";
                return View(model);
        }

        public async Task<IActionResult> IsTitleAvailable(string title, int id = 0)
        {
            bool result = await courseService.IsTitleAvailable(title, id);
            return Json(result);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Modifica Corso";
            CourseEditInputModel editModel = await courseService.GetCourseForEditAsync(id);
            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseEditInputModel model)
        {
            if (ModelState.IsValid) {
                try
                {
                    CourseDetailViewModel course = await courseService.EditCourseAsync(model);
                    TempData["ConfirmationMessage"] = $"Il corso {model.Title} è stato salvato con successo!";
                    TempData["CacheListMessage"] = "Nella pagina 'Catalogo Corsi' dovrai aspettare qualche minuto per vedere le modifiche!!!";
                    return RedirectToAction(nameof(Detail), new { id = model.Id });
                }
                catch (CourseTitleUnavailableException) {
                    ModelState.AddModelError(nameof(CourseDetailViewModel.Title), "Questo titolo esiste già");
                }
                catch(OptimisticConcurrencyException)
                {
                    ModelState.AddModelError("", "Spiacenti, il salvataggio non è possibile perchè il corso è stato modificato nel mentre da un altro utente");
                }
            }

            ViewData["Title"] = "Modifica Corso";
            return View(model);
        }
    }
}
 