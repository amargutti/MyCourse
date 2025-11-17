using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.ViewModel
{
    public class HomeViewModel
    {
        public List<CourseViewModel> BestRatingCourse { get; set; }
        public List<CourseViewModel> MostRecentCourses { get; set; }

    }
}
