using MyCourse.Models.InputModels;

namespace MyCourse.Models.ViewModel.Courses
{
    public class CourseListViewModel
    {
        public ListViewModel<CourseViewModel> Courses { get; set; }
        public CourseListInputModel Input {  get; set; }
    }
}
