using MyCourse.Models.InputModels;
using MyCourse.Models.ViewModel.Lessons;

namespace MyCourse.Models.Services.Application.Lessons
{
    public interface ILessonService
    {
        Task<LessonDetailViewModel> CreateLessonAsync(LessonCreateInputModel inputModel);
        Task DeleteLessonAsync(LessonDeleteInputModel inputModel);
        Task<LessonDetailViewModel> EditLessonAsync(LessonEditInputModel editModel);
        Task<LessonDetailViewModel> GetLessonAsync(int lessonId);
        Task<LessonEditInputModel> GetLessonForEditingAsync(int id);
    }
}
