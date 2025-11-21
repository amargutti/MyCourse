using Microsoft.AspNetCore.Mvc;
using MyCourse.Controllers.Courses;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MyCourse.Models.InputModels
{
    public class CourseEditInputModel : IValidatableObject
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Il titolo è obbligatorio"),
            MinLength(10,  ErrorMessage = "Il titolo deve essere lungo almeno {1} caratteri"),
            MaxLength(100, ErrorMessage = "Il titolo non può essere più lungo di {1} caratteri"),
            RegularExpression(@"^[\w\s\.']+$", ErrorMessage = "Titolo non valido"),
             Remote(action: nameof(CoursesController.IsTitleAvailable), controller: "Courses", ErrorMessage = "Il titolo esiste già", AdditionalFields = "Id"),
            Display(Name = "Titolo")
            ]
        public string Title { get; set; }

        [MinLength(10, ErrorMessage = "La descrizione dev'essere di almeno {1} caratteri"),
        MaxLength(4000, ErrorMessage = "La descrizione dev'essere di massimo {1} caratteri"),
        Display(Name = "Descrizione")]
        public string Description { get; set; }

        [Display(Name = "Immagine rappresentativa")]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "L'email di contatto è obbligatoria"),
        EmailAddress(ErrorMessage = "Devi inserire un indirizzo email"),
        Display(Name = "Email di contatto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il prezzo intero è obbligatorio"),
         Display(Name = "Prezzo intero")]
        public Money FullPrice { get; set; }

        [Required(ErrorMessage = "Il prezzo corrente è obbligatorio"),
        Display(Name = "Prezzo corrente")]
        public Money CurrentPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FullPrice.Currency != CurrentPrice.Currency)
            {
                yield return new ValidationResult("Il prezzo intero deve avere la stessa valuta del prezzo corrente", new[] { nameof(FullPrice) });
            }

            if(FullPrice.Amount < CurrentPrice.Amount)
            {
                yield return new ValidationResult("Il prezzo intero deve essere maggiore o uguale al prezzo corrente", new[] { nameof(FullPrice) });
            }
        }

        public static CourseEditInputModel FromDataRow(DataRow row)
        {
            var courseEditInputModel = new CourseEditInputModel
            {
                Id = Convert.ToInt32(row["Id"]),
                Title = Convert.ToString(row["Title"]),
                Description = Convert.ToString(row["Description"]),
                ImagePath = Convert.ToString(row["ImagePath"]),
                Email = Convert.ToString(row["Email"]),
                FullPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(row["FullPrice_Currency"])),
                    Convert.ToDecimal(row["FullPrice_Amount"])
                ),
                CurrentPrice = new Money(
                    Enum.Parse<Currency>(Convert.ToString(row["CurrentPrice_Currency"])),
                    Convert.ToDecimal(row["CurrentPrice_Amount"])
                ),
            };

            return courseEditInputModel;
        }
    }
}
