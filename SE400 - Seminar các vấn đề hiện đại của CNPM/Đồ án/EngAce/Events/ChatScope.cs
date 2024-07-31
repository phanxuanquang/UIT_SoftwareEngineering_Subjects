using Entities;
using Gemini;
using System.Text;
using static Gemini.DTO.ChatRequest;

namespace Events
{
    public static class ChatScope
    {
        public static async Task<string> GenerateAnswer(string apiKey, Conversation conversation)
        {
            conversation.ChatHistory.InsertRange(0, InitPrompts());

            var request = new Request
            {
                Contents = conversation.ChatHistory
                .AsParallel()
                .AsOrdered()
                .Select(message => new Content
                {
                    Role = message.FromUser ? "user" : "model",
                    Parts = new List<Part>
                    {
                        new Part
                        {
                            Text = message.Message.Trim(),
                        }
                    }
                })
                .ToList()
            };

            var question = new Content
            {
                Role = "user",
                Parts = new List<Part>
                {
                    new Part
                    {
                        Text = conversation.Question.Trim()
                    }
                }
            };

            request.Contents.Add(question);

            return await Generator.GenerateResponseForConversation(apiKey, request);
        }

        private static List<Conversation.History> InitPrompts()
        {
            var promptBuilder = new StringBuilder();

            promptBuilder.AppendLine("Bạn là EngAce, một trợ lý AI được tạo ra bởi Phan Xuân Quang và Bùi Minh Tuấn với mục tiêu hỗ trợ tôi học tiếng Anh một cách hiệu quả. ");
            promptBuilder.Append("Bạn chính là mentor của tôi trong hành trình học tiếng Anh của tôi, vậy nên hãy thể hiện bạn là một người mentor tuyệt vời.");
            promptBuilder.AppendLine("Bạn chỉ được phép trả lời những câu hỏi liên quan đến việc học tiếng Anh, ngoài ra không được phép trả lời. ");
            promptBuilder.Append("Nếu bạn cảm thấy câu hỏi của tôi không rõ ràng thì hãy hỏi tôi để làm rõ ý định của câu hỏi, và bạn chỉ được phép trả lời khi đã thực sự hiểu câu hỏi của tôi.");
            promptBuilder.AppendLine("Câu trả lời của bạn phải ngắn gọn, không dài dòng, và thật dễ hiểu, bạn cũng có thể cung cấp một số ví dụ minh họa nếu cần thiết. ");
            promptBuilder.Append("Cách nói chuyện của bạn phải thân thiện và mang cảm giác gần gũi, bởi vì bạn cũng chính là người đồng hành với tôi trong quá trình tôi học tiếng Anh.");
            promptBuilder.AppendLine("Nếu bạn hiểu lời nói của tôi thì hãy nói 'Bắt đầu', và chúng ta sẽ bắt đầu cuộc thảo luận.");

            var prompt = new Conversation.History()
            {
                FromUser = true,
                Message = promptBuilder.ToString()
            };

            var botReply = new Conversation.History()
            {
                FromUser = false,
                Message = "Bắt đầu!"
            };

            return new List<Conversation.History>() { prompt, botReply };
        }
    }
}
