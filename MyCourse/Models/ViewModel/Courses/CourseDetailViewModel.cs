using MyCourse.Models.Enums;
using MyCourse.Models.ValueObjects;
using MyCourse.Models.ViewModel.Courses;
using MyCourse.Models.ViewModel.Lessons;
using System.Data;

public class CourseDetailViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ImagePath { get; set; }
    public double Rating { get; set; }
    public Money FullPrice { get; set; }
    public Money CurrentPrice { get; set; }
    public string Description { get; set; }
    //TODO: Aggiungere Lezioni e durata totale corso
    public List<LessonViewModel> Lessons { get; set; } = new List<LessonViewModel>();
    

    public static CourseDetailViewModel FromDataRow(DataRow dataRow)
    {
        var courseViewModel = new CourseDetailViewModel
        {
            Title = (string)dataRow["Title"],
            Author = (string)dataRow["Author"],
            ImagePath = (string)dataRow["ImagePath"],
            Rating = (double)dataRow["Rating"],
            Description = dataRow["Description"] != null ? (string)dataRow["Description"] : "",
            FullPrice = new Money
            {
                Amount = Convert.ToDecimal(dataRow["FullPrice_Amount"]),
                Currency = Enum.Parse<Currency>((string)dataRow["FullPrice_Currency"])
            },
            CurrentPrice = new Money
            {
                Amount = Convert.ToDecimal(dataRow["CurrentPrice_Amount"]),
                Currency = Enum.Parse<Currency>((string)dataRow["CurrentPrice_Currency"])
            },
            Lessons = new List<LessonViewModel>()
        };

        return courseViewModel;
    }
}