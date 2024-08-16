using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Net;
using WebApiTestProject.Core;

namespace WebApiTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StringGeneratorController : ControllerBase
    {
        const string CacheKey = "StringList";
        private IMemoryCache _cache;
        private MemoryCacheEntryOptions _cacheOptions;
        private readonly ILogger<StringGeneratorController> _logger;
        private readonly IStringGenerator _stringGenerator;
        private readonly int _stringLength;
        const int defaultSize = 1000;

        public StringGeneratorController(ILogger<StringGeneratorController> logger,
            IStringGenerator stringGenerator,
            IMemoryCache cache,
            int stringLength = 3)
        {
            _logger = logger;
            _stringGenerator = stringGenerator;
            _stringLength = stringLength;
            _cache = cache;

            _cacheOptions = new MemoryCacheEntryOptions()
               .SetSlidingExpiration(TimeSpan.FromHours(1))
               .SetAbsoluteExpiration(TimeSpan.FromHours(1))
               .SetPriority(CacheItemPriority.Normal);
        }

        private async Task<IEnumerable<string>> GetAllStringCombinations()
        {
            if (_cache.TryGetValue(CacheKey, out List<string> list))
            {
                return list;
            }

            list = await _stringGenerator.GetStringCombination(_stringLength, defaultSize);

            _cache.Set(CacheKey, list, _cacheOptions);

            return list;
        }

        /// <summary>
        /// endpoint for the fixed size of 1000 && pass a parameter for dynamic page size
        /// </summary>
        /// <param name="_pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int _pageSize = defaultSize)
        {
            return Ok(GetAllStringCombinations());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetPaged([FromQuery] int pageSize = 10, int page = 1)
        {
            var items = await GetAllStringCombinations();

            var list = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var paged = new QueryResult<string>
            {
                Items = list,
                TotalItems = items.Count(),
                Page = page
            };

            return Ok(paged);
        }
    }
}
