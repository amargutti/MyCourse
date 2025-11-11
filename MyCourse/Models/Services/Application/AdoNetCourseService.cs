using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModel.Courses;
using System.Data;

namespace MyCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;

        public AdoNetCourseService(IDatabaseAccessor db)
        {
            this.db = db;
        }

        public CourseDetailViewModel GetCourse(string id)
        {
            throw new NotImplementedException();
        }

        public List<CourseViewModel> GetCourses()
        {
            string query = "SELECT * FROM Courses";
            DataSet dataSet = db.Query(query);
            throw new NotImplementedException();
        }
    }
}
