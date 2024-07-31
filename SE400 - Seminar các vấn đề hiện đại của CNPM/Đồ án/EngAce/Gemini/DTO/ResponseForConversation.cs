using Newtonsoft.Json;

namespace Gemini.DTO
{
    public class ResponseForConversation
    {
        public class Candidate
        {
            [JsonProperty("content")]
            public Content Content;

            [JsonProperty("finishReason")]
            public string FinishReason;

            [JsonProperty("index")]
            public int Index;

            [JsonProperty("safetyRatings")]
            public List<SafetyRating> SafetyRatings;
        }

        public class Content
        {
            [JsonProperty("parts")]
            public List<Part> Parts;

            [JsonProperty("role")]
            public string Role;
        }

        public class Part
        {
            [JsonProperty("text")]
            public string Text;
        }

        public class Response
        {
            [JsonProperty("candidates")]
            public List<Candidate> Candidates;

            [JsonProperty("usageMetadata")]
            public UsageMetadata UsageMetadata;
        }

        public class SafetyRating
        {
            [JsonProperty("category")]
            public string Category;

            [JsonProperty("probability")]
            public string Probability;
        }

        public class UsageMetadata
        {
            [JsonProperty("promptTokenCount")]
            public int PromptTokenCount;

            [JsonProperty("candidatesTokenCount")]
            public int CandidatesTokenCount;

            [JsonProperty("totalTokenCount")]
            public int TotalTokenCount;
        }


    }
}
