using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyCourse.Models.InputModels;
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

        public Task<List<CourseViewModel>> GetBestRatingCourses ()
        {
            return memoryCache.GetOrCreateAsync("BestRatingCourses", cacheEntry =>
            {
                cacheEntry.SetSize(2);
                cacheEntry.SetAbsoluteExpiration(options.CurrentValue.AbsoluteExpirationTimer);
                return courseService.GetBestRatingCourses();
            });
        }

        public Task<List<CourseViewModel>> GetMostRecentCourses()
        {
            return memoryCache.GetOrCreateAsync("MostRecentCourses", cacheEntry =>
            {
                cacheEntry.SetSize(2);
                cacheEntry.SetAbsoluteExpiration(options.CurrentValue.AbsoluteExpirationTimer);
                return courseService.GetMostRecentCourses();
            });
        }

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            //TODO: ricordati di rimuovere gli elementi dalla cache quando verrà implementata la edit

            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(options.CurrentValue.AbsoluteExpirationTimer);
                return courseService.GetCourseAsync(id);
            });
        }

        public Task<ListViewModel<CourseViewModel>> GetCoursesAsync(CourseListInputModel model)
        {
            //Metto in cache i risultati solo per le prime 5 pagine del catalogo, che reputo essere
            //le più visitate dagli utenti, e che perciò mi permettono di avere il maggior beneficio dalla cache.
            //E inoltre, metto in cache i risultati solo se l'utente non ha cercato nulla.
            //In questo modo riduco drasticamente il consumo di memoria RAM
            bool canCache = model.Page <= 5 && string.IsNullOrEmpty(model.Search);

            //Se canCache è true, sfrutto il meccanismo di caching
            if (canCache)
            {
                return memoryCache.GetOrCreateAsync($"Courses{model.Page}-{model.OrderBy}-{model.Ascending}", cacheEntry =>
                {
                    cacheEntry.SetSize(2);
                    cacheEntry.SetAbsoluteExpiration(options.CurrentValue.AbsoluteExpirationTimer);
                    return courseService.GetCoursesAsync(model);
                });
            }

            //Altrimenti uso il servizio applicativo sottostante, che recupererà sempre i valori dal database
            return courseService.GetCoursesAsync(model);
        }

        public Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel model)
        {
            return courseService.CreateCourseAsync(model);
        }

        public Task<bool> IsTitleAvailable(string title)
        {
            return courseService.IsTitleAvailable(title);
        }
    }
}
