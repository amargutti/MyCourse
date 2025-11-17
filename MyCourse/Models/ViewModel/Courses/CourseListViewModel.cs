using MyCourse.Models.InputModels;

namespace MyCourse.Models.ViewModel.Courses
{
    public class CourseListViewModel : IPaginationInfo
    {
        public ListViewModel<CourseViewModel> Courses { get; set; }
        public CourseListInputModel Input {  get; set; }

        #region Implementazione IPaginationInfo
        //Implementazione esplicita dell'interfaccia IPaginationInfo
        int IPaginationInfo.CurrentPage { get => Input.Page; set => throw new NotImplementedException(); }
        int IPaginationInfo.TotalResults { get => Courses.TotalCount; set => throw new NotImplementedException(); }
        int IPaginationInfo.ResultsPerPage { get => Input.Limit; set => throw new NotImplementedException(); }

        string IPaginationInfo.Search => Input.Search;

        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
        #endregion
    }
}
