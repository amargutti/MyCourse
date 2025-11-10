using MyCourse.Models.ViewModel.Courses;
using MyCourse.Models.ViewModel.Lessons;

namespace MyCourse.Models.Services.Application
{
    public class CourseService
    {
        public static CourseDetailViewModel GetCourse(string id)
        {
            var rand = new Random();
            float rating()
            {
                return (float)(rand.NextDouble() * (4 + 1));
            }

            CourseDetailViewModel viewModel = new CourseDetailViewModel
            {
                Id = Convert.ToInt32(id),
                Title = $"Corso {id}",
                Author = "Andrea Margutti",
                ImagePath = "/logo.svg",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin sagittis vehicula sapien et auctor. In vel dui et diam tincidunt rhoncus et vel leo. Nullam vitae vestibulum justo, at pretium felis. Ut interdum eros arcu, auctor eleifend nunc euismod ac. Aliquam rutrum quam non lorem fringilla aliquam in vitae mi. Quisque non tincidunt mauris. Nulla vitae imperdiet nisi. Ut volutpat at odio at congue. Aenean elementum pellentesque nisi, sed efficitur quam ultricies eget.\r\n\r\nEtiam eu dui nulla. Donec at consequat libero. Praesent vitae arcu enim. Nullam dolor purus, dapibus eget facilisis et, accumsan eu tellus. Proin imperdiet neque eget sem dictum pulvinar. Curabitur in ipsum nec lorem mattis vehicula eget eu mauris. Cras rhoncus tortor ac maximus maximus. Suspendisse elementum fermentum justo vel ornare. Ut pulvinar et purus et fermentum.\r\n\r\nUt eu odio eu tortor pulvinar tincidunt. Integer eget ligula ante. In elementum nulla vel erat tincidunt porttitor. Phasellus porttitor, ante vitae dapibus euismod, augue velit laoreet dui, vel ultrices dui odio id justo. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum dictum, mauris quis semper vestibulum, erat neque tempor augue, quis porta libero dolor quis ligula. Mauris vehicula felis sapien, vitae pulvinar magna sollicitudin quis. Praesent ultrices augue tristique, facilisis quam eu, auctor dolor. Nunc vestibulum volutpat felis nec ultrices. Morbi euismod sapien nec turpis laoreet porta. Fusce velit augue, pharetra at ultricies a, venenatis quis dolor. Vivamus id sollicitudin tortor, quis feugiat nisl. Pellentesque eleifend lectus sit amet lectus condimentum iaculis. Integer orci neque, auctor nec ligula et, consectetur congue tellus.",
                Rating = rating(),
                FullPrice = new ValueObjects.Money { Amount = 19.99m, Currency = Enums.Currency.EUR },
                CurrentPrice = new ValueObjects.Money { Amount = 15.99m, Currency = Enums.Currency.EUR },
                Lessons = new List<LessonViewModel>()
            };

            for (int i = 0; i <= 5; i++)
            {
                LessonViewModel lesson = new LessonViewModel
                {
                    Id = i,
                    Name = $"Lezione N.{i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40, 90))
                };

                viewModel.Lessons.Add(lesson);
            }

            return viewModel;
        }

        public List<CourseViewModel> GetCourses()
        {
            var rand = new Random();
            float rating()
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
