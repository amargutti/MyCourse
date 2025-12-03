using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MyCourse.Models.InputModels
{
    public class LessonEditInputModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Duration { get; set; }

        public static LessonEditInputModel FromDataRow(DataRow row)
        {
            var model = new LessonEditInputModel
            {
                Id = (int)row["Id"],
                CourseId = (int)row["CourseId"],
                Title = (string)row["Title"],
                Description = (row["Description"] !=  DBNull.Value) ? (string)row["Description"] : "",
                Duration = (string)row["Duration"]
            };

            return model;
        }
    }
}
