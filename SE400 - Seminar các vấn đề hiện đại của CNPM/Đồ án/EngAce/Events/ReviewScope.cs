using Entities;
using Entities.Enums;
using Gemini;
using Helper;
using Newtonsoft.Json;
using System.Text;

namespace Events
{
    public static class ReviewScope
    {
        public const short MinTotalWords = 30;
        public const short MaxTotalWords = 300;
        public static async Task<Comment> GenerateReview(string apiKey, EnglishLevel level, string content)
        {
            var promptBuilder = new StringBuilder();
            var userLevel = GeneralHelper.GetEnumDescription(level);
            var model = GeneralHelper.GetTotalWords(content) <= (MaxTotalWords / 2) ? GenerativeModel.Gemini_15_Flash : GenerativeModel.Gemini_15_Pro;

            promptBuilder.Append("Bạn là một giáo viên tiếng Anh với hơn 20 năm kinh nghiệm giảng dạy, đồng thời đang làm việc tại một trung tâm dạy IELTS lớn. ");
            promptBuilder.Append($"Trình độ tiếng Anh của tôi theo tiêu chuẩn CEFR là '{userLevel}'. ");
            promptBuilder.AppendLine("Bạn hãy đọc phân tích bài viết của tôi rồi cho nhận xét và chỉnh sửa lại để nó tốt hơn. ");
            promptBuilder.AppendLine("Output phải bao gồm 2 phần như sau:");
            promptBuilder.AppendLine("- GeneralComment: Nhận xét chung bằng tiếng Việt cho cả bài viết. Nhận xét của bạn phải dựa trên nội dung chính của bài viết và trình độ tiếng Anh của tôi. Nội dung nhận xét phải bao gồm: Phát hiện lỗi chính tả và nêu cách sửa, phát hiện lỗi ngữ pháp và giải thích cho từng lỗi, đề xuất cách thay thế từ ngữ phù hợp hơn, phân tích phong cách viết và đề xuất cách viết phù hợp với ngữ cảnh và đối tượng tùy vào từng câu trong bài viết, phát hiện lỗi logic và đề xuất cách sửa để nội dung mạch lạc hơn. Bạn cũng phải giải thích chi tiết cho các gợi ý sửa chữa để tôi hiểu rõ và áp dụng hiệu quả.");
            promptBuilder.AppendLine("- ImprovedContent: Bài viết được chỉnh sửa sau khi áp dụng những sửa chữa trong phần GeneralComment, nhớ highlight những đoạn được chỉnh sửa bằng cặp dấu **. Bạn tuyệt đối không được tự ý thay đổi nội dung chính của bài viết, và bài viết sau khi được chỉnh sửa không được phép dài hơn 1.5 lần bài viết ban đầu.");
            promptBuilder.AppendLine("Output phải là một JSON object tương ứng với class C# sau: ");
            promptBuilder.AppendLine("class ReviewerResponse");
            promptBuilder.AppendLine("{");
            promptBuilder.AppendLine("    string GeneralComment;");
            promptBuilder.AppendLine("    string ImprovedContent;");
            promptBuilder.AppendLine("}");
            promptBuilder.AppendLine("Ví dụ về output:");
            promptBuilder.AppendLine("{");
            promptBuilder.AppendLine("  \"GeneralComment\": \"Đây là nhận xét chung cho bài viết của tôi, hãy nhớ là phải sử dụng tiếng Việt\",");
            promptBuilder.AppendLine("  \"ImprovedContent\": \"Đây là bài viết đã được sửa chữa dựa trên nhận xét trong phần GeneralComment.\"");
            promptBuilder.AppendLine("}");
            promptBuilder.AppendLine("Nếu bài viết của tôi là một thứ vô nghĩa hoặc không thể khác định hoặc không thể hiểu được, GenerateComment sẽ mang giá trị 'Không thể nhận xét' và ImprovedContent mang giá trị ''.");
            promptBuilder.AppendLine("Nội dung bài viết của tôi: ");
            promptBuilder.AppendLine($"{content.Trim()}");

            var result = await Gemini.Generator.GenerateContent(apiKey, promptBuilder.ToString(), true, 50, model);
            return JsonConvert.DeserializeObject<Comment>(result);
        }

