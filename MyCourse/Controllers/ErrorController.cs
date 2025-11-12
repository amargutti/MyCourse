using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Exceptions;
using MyCourse.Models.Services.Application;

namespace MyCourse.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ErrorService errorService;

        public ErrorController(ErrorService errorService)
        {
            this.errorService = errorService;
        }

        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Exception exc = feature.Error;

            string errorPage =  errorService.getErrorPage(exc);
            
            return View(errorPage);
        }
    }
}
