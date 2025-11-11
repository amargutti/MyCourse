using System.Data;

namespace MyCourse.Models.ViewModel.Lessons
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }

        public static LessonViewModel FromDataRow(DataRow row)
        {
            LessonViewModel viewmodel = new LessonViewModel
            {
                Id = (int) row["Id"],
                Name = (string) row["Title"],
                Duration = Convert.ToString(row["Duration"])
            };

            return viewmodel;
        }
    }
}
