using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiTestProject.Core;

namespace WebApiTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StringGeneratorController : ControllerBase
    {
        private readonly ILogger<StringGeneratorController> _logger;
        private readonly IStringGenerator _stringGenerator;
        private readonly int _stringLength;
        const int defaultSize = 1000;

        public StringGeneratorController(ILogger<StringGeneratorController> logger, IStringGenerator stringGenerator, int stringLength = 3)
        {
            _logger = logger;
            _stringGenerator = stringGenerator;
            _stringLength = stringLength;
        }

        /// <summary>
        /// endpoint for the fixed size of 1000 && pass a parameter for dynamic page size
        /// </summary>
        /// <param name="_pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int _pageSize = defaultSize)
        {
            var list = await _stringGenerator.GetStringCombination(_stringLength, _pageSize);

            return Ok(list);
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
            var items = await _stringGenerator.GetStringCombination(_stringLength, defaultSize);

            var list = items.Skip((page - 1) * pageSize).Take(pageSize);

            var paged = new QueryResult<string>
            {
                Items = list,
                TotalItems = items.Count,
                Page = page
            };

            return Ok(paged);
        }
    }
}
