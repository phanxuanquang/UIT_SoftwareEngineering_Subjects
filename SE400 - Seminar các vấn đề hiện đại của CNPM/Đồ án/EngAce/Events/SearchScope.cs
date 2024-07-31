using System.Text;

namespace Events
{
    public static class SearchScope
    {
        public const sbyte MaxKeywordTotalWords = 5;
        public const sbyte MaxContextTotalWords = 15;
        public static async Task<string> Search(string apiKey, bool useEnglish, string keyword, string context)
        {
            var promptBuilder = new StringBuilder();
            keyword = keyword.Trim();
            context = context.Trim();

            if (useEnglish)
            {
                promptBuilder.Append("You are an English-Vietnamese dictionary using the AI technology.");
                promptBuilder.Append($"Please explain in a very understandable way the meaning of '{keyword}'");
                promptBuilder.Append(!string.IsNullOrEmpty(context) ? $" within the context '{context}'." : string.Empty);
                promptBuilder.AppendLine("Your output must include 8 parts:");
                promptBuilder.AppendLine($"- Pronunciation and part of speech (noun, verb, adjective, adverb, idiomatic expression...) of '{keyword}'");
                promptBuilder.AppendLine($"- Definition of '{keyword}' within the provided context (if available); otherwise, provide up to the 10 most common meanings of '{keyword}' with detailed explanations");
                promptBuilder.AppendLine($"- Provide at least 5 examples of how '{keyword}' is used, along with the contexts in which it applies and other related vocabulary.");
                promptBuilder.AppendLine("- Provide at least 3 synonyms and 3 antonyms if available, and explain them in detail.");
                promptBuilder.AppendLine($"- Provide some common idioms and phrases related to '{keyword}'.");
                promptBuilder.AppendLine($"- Provide information about the origin of '{keyword}' and its derived forms to understand the word structure better.");
                promptBuilder.AppendLine($"- Provide information about the historical development of the word '{keyword}' (if available).");
                promptBuilder.AppendLine($"- Different forms of '{keyword}' such as past tense, present tense, plural form, comparative form, etc. (if available).");
                promptBuilder.AppendLine("Your output should be presented in a very understandable manner, but not overly verbose.");
            }
            else
            {
                promptBuilder.Append("Bạn là từ điển Anh-Việt siêu ưu việt ứng dụng công nghệ AI vào việc tra cứu. Nhiệm vụ của bạn là giúp tôi giải nghĩa tiếng Anh.");
                promptBuilder.AppendLine($"Nếu từ được input là một thứ vô nghĩa hoặc không thể hiểu được hoặc quá tục tĩu, bạn hãy đưa ra output là 'Không thể giải nghĩa'");
                promptBuilder.Append($"Hãy cho tôi lời giải thích của '{keyword}'");
                promptBuilder.Append(!string.IsNullOrEmpty(context) ? $" trong ngữ cảnh '{context}'." : string.Empty);
                promptBuilder.AppendLine("Nội dung output của bạn phải bao gồm 10 phần:");
                promptBuilder.AppendLine($"- Tiêu đề của output: '{keyword.ToUpper()}'");
                promptBuilder.AppendLine($"- Phiên âm và từ loại (danh từ, động từ, tính từ, trạng từ, thành ngữ...) của '{keyword}', nếu input không phải là từ vựng mà là thành ngữ thì không cần phiên âm");
                promptBuilder.AppendLine($"- Định nghĩa của '{keyword}' trong ngữ cảnh được cung cấp (nếu có), nếu không có ngữ cảnh thì cho tôi tối đa 10 nghĩa phổ biến nhất của '{keyword}' kèm lời giải thích chi tiết");
                promptBuilder.AppendLine($"- Cung cấp tối thiểu 5 ví dụ về cách áp dụng kèm hoàn cảnh áp dụng từ '{keyword}' và một số từ vựng khác có liên quan.");
                promptBuilder.AppendLine("- Cung cấp tối thiểu 3 từ đồng nghĩa và 3 từ trái nghĩa nếu có, đồng thời giải thích chi tiết về chúng.");
                promptBuilder.AppendLine($"- Cung cấp một số idiom và cụm từ phổ biến liên quan chứa từ '{keyword}'.");
                promptBuilder.AppendLine($"- Cung cấp thông tin về từ gốc và các từ phái sinh của '{keyword}' để hiểu sâu hơn về cấu trúc từ.");
                promptBuilder.AppendLine($"- Cung cấp thông tin về lịch sử hình thành của từ '{keyword}' (nếu có).");
                promptBuilder.AppendLine($"- Các dạng biến đổi của '{keyword}' được tra cứu như thì quá khứ, thì hiện tại, dạng số nhiều, dạng so sánh,... (nếu có).");
                promptBuilder.AppendLine($"- Một số fun facts vui liên quan đến '{keyword}' (nếu có).");
                promptBuilder.AppendLine("Cách trình bày output của bạn phải thật dễ hiểu và chi tiết, tuy nhiên không được quá dài dòng.");

            }

            return await Gemini.Generator.GenerateContent(apiKey, promptBuilder.ToString(), false);
        }
    }
}
