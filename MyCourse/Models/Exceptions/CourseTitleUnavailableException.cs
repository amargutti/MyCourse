namespace MyCourse.Models.Exceptions
{
    public class CourseTitleUnavailableException : Exception
    { 
        public CourseTitleUnavailableException (string title, Exception exception) : base ($"Course title {title} existed", exception)
        {
        }
    }
}
