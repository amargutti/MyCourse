using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.Services.Application
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCoursesAsync(string search, int page, string orderby, bool ascending);

        Task<CourseDetailViewModel> GetCourseAsync(string id);
    }
}
