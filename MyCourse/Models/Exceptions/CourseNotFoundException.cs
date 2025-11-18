namespace MyCourse.Models.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        public CourseNotFoundException()
        {
        }

        public CourseNotFoundException(int courseId) : base($"Corso {courseId} not found!")
        {
        }
    }
}
