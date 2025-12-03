using System.ComponentModel.DataAnnotations;

namespace MyCourse.Models.InputModels
{
    public class LessonCreateInputModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Il titolo della lezione è obbligatorio"),
            MinLength(10, ErrorMessage = "Il titolo deve avere una lunghezza minima di {1}"),
            MaxLength(100, ErrorMessage = "Il titolo non può superare la lunghezza massima consentita di {1} caratteri"),
            RegularExpression(@"^[\w\s\.]+$", ErrorMessage = "Formato del titolo non consentito"),
            ]
        public string Title { get; set; }
    }
}
