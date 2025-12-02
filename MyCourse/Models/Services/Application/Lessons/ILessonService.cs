using MyCourse.Models.ViewModel.Lessons;

namespace MyCourse.Models.Services.Application.Lessons
{
    public interface ILessonService
    {
        Task<LessonDetailViewModel> GetLessonAsync(int lessonId);
    }
}
