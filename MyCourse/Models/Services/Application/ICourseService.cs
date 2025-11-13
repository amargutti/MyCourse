using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.Services.Application
{
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCoursesAsync(string search);

        Task<CourseDetailViewModel> GetCourseAsync(string id);
    }
}
