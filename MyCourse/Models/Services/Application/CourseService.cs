
using MyCourse.Models.ViewModel;

namespace MyCourse.Models.Services.Application
{
    public class CourseService
    {


        public List<CourseViewModel> GetCourses()
        {
            var rand = new Random();
            float rating ()
            {
                return (float)(rand.NextDouble() * (4 + 1));
            }
            List<CourseViewModel> courses = new List<CourseViewModel>();

            for (int i = 0; i <= 20; i++)
            {
                var course = new CourseViewModel
                {
                    Id = i + 1,
                    Title = $"Corso {i + 1}",
                    Author = "Andrea Margutti",
                    ImagePath = "/logo.svg",
                    Rating = rating(),
                    FullPrice = new ValueObjects.Money { Amount = 19.99m, Currency = Enums.Currency.EUR },
                    CurrentPrice = new ValueObjects.Money { Amount = 15.99m, Currency = Enums.Currency.EUR }
                };

                courses.Add(course);
            }

            return courses;
        }
    }
}
