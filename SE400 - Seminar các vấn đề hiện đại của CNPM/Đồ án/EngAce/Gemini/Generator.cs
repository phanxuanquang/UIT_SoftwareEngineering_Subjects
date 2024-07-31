using Gemini.DTO;
using Helper;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Gemini
{
    public static class Generator
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<string> GenerateContent(string apiKey, string query, bool useJson = true, double creativeLevel = 50, GenerativeModel model = GenerativeModel.Gemini_15_Flash)
        {
            var endpoint = GetUriWithHeadersIfAny(apiKey, model);

            var request = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = query
                            }
                        }
                    }
                },
                safetySettings = new[]
                {
                    new
                    {
                        category = "HARM_CATEGORY_DANGEROUS_CONTENT",
                        threshold = "BLOCK_NONE"
                    },
                    new
                    {
                        category = "HARM_CATEGORY_HARASSMENT",
                        threshold = "BLOCK_NONE"
                    },
                    new
                    {
                        category = "HARM_CATEGORY_HATE_SPEECH",
                        threshold = "BLOCK_NONE"
                    },
                    new
                    {
                        category = "HARM_CATEGORY_SEXUALLY_EXPLICIT",
                        threshold = "BLOCK_NONE"
                    }
                },
                generationConfig = new
                {
                    temperature = creativeLevel / 100,
                    topP = 0.8,
                    topK = 10,
                    responseMimeType = useJson ? "application/json" : "text/plain"
                }
            };

            var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(endpoint, body).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseDTO = JsonConvert.DeserializeObject<ResponseForOneShot.Response>(responseData);

            return responseDTO.Candidates[0].Content.Parts[0].Text;
        }

        public static async Task<string> GenerateResponseForConversation(string apiKey, ChatRequest.Request requestData)
        {
            var endpoint = GetUriWithHeadersIfAny(apiKey, GenerativeModel.Gemini_15_Flash);

            var body = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(endpoint, body);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<ResponseForConversation.Response>(responseData);

            return dto.Candidates[0].Content.Parts[0].Text;
        }

        public static async Task<string> GenerateReviewFromImage(string apiKey, string query, string base64Img, bool useJson = true, double creativeLevel = 50, GenerativeModel model = GenerativeModel.Gemini_15_Flash)
        {
            var endpoint = GetUriWithHeadersIfAny(apiKey, model);

            var request = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new object[]
                        {
                            new
                            {
                                text = query
                            },
                            new
                            {
                                inline_data = new
                                {
                                    mime_type = "image/jpeg",
                                    data = base64Img
                                }
                            }
                        }
                    }
                },
                safetySettings = new[]
                {
                    new
                    {
                        category = "HARM_CATEGORY_DANGEROUS_CONTENT",
                        threshold = "BLOCK_NONE"
                    },
                    new
                    {
                        category = "HARM_CATEGORY_HARASSMENT",
                        threshold = "BLOCK_NONE"
                    },
                    new
                    {
                        category = "HARM_CATEGORY_HATE_SPEECH",
                        threshold = "BLOCK_NONE"
                    },
                    new
                    {
                        category = "HARM_CATEGORY_SEXUALLY_EXPLICIT",
                        threshold = "BLOCK_NONE"
                    }
                },
                generationConfig = new
                {
                    temperature = creativeLevel / 100,
                    topP = 0.8,
                    topK = 10,
                    responseMimeType = useJson ? "application/json" : "text/plain"
                }
            };

            var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(endpoint, body).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseDTO = JsonConvert.DeserializeObject<ResponseForOneShot.Response>(responseData);

            return responseDTO.Candidates[0].Content.Parts[0].Text;
        }

        private static string GetUriWithHeadersIfAny(string accessKey, GenerativeModel model)
        {
            var modelName = GeneralHelper.GetEnumDescription(model);
            var endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/{modelName}:generateContent";

            Client.DefaultRequestHeaders.Clear();

            if (accessKey.StartsWith("AIza"))
            {
                endpoint += $"?key={accessKey}";
                return endpoint;
            }

            Client.DefaultRequestHeaders.Add("x-goog-user-project", "engace-426517");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessKey);

            return endpoint;
        }
    }
}