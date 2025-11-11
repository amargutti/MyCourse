using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModel.Courses;
using MyCourse.Models.ViewModel.Lessons;
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
            string query = $"SELECT Id, Title, ImagePath, Description, Author, Rating, CurrentPrice_Amount, CurrentPrice_Currency, FullPrice_Amount, FullPrice_Currency FROM Courses WHERE Id={Convert.ToInt32(id)};" +
                $"SELECT * FROM Lessons WHERE CourseId={Convert.ToInt32(id)}";


            DataSet dataSet = db.Query(query);

            DataTable courseTable = dataSet.Tables[0];
            //if (courseTable.Rows.Count != 1)
            //{
            //    throw new InvalidOperationException($"Did not return exactly 1 row for Course {id}");
            //}
            
            CourseDetailViewModel course = CourseDetailViewModel.FromDataRow(courseTable.Rows[0]);

            DataTable lessonTable = dataSet.Tables[1];

            foreach (DataRow row in lessonTable.Rows) { 
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(row);
                course.Lessons.Add(lessonViewModel);
            }

            return course;
        }

        public List<CourseViewModel> GetCourses()
        {
            string query = "SELECT Id, Title, ImagePath, Author, Rating, CurrentPrice_Amount, CurrentPrice_Currency, FullPrice_Amount, FullPrice_Currency FROM Courses";
            DataSet dataSet = db.Query(query);
            DataTable dataTable = dataSet.Tables[0];
            var courseList = new List<CourseViewModel>();
            foreach(DataRow dataRow in dataTable.Rows)
            {
                CourseViewModel courseViewModel = CourseViewModel.FromDataRow(dataRow);
                courseList.Add(courseViewModel);
            }

            return courseList;
        }
    }
}
