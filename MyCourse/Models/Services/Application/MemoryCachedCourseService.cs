using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyCourse.Models.Options;
using MyCourse.Models.ViewModel.Courses;

namespace MyCourse.Models.Services.Application
{
    public class MemoryCachedCourseService : ICachedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;
        private readonly IOptionsMonitor<CacheTimerOptions> options;

        public MemoryCachedCourseService(ICourseService courseService, IMemoryCache memoryCache, IOptionsMonitor<CacheTimerOptions> options)
        {
            this.courseService = courseService;
            this.memoryCache = memoryCache;
            this.options = options;
        }

        public Task<CourseDetailViewModel> GetCourseAsync(string id)
        {
            //TODO: ricordati di rimuovere gli elementi dalla cache quando verrà implementata la edit

            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(options.CurrentValue.AbsoluteExpirationTimer);
                return courseService.GetCourseAsync(id);
            });
        }

        public Task<List<CourseViewModel>> GetCoursesAsync(string search, int page)
        {
            return memoryCache.GetOrCreateAsync($"Courses{search}-{page}", cacheEntry =>
            {
                cacheEntry.SetSize(2);
                cacheEntry.SetAbsoluteExpiration(options.CurrentValue.AbsoluteExpirationTimer);
                return courseService.GetCoursesAsync(search, page);
            });
        }
    }
}
