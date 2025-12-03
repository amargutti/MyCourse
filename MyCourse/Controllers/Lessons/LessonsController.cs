using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Exceptions;
using MyCourse.Models.InputModels;
using MyCourse.Models.Services.Application.Lessons;
using MyCourse.Models.ViewModel.Lessons;

namespace MyCourse.Controllers.Lessons
{
    public class LessonsController : Controller
    {
        private readonly ILessonService lessonService;

        public LessonsController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        public async Task<IActionResult> Detail (int id)
        {
            ViewData["Title"] = $"Lezione {id}";
            LessonDetailViewModel lesson = await lessonService.GetLessonAsync(id);
            return View(lesson);
        }

        public IActionResult Create (int id)
        {
            ViewData["Title"] = "Nuova lezione";
            LessonCreateInputModel inputModel = new LessonCreateInputModel();
            inputModel.CourseId = id;
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LessonCreateInputModel inputModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    LessonDetailViewModel lesson = await lessonService.CreateLessonAsync(inputModel);
                    return RedirectToAction(nameof(Detail));
                }
                catch (Exception) //TODO: Implementare eccezione personalizzata
                {
                    ModelState.AddModelError(nameof(Exception), "Questo titolo esiste già");
                }
            }

            ViewData["Title"] = "Nuova Lezioni";
            return View(inputModel);
        }
    }
}
