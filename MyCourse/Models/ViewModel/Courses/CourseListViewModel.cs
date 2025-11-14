using MyCourse.Models.InputModels;

namespace MyCourse.Models.ViewModel.Courses
{
    public class CourseListViewModel
    {
        public List<CourseViewModel> Courses { get; set; }
        public CourseListInputModel Input {  get; set; }
    }
}
