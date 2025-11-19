using Microsoft.AspNetCore.Mvc;
using MyCourse.Controllers.Courses;
using System.ComponentModel.DataAnnotations;

namespace MyCourse.Models.InputModels
{
    public class CourseCreateInputModel
    {
        [
            Required(ErrorMessage = "Il titolo è obbligatorio"),
            MinLength(10, ErrorMessage = "Il titolo non può avere una lunghezza inferiore a {1}"),
            MaxLength(100, ErrorMessage = "Il titolo non può avere più di {1} caratteri"),
            RegularExpression(@"^[\w\s\.]+$", ErrorMessage = "Formato del titolo non consentito"),
            Remote(action: nameof(CoursesController.IsTitleAvailable), controller: "Courses", ErrorMessage = "Il Titolo già esiste")
        ]
        public string Title { get; set; }
    }
}
