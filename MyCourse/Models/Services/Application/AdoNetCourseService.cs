using Microsoft.Extensions.Options;
using MyCourse.Models.Exceptions;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModel.Courses;
using MyCourse.Models.ViewModel.Lessons;
using System.Data;
using System.Threading.Tasks;

namespace MyCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;
        private readonly ILogger<AdoNetCourseService> log;

        public AdoNetCourseService(IDatabaseAccessor db, IOptionsMonitor<CoursesOptions> coursesOptions, ILogger<AdoNetCourseService> log)
        {
            this.db = db;
            this.coursesOptions = coursesOptions;
            this.log = log;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(string id)
        {
            FormattableString query = @$"SELECT Id, Title, ImagePath, Description, Author, Rating, CurrentPrice_Amount, CurrentPrice_Currency, FullPrice_Amount, FullPrice_Currency FROM Courses WHERE Id={Convert.ToInt32(id)};
                SELECT * FROM Lessons WHERE CourseId={Convert.ToInt32(id)}";

            log.LogInformation("Query formed correctyl");

            DataSet dataSet = await db.QueryAsync(query);

            DataTable courseTable = dataSet.Tables[0];
            if (courseTable.Rows.Count != 1)
            {
                log.LogWarning("Course {id} not found!", id);
                throw new CourseNotFoundException(id);
            }

            CourseDetailViewModel course = CourseDetailViewModel.FromDataRow(courseTable.Rows[0]);

            DataTable lessonTable = dataSet.Tables[1];

            foreach (DataRow row in lessonTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(row);
                course.Lessons.Add(lessonViewModel);
            }

            return course;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync(string search)
        {
            FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, CurrentPrice_Amount, CurrentPrice_Currency, FullPrice_Amount, FullPrice_Currency FROM Courses WHERE Title LIKE '%{search}%'";
            DataSet dataSet = await db.QueryAsync(query);
            DataTable dataTable = dataSet.Tables[0];
            var courseList = new List<CourseViewModel>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                CourseViewModel courseViewModel = CourseViewModel.FromDataRow(dataRow);
                courseList.Add(courseViewModel);
            }

            return courseList;
        }
    }
}
