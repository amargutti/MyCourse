using Microsoft.Extensions.Caching.Memory;

namespace MyCourse.Models.Options
{
    public class CacheTimerOptions : MemoryCacheOptions
    {
        public TimeSpan AbsoluteExpirationTimer { get; set; }
    }
}
