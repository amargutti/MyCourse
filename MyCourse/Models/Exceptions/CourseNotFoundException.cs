namespace MyCourse.Models.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException()
        {
        }

        public CourseNotFoundException(string courseId) : base($"Corso {courseId} not found!")
        {
        }
    }
}
