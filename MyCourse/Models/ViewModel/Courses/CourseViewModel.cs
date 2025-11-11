using MyCourse.Models.Enums;
using MyCourse.Models.ValueObjects;
using System.Data;

namespace MyCourse.Models.ViewModel.Courses
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public double Rating { get; set; }
        public Money FullPrice { get; set; }
        public Money CurrentPrice { get; set; }


        public static CourseViewModel FromDataRow(DataRow dataRow)
        {
            var courseViewModel = new CourseViewModel
            {
                Title = (string)dataRow["Title"],
                Author = (string)dataRow["Author"],
                ImagePath = (string)dataRow["ImagePath"],
                Rating = (double)dataRow["Rating"],
                FullPrice = new Money
                {
                    Amount = Convert.ToDecimal(dataRow["FullPrice_Amount"]),
                    Currency = Enum.Parse<Currency>((string)dataRow["FullPrice_Currency"])
                },
                CurrentPrice = new Money
                {
                    Amount = Convert.ToDecimal(dataRow["CurrentPrice_Amount"]),
                    Currency = Enum.Parse<Currency>((string)dataRow["CurrentPrice_Currency"])
                }
            };
            return courseViewModel;
        }
    }
}
