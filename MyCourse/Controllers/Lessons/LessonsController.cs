using Microsoft.AspNetCore.Mvc;
using MyCourse.Controllers.Courses;
using MyCourse.Models.Exceptions;
using MyCourse.Models.InputModels;
using MyCourse.Models.Services.Application.Lessons;
using MyCourse.Models.ViewModel.Lessons;
using System.Threading.Tasks;

namespace MyCourse.Controllers.Lessons
{
    public class LessonsController : Controller
    {
        private readonly ILessonService lessonService;

        public LessonsController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        public async Task<IActionResult> Detail(int id)
        {
            ViewData["Title"] = $"Lezione {id}";
            LessonDetailViewModel lesson = await lessonService.GetLessonAsync(id);
            return View(lesson);
        }

        public IActionResult Create(int id)
        {
            ViewData["Title"] = "Nuova lezione";
            LessonCreateInputModel inputModel = new LessonCreateInputModel();
            inputModel.CourseId = id;
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LessonCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                LessonDetailViewModel lesson = await lessonService.CreateLessonAsync(inputModel);
                TempData["ConfirmationMessage"] = "Ok! La lezione è stata creata, aggiungi anche gli altri dati";
                return RedirectToAction(nameof(Edit), new { id = inputModel.Id});
            }

            ViewData["Title"] = "Nuova Lezioni";
            return View(inputModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Modifica Lezione";
            LessonEditInputModel editModel = await lessonService.GetLessonForEditingAsync(id);
            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LessonEditInputModel editModel)
        {
            if (ModelState.IsValid)
            {
                LessonDetailViewModel editedLesson = await lessonService.EditLessonAsync(editModel);
                TempData["ConfirmationMessage"] = "Ok, i dati della lezione sono stati inseriti correttamente! Ecco qua:";
                return RedirectToAction(nameof(Detail), new { id = editedLesson.Id });
            }

            ViewData["Title"] = "Modifica Lezione";
            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LessonDeleteInputModel inputModel)
        {
            await lessonService.DeleteLessonAsync(inputModel);
            TempData["ConfirmationMessage"] = "La lezione è stata eliminata";
            return RedirectToAction(nameof(CoursesController.Detail), "Courses", new { id = inputModel.CourseId });
        }
    }
}
