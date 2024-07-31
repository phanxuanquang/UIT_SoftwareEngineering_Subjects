using System.ComponentModel;

namespace Entities.Enums
{
    public enum EnglishLevel
    {
        [Description("Beginner: Hiểu các thông điệp rõ ràng và cụ thể trong thông tin cơ bản về cá nhân, hoặc những câu đơn giản trong các nội dung rất ngắn")]
        Beginner = 1,

        [Description("Elementary: Hiểu sơ lượt văn bản ngắn về các chủ đề quen thuộc trong cuộc sống và những thông tin cơ bản từ email ngắn")]
        Elementary = 2,

        [Description("Intermediate: Hiểu các văn bản đơn giản như tin tức hoặc tiểu sử cá nhân, có thể tìm thông tin cụ thể trong bài báo ngắn")]
        Intermediate = 3,

        [Description("Upper Intermediate: Hiểu các văn bản khá phức tạp như tác phẩm văn học hoặc thông tin kỹ thuật, có thể hiểu ý chính của văn bản dài")]
        UpperIntermediate = 4,

        [Description("Advanced: Hiểu các văn bản phức tạp như bài nghiên cứu khoa học hoặc bài phê bình văn học, có thể phân tích và đánh giá luận điểm phức tạp")]
        Advanced = 5,

        [Description("Proficient: Hiểu các văn bản mang tính hàn lâm, trừu tượng và rất phức tạp, có khả năng hiểu sâu rộng và phân tích đa chiều nội dung đọc được")]
        Proficient = 6,
    }
}
