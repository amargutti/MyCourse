using MyCourse.Models.InputModels;
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

        public async Task<LessonDetailViewModel> CreateLessonAsync(LessonCreateInputModel inputModel)
        {
            string title = inputModel.Title;
            int courseId = inputModel.CourseId;

            FormattableString cmd = @$"INSERT INTO Lessons (CourseId, Title) VALUES ({courseId}, '{title}');
                                    SELECT TOP 1 * FROM Lessons ORDER BY Id DESC";

            #region Query to try
            //TODO: Provare ad usare questa query SELECT * FROM Lessons WHERE Id = (SELECT MAX(Id) FROM Lessons);
            #endregion

            int lessonID = await db.QueryScalarAsync<int>(cmd);

            LessonDetailViewModel lesson = await GetLessonAsync(lessonID);

            return lesson;
        }

        public async Task DeleteLessonAsync(LessonDeleteInputModel inputModel)
        {
            int affected = await db.CommandAsync($"DELETE FROM Lessons WHERE Id = {inputModel.Id}");

        }

        public async Task<LessonDetailViewModel> EditLessonAsync(LessonEditInputModel editModel)
        {
            FormattableString cmd = @$"UPDATE Lessons SET Title = '{editModel.Title}', Description = '{editModel.Description}',
                                        Duration = '{editModel.Duration}' WHERE Id = {editModel.Id}";

            int affectedRows = await db.CommandAsync(cmd);

            LessonDetailViewModel editedLesson = await GetLessonAsync(editModel.Id);

            return editedLesson;
        }

        public async Task<LessonDetailViewModel> GetLessonAsync(int id)
        {
            FormattableString query = $"SELECT * FROM Lessons WHERE Id = {id}";

            DataSet dataSet = await db.QueryAsync(query);
            DataTable dataTable = dataSet.Tables[0];

            //TODO: Add exception for lesson not found!
            if (dataTable.Rows.Count != 1)
            {
                throw new Exception();
            }

            DataRow lessonRow = dataTable.Rows[0];

            LessonDetailViewModel viewModel = LessonDetailViewModel.FromDataRow(lessonRow);

            return viewModel;
        }

        public async Task<LessonEditInputModel> GetLessonForEditingAsync(int id)
        {
            FormattableString query = $"SELECT * FROM Lessons WHERE Id = {id}";

            DataSet dataSet = await db.QueryAsync(query);
            DataTable dataTable = dataSet.Tables[0];

            if (dataTable.Rows.Count != 1) { throw new Exception(); }

            DataRow lessonRow = dataTable.Rows[0];

            LessonEditInputModel editModel = LessonEditInputModel.FromDataRow(lessonRow);

            return editModel;
        }
    }

}
