using MyCourse.Models.InputModels;
using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.Services.Application.Courses
{
    public interface ICourseService
    {
        Task<ListViewModel<CourseViewModel>> GetCoursesAsync(CourseListInputModel model);

        Task<CourseDetailViewModel> GetCourseAsync(int id);

        Task<List<CourseViewModel>> GetBestRatingCourses();
        Task<List<CourseViewModel>> GetMostRecentCourses();
        Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel model);
        Task<bool> IsTitleAvailable(string title, int id);
        Task<CourseEditInputModel> GetCourseForEditAsync(int id);
        Task<CourseDetailViewModel> EditCourseAsync(CourseEditInputModel model);
        Task DeleteCourseAsync(CourseDeleteInputModel model);
    }
}
