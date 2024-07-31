using Entities;
using Entities.Enums;
using Events;
using Helper;
using Microsoft.AspNetCore.Mvc;

namespace EngAce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly string _accessKey;

        public ReviewController(ILogger<DictionaryController> logger)
        {
            _accessKey = HttpContextHelper.GetAccessKey();
        }

        /// <summary>
        /// Generates the review based on the provided content and English level
        /// </summary>
        /// <param name="content">The content for which to generate a comment.</param>
        /// <param name="englishLevel">The English level for the generated comment. Default is Intermediate.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the generated comment if the operation is successful,
        /// or an error response if the access key is invalid or an exception occurs during generation.
        /// </returns>
        /// <response code="200">The generated comment.</response>
        /// <response code="400">The error message if an error occurs during generation.</response>
        /// <response code="401">The error message if the access key is invalid.</response>
        [HttpPost("Generate")]
        [ResponseCache(Duration = QuizScope.OneHourAsCachingAge, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<ActionResult<Comment>> Generate([FromBody] string content, EnglishLevel englishLevel = EnglishLevel.Intermediate)
        {
            if (string.IsNullOrEmpty(_accessKey))
            {
                return Unauthorized("Invalid Access Key");
            }
            content = content.Trim();

            if (!GeneralHelper.IsEnglish(content))
            {
                return BadRequest("Nội dung phải là tiếng Anh");
            }

            if (GeneralHelper.GetTotalWords(content) < ReviewScope.MinTotalWords)
            {
                return BadRequest($"Bài viết phải dài tối thiểu {ReviewScope.MinTotalWords} từ.");
            }

            if (GeneralHelper.GetTotalWords(content) > ReviewScope.MaxTotalWords)
            {
                return BadRequest($"Bài viết không được dài hơn {ReviewScope.MaxTotalWords} từ.");
            }

            try
            {
                var result = await ReviewScope.GenerateReview(_accessKey, englishLevel, content);
                return Ok(result);
            }
            catch
            {
                return BadRequest("Có lỗi xảy ra! Vui lòng kiểm tra lại nội dung bài viết.");
            }
        }

        /// <summary>
        /// Generates a comment from an uploaded image based on the specified English level
        /// </summary>
        /// <param name="file">The image file from which to generate a comment.</param>
        /// <param name="englishLevel">The English level for the generated comment. Default is Intermediate.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the generated comment from the image if the operation is successful,
        /// or an error response if the access key is invalid, no image is uploaded, the image size exceeds the maximum limit, or an exception occurs during generation.
        /// </returns>
        /// <response code="200">The generated comment from the image.</response>
        /// <response code="400">The error message if no image is uploaded, the image size exceeds the maximum limit, or an error occurs during generation.</response>
        /// <response code="401">The error message if the access key is invalid.</response>
        [HttpPost("GenerateFromImage")]
        [ResponseCache(Duration = QuizScope.OneHourAsCachingAge, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<ActionResult<CommentFromImage>> GenerateFromImage(IFormFile file, EnglishLevel englishLevel = EnglishLevel.Intermediate)
        {
            if (string.IsNullOrEmpty(_accessKey))
            {
                return Unauthorized("Invalid Access Key");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("Không có ảnh nào được tải lên.");
            }

            var maxSize = 15 * 1000 * 1000;

            if (file.Length > maxSize)
            {
                return BadRequest($"Dung lượng ảnh phải nhỏ hơn {maxSize / 1024 / 1024} MB.");
            }

            using (var stream = new MemoryStream())
            {
                try
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    var result = await ReviewScope.GenerateReviewFromImage(_accessKey, englishLevel, stream);
                    return Ok(result);
                }
                catch
                {
                    return BadRequest("Có lỗi xảy ra! Vui lòng kiểm tra lại ảnh và thử lại.");
                }
            }
        }
    }
}