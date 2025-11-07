using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers.Courses
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return Content("Sono la pagina lista di Corsi");
        }

        public IActionResult Detail(string id) {
            return Content($"Sarò la view di dettaglio con questo {id}");
        }
    }
}
