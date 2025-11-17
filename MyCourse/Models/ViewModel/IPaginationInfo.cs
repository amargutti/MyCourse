namespace MyCourse.Models.ViewModel
{
    public interface IPaginationInfo
    {
        int CurrentPage { get; set; }
        int TotalResults { get; set; }
        int ResultsPerPage { get; set; }
        string Search {  get; }
        string OrderBy { get; }
        bool Ascending { get; }
    }
}
