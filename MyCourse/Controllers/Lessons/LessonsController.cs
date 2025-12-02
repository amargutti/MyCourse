using Microsoft.AspNetCore.Mvc;
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
    }
}
