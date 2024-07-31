using Entities;
using Events;
using Helper;
using Microsoft.AspNetCore.Mvc;

namespace EngAce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly ILogger<DictionaryController> _logger;
        private readonly string _accessKey;

        public ChatbotController(ILogger<DictionaryController> logger)
        {
            _logger = logger;
            _accessKey = HttpContextHelper.GetAccessKey();
        }

        /// <summary>
        /// Generates an answer from the chat history and user's question
        /// </summary>
        /// <param name="request">The conversation request containing the question.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the generated answer as a string if the operation is successful,
        /// or a bad request response if the question is empty or an error occurs during the generation.
        /// </returns>
        /// <response code="200">The generated answer as a string.</response>
        /// <response code="400">The error message if the question is empty or if an exception occurs.</response>
        [HttpPost("GenerateAnswer")]
        public async Task<ActionResult<string>> GenerateAnswer([FromBody] Conversation request)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
            {
                return BadRequest("The question must not be empty");
            }

            try
            {
                var result = await ChatScope.GenerateAnswer(_accessKey, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}