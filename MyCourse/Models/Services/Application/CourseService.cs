
using MyCourse.Models.ViewModel;

namespace MyCourse.Models.Services.Application
{
    public class CourseService
    {


        public List<CourseViewModel> GetCourses()
        {
            var rand = new Random();
            List<CourseViewModel> courses = new List<CourseViewModel>();

            for (int i = 0; i <= 20; i++)
            {
                var course = new CourseViewModel
                {
                    Id = i,
                    Title = $"Corso {i}",
                    Author = "Andrea Margutti",
                    ImagePath = "/logo.svg",
                    Rating = rand.Next(5),
                    FullPrice = new ValueObjects.Money { Amount = 19.99m, Currency = Enums.Currency.EUR },
                    CurrentPrice = new ValueObjects.Money { Amount = 15.99m, Currency = Enums.Currency.EUR }
                };

                courses.Add(course);
            }

            return courses;
        }
    }
}
