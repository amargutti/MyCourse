using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.Services.Application
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCoursesAsync(string search, int page);

        Task<CourseDetailViewModel> GetCourseAsync(string id);
    }
}