        public static async Task<CommentFromImage> GenerateReviewFromImage(string apiKey, EnglishLevel level, MemoryStream image)
        {
            var base64 = await ImageOptimizer.GetOptimizedBase64VersionOf(image);
            var promptBuilder = new StringBuilder();
            var userLevel = GeneralHelper.GetEnumDescription(level);

            promptBuilder.Append("Bạn là một giáo viên tiếng Anh với hơn 20 năm kinh nghiệm giảng dạy, đồng thời đang làm việc tại một trung tâm dạy IELTS lớn. ");
            promptBuilder.Append($"Trình độ tiếng Anh của tôi theo tiêu chuẩn CEFR là '{userLevel}'. ");
            promptBuilder.AppendLine("Bạn hãy đọc nội dung bài viết trong ảnh chụp của tôi, sau đó nhận xét và chỉnh sửa lại để nó tốt hơn. ");
            promptBuilder.AppendLine("Output phải bao gồm 3 phần như sau:");
            promptBuilder.AppendLine("- ExtractedContent: Nội dung bài viết mà bạn extract từ tấm ảnh. Nếu tấm ảnh có quá nhiều text được đặt rời rạc nhau, bạn hãy lấy nội dung text nổi bật nhất trong tấm ảnh");
            promptBuilder.AppendLine("- GeneralComment: Nhận xét chung bằng tiếng Việt cho cả bài viết. Nhận xét của bạn phải dựa trên nội dung chính của bài viết và trình độ tiếng Anh của tôi. Nội dung nhận xét phải bao gồm: Phát hiện lỗi chính tả và nêu cách sửa, phát hiện lỗi ngữ pháp và giải thích cho từng lỗi, đề xuất cách thay thế từ ngữ phù hợp hơn, phân tích phong cách viết và đề xuất cách viết phù hợp với ngữ cảnh và đối tượng tùy vào từng câu trong bài viết, phát hiện lỗi logic và đề xuất cách sửa để nội dung mạch lạc hơn. Bạn cũng phải giải thích chi tiết cho các gợi ý sửa chữa để tôi hiểu rõ và áp dụng hiệu quả.");
            promptBuilder.AppendLine("- ImprovedContent: Bài viết được chỉnh sửa sau khi áp dụng những sửa chữa trong phần GeneralComment, nhớ highlight những đoạn được chỉnh sửa bằng cặp dấu **. Bạn tuyệt đối không được tự ý thay đổi nội dung chính của bài viết, và bài viết sau khi được chỉnh sửa không được phép dài hơn 1.5 lần bài viết ban đầu.");
            promptBuilder.AppendLine("Output phải là một JSON object tương ứng với class C# sau: ");
            promptBuilder.AppendLine("class ReviewerResponse");
            promptBuilder.AppendLine("{");
            promptBuilder.AppendLine("    string ExtractedContent;");
            promptBuilder.AppendLine("    string GeneralComment;");
            promptBuilder.AppendLine("    string ImprovedContent;");
            promptBuilder.AppendLine("}");
            promptBuilder.AppendLine("Ví dụ về output:");
            promptBuilder.AppendLine("{");
            promptBuilder.AppendLine("  \"ExtractedContent\": \"Đây là nội dung bài viết được extract từ ảnh của tôi\",");
            promptBuilder.AppendLine("  \"GeneralComment\": \"Đây là nhận xét chung cho bài viết của tôi, hãy nhớ là phải sử dụng tiếng Việt\",");
            promptBuilder.AppendLine("  \"ImprovedContent\": \"Đây là bài viết đã được sửa chữa dựa trên nhận xét trong phần GeneralComment.\"");
            promptBuilder.AppendLine("}");
            promptBuilder.AppendLine("Nếu bài viết của tôi là một thứ vô nghĩa hoặc không thể khác định hoặc không thể hiểu được hoặc không phải một bài viết tiếng Anh, GenerateComment sẽ mang giá trị 'Không thể nhận xét', còn ImprovedContent mang giá trị ''.");
            promptBuilder.AppendLine("Nội dung bài viết của tôi: ");

            var result = await Gemini.Generator.GenerateReviewFromImage(apiKey, promptBuilder.ToString(), base64);

            return JsonConvert.DeserializeObject<CommentFromImage>(result);
        }
    }
}
