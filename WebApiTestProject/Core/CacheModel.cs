using Microsoft.Extensions.Caching.Memory;

namespace WebApiTestProject.Core
{
    public class CacheModel
    {
        private readonly IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

        public int Get(string cacheKey)
        {
            var result = _memoryCache.Get(cacheKey);
            return Convert.ToInt32(result);
        }

        public void Add(string cacheKey, int value)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(20)
            };

            _memoryCache.Set(cacheKey, value, cacheExpiryOptions);

        }
        
        public void Delete(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);

        }
    }
}