using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.Services.Application
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCoursesAsync();

        Task<CourseDetailViewModel> GetCourseAsync(string id);
    }
}
