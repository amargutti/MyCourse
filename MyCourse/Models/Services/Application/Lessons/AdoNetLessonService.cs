using MyCourse.Models.Services.Application.Courses;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModel.Lessons;
using System.Data;

namespace MyCourse.Models.Services.Application.Lessons
{
    public class AdoNetLessonService : ILessonService
    {
        private readonly IDatabaseAccessor db;
        private readonly ILogger<AdoNetCourseService> log;

        public AdoNetLessonService(IDatabaseAccessor db, ILogger<AdoNetCourseService> log)
        {
            this.db = db;
            this.log = log;
        }

        public async Task<LessonDetailViewModel> GetLessonAsync(int id)
        {
            FormattableString query = $"SELECT * FROM Lessons WHERE Id = {id}";

            DataSet dataSet = await db.QueryAsync(query);
            DataTable dataTable = dataSet.Tables[0];

            //TODO: Add exception for lesson not found!
            if(dataTable.Rows.Count != 1)
            {
                throw new Exception();
            }

            DataRow lessonRow = dataTable.Rows[0];

            LessonDetailViewModel viewModel = LessonDetailViewModel.FromDataRow(lessonRow);

            return viewModel;
        }
    }
}
