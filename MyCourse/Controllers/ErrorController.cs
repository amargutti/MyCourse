using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            switch (feature.Error)
            {
                case InvalidOperationException:
                    ViewData["Title"] = "Corso non trovato";
                    return View("Index");
                default:
                    ViewData["Title"] = "Errore";
                    return View();
            }
        }
    }
}
