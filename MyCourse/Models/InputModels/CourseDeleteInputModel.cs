using System.ComponentModel.DataAnnotations;

namespace MyCourse.Models.InputModels
{
    public class CourseDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}
