using MyCourse.Models.InputModels;
using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.Services.Application
{
    public interface ICourseService
    {
        Task<ListViewModel<CourseViewModel>> GetCoursesAsync(CourseListInputModel model);

        Task<CourseDetailViewModel> GetCourseAsync(string id);
    }
}
