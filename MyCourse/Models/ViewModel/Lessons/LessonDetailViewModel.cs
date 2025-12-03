using System.Data;

namespace MyCourse.Models.ViewModel.Lessons
{
    public class LessonDetailViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }

        internal static LessonDetailViewModel FromDataRow(DataRow lessonRow)
        {
            var lessonViewModel = new LessonDetailViewModel
            {
                Id = Convert.ToInt32(lessonRow["Id"]),
                CourseId = Convert.ToInt32(lessonRow["CourseId"]),
                Title = Convert.ToString(lessonRow["Title"]),
                Description = Convert.ToString(lessonRow["Description"]),
                Duration = Convert.ToString(lessonRow["Duration"])
            };

            return lessonViewModel;
        }
    }
}
