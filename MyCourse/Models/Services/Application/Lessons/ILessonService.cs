using MyCourse.Models.InputModels;
using MyCourse.Models.ViewModel.Lessons;

namespace MyCourse.Models.Services.Application.Lessons
{
    public interface ILessonService
    {
        Task<LessonDetailViewModel> CreateLessonAsync(LessonCreateInputModel inputModel);
        Task<LessonDetailViewModel> GetLessonAsync(int lessonId);
    }
}
