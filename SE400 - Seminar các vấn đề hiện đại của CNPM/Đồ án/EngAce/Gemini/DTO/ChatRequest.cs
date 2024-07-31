using Newtonsoft.Json;

namespace Gemini.DTO
{
    public class ChatRequest
    {
        public class Content
        {
            [JsonProperty("role")]
            public string Role { get; set; }

            [JsonProperty("parts")]
            public List<Part> Parts { get; set; }
        }

        public class Part
        {
            [JsonProperty("text")]
            public string Text { get; set; }
        }

        public class Request
        {
            [JsonProperty("contents")]
            public List<Content> Contents { get; set; }
        }
    }
}
