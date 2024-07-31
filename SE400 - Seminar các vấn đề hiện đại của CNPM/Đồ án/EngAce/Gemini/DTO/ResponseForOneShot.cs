using Newtonsoft.Json;

namespace Gemini.DTO
{
    public class ResponseForOneShot
    {
        public class Candidate
        {
            [JsonProperty("content")]
            public Content Content;

            [JsonProperty("finishReason")]
            public string FinishReason;

            [JsonProperty("index")]
            public int? Index;

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

        public class PromptFeedback
        {
            [JsonProperty("safetyRatings")]
            public List<SafetyRating> SafetyRatings;
        }

        public class Response
        {
            [JsonProperty("candidates")]
            public List<Candidate> Candidates;

            [JsonProperty("promptFeedback")]
            public PromptFeedback PromptFeedback;
        }

        public class SafetyRating
        {
            [JsonProperty("category")]
            public string Category;

            [JsonProperty("probability")]
            public string Probability;
        }
    }
}
