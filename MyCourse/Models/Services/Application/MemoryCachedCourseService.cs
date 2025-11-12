using Microsoft.Extensions.Caching.Memory;
using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.Services.Application
{
    public class MemoryCachedCourseService : ICachedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;

        public MemoryCachedCourseService(ICourseService courseService, IMemoryCache memoryCache)
        {
            this.courseService = courseService;
            this.memoryCache = memoryCache;
        }

        public Task<CourseDetailViewModel> GetCourseAsync(string id)
        {
            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.GetCourseAsync(id);
            });
        }

        public Task<List<CourseViewModel>> GetCoursesAsync()
        {
            return memoryCache.GetOrCreateAsync("Courses", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                return courseService.GetCoursesAsync();
            });
        }
    }
}
