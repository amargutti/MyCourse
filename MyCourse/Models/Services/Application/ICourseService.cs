using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.Services.Application
{
    public interface ICourseService
    {
        List<CourseViewModel> GetCourses();

        CourseDetailViewModel GetCourse(string id);
    }
}
