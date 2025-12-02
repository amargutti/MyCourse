using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MyCourse.Models.Exceptions;
using MyCourse.Models.InputModels;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModel.Courses;
using MyCourse.Models.ViewModel.Lessons;
using System.Data;
using System.Threading.Tasks;

namespace MyCourse.Models.Services.Application.Courses
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;
        private readonly ILogger<AdoNetCourseService> log;
        private readonly IImagePersister imagePersister;

        public AdoNetCourseService(IDatabaseAccessor db, IOptionsMonitor<CoursesOptions> coursesOptions, ILogger<AdoNetCourseService> log, IImagePersister imagePersister)
        {
            this.db = db;
            this.coursesOptions = coursesOptions;
            this.log = log;
            this.imagePersister = imagePersister;
        }

        public async Task<List<CourseViewModel>> GetBestRatingCourses()
        {
            CourseListInputModel inputModel = new CourseListInputModel(search: "", page: 1, orderby: "Rating", ascending: true, limit: 3, orderOptions: coursesOptions.CurrentValue.Order);
            ListViewModel<CourseViewModel> result = await GetCoursesAsync(inputModel);
            return result.Results;
        }

        public async Task<List<CourseViewModel>> GetMostRecentCourses()
        {
            CourseListInputModel inputModel = new CourseListInputModel
                (
                    search: "",
                    page: 1,
                    orderby: "Id",
                    ascending: false,
                    limit: 3,
                    orderOptions: coursesOptions.CurrentValue.Order
                );

            ListViewModel<CourseViewModel> result = await GetCoursesAsync(inputModel);
            return result.Results;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
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

        public async Task<ListViewModel<CourseViewModel>> GetCoursesAsync(CourseListInputModel model)
        {
            string orderby = model.OrderBy == "CurrentPrice" ? "CurrentPrice_Amount" : model.OrderBy;
            string direction = model.Ascending ? "ASC" : "DESC";

            FormattableString query = @$"SELECT Id, Title, ImagePath, Author, Rating, CurrentPrice_Amount, CurrentPrice_Currency, FullPrice_Amount, FullPrice_Currency FROM Courses WHERE Title LIKE '%{model.Search}%' ORDER BY {orderby} {direction} OFFSET {model.Offset} ROWS FETCH NEXT {model.Limit} ROWS ONLY; 
                                        SELECT COUNT (*) FROM Courses WHERE Title LIKE '%{model.Search}%'";
            DataSet dataSet = await db.QueryAsync(query);
            DataTable dataTable = dataSet.Tables[0];
            var courseList = new List<CourseViewModel>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                CourseViewModel courseViewModel = CourseViewModel.FromDataRow(dataRow);
                courseList.Add(courseViewModel);
            }

            ListViewModel<CourseViewModel> result = new ListViewModel<CourseViewModel>
            {
                Results = courseList,
                TotalCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0])
            };

            return result;
        }

        public async Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel model)
        {
            string title = model.Title;
            string author = "Mario Mariotti";

            try
            {
                FormattableString query = @$"INSERT INTO Courses (Title, Author, Description, ImagePath, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency)
                                        VALUES ('{model.Title}', '{author}', '', '/logo.svg', 0, 0, 'EUR', 0, 'EUR');
                                        SELECT TOP 1 * FROM Courses ORDER BY Id DESC;";

                int courseId = await db.QueryScalarAsync<int>(query);

                CourseDetailViewModel course = await GetCourseAsync(courseId);

                return course;
            }
            catch (SqlException ex) when (ex.Number == 2601)
            {
                throw new CourseTitleUnavailableException(title, ex);
            }
        }

        public async Task<bool> IsTitleAvailable(string title, int id)
        {
            bool titleExists = await db.QueryScalarAsync<bool>($"SELECT COUNT (*) FROM Courses WHERE Title LIKE '{title}' AND Id<>{id};");
            return !titleExists;
        }

        public async Task<CourseEditInputModel> GetCourseForEditAsync(int id)
        {
            FormattableString query = $"SELECT Id, Title, Description, ImagePath, Email, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency, RowVersion FROM Courses WHERE Id = {id};";

            DataSet dataSet = await db.QueryAsync(query);

            DataTable table = dataSet.Tables[0];

            if (table.Rows.Count < 1)
            {
                throw new CourseNotFoundException(id);
            }

            DataRow row = table.Rows[0];
            CourseEditInputModel editInputModel = CourseEditInputModel.FromDataRow(row);

            return editInputModel;
        }

        public async Task<CourseDetailViewModel> EditCourseAsync(CourseEditInputModel model)
        {
            try
            {
                string imagePath = null;
                if (model.Image != null)
                {
                    imagePath = await imagePersister.SaveCourseImage(model.Id, model.Image);
                }
                int affectedRows = await db.CommandAsync($"UPDATE Courses SET ImagePath=COALESCE('{imagePath}', 'ImagePath'), Title='{model.Title}', Description='{model.Description}', Email='{model.Email}', FullPrice_Amount={model.FullPrice.Amount}, FullPrice_Currency='{model.FullPrice.Currency}', CurrentPrice_Amount={model.CurrentPrice.Amount}, CurrentPrice_Currency='{model.CurrentPrice.Currency}' WHERE Id={model.Id} AND RowVersion={model.RowVersion};");
                if (affectedRows == 0)
                {
                    bool courseExists = await db.QueryScalarAsync<bool>($"SELECT COUNT * FROM Courses WHERE Id={model.Id}");
                    if (courseExists)
                    {
                        throw new OptimisticConcurrencyException();
                    } else
                    {
                        throw new CourseNotFoundException(model.Id);
                    }
                }
            }
            catch (SqlException exc) when (exc.Number == 2601)
            {
                throw new CourseTitleUnavailableException(model.Title, exc);
            }

            CourseDetailViewModel course = await GetCourseAsync(model.Id);
            return course;
        }
    }
}
