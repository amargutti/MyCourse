using MyCourse.Models.Services.Application.Courses;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModel.Lessons;

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

        public Task<LessonDetailViewModel> GetLessonAsync(int lessonId)
        {
            throw new NotImplementedException();
        }
    }
}
