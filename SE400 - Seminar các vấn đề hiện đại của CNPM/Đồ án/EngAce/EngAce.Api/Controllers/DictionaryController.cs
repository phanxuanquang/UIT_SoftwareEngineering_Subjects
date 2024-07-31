using Events;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EngAce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<DictionaryController> _logger;
        private readonly string _accessKey;

        public DictionaryController(IMemoryCache cache, ILogger<DictionaryController> logger)
        {
            _cache = cache;
            _logger = logger;
            _accessKey = HttpContextHelper.GetAccessKey();
        }

        /// <summary>
        /// Searches for a given keyword within an optional context
        /// </summary>
        /// <param name="keyword">The keyword to search for (must be in English).</param>
        /// <param name="context">The optional context for the search (must be in English, contain the keyword, and have less than 100 words)</param>
        /// <param name="useEnglishToExplain">Indicates whether the explanation should be in English.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the search result as a string if the operation is successful,
        /// or an error response if validation fails or an exception occurs during the search.
        /// </returns>
        /// <response code="200">The search result from the cache if available.</response>
        /// <response code="201">The search result after performing the search successfully.</response>
        /// <response code="400">The error message if the input validation fails or if an error occurs during the search.</response>
        /// <response code="401">Invalid Access Key</response>
        [HttpGet("Search")]
        public async Task<ActionResult<string>> Search(string keyword, string? context, bool useEnglishToExplain = false)
        {
            if (string.IsNullOrEmpty(_accessKey))
            {
                return Unauthorized("Invalid Access Key");
            }

            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest("Không được để trống từ khóa");
            }

            context = string.IsNullOrEmpty(context) ? "" : context.Trim();
            keyword = keyword.ToLower().Trim();

            var cacheKey = $"Search-{keyword}-{context.ToLower()}-{useEnglishToExplain}";
            if (_cache.TryGetValue(cacheKey, out string cachedResult))
            {
                return Ok(cachedResult);
            }

            if (GeneralHelper.GetTotalWords(keyword) > SearchScope.MaxKeywordTotalWords)
            {
                return BadRequest($"Nội dung tra cứu chỉ chứa tối đa {SearchScope.MaxKeywordTotalWords} từ");
            }

            if (!GeneralHelper.IsEnglish(keyword))
            {
                return BadRequest("Từ khóa cần tra cứu phải là tiếng Anh");
            }

            if (!string.IsNullOrEmpty(context))
            {
                if (GeneralHelper.GetTotalWords(context) > SearchScope.MaxContextTotalWords)
                {
                    return BadRequest($"Ngữ cảnh chỉ chứa tối đa {SearchScope.MaxContextTotalWords} từ");
                }

                if (!GeneralHelper.IsEnglish(context))
                {
                    return BadRequest("Ngữ cảnh phải là tiếng Anh");
                }

                if (!context.ToLower().Contains(keyword))
                {
                    return BadRequest("Ngữ cảnh phải chứa từ khóa cần tra");
                }
            }

            try
            {
                var result = await SearchScope.Search(_accessKey, useEnglishToExplain, keyword, context);
                _cache.Set(cacheKey, result, TimeSpan.FromHours(1));
                return Created("Success", result);
            }
            catch
            {
                return BadRequest("Có lỗi xảy ra! Vui lòng kiểm tra lại.");
            }
        }
    }
}