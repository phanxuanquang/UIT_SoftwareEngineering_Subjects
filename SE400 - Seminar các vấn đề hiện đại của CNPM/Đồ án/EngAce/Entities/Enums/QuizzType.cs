using System.ComponentModel;

namespace Entities.Enums
{
    public enum QuizzType
    {
        [Description("Câu hỏi dạng chọn câu đúng (Sentence Correction Question)")]
        SentenceCorrection = 1,

        [Description("Câu hỏi dạng điền từ (Fill-in-the-Blank Question)")]
        FillTheBlank = 2,

        [Description("Câu hỏi về đọc hiểu (Reading Comprehension Question)")]
        ReadingComprehension = 3,

        [Description("Câu hỏi về đồng nghĩa và trái nghĩa (Synonym and Antonym Question)")]
        SynonymAndAntonym = 4,

        [Description("Câu hỏi về chức năng ngôn ngữ (Functional Language Question)")]
        FunctionalLanguage = 5,

        [Description("Câu hỏi về từ vựng (Vocabulary Question)")]
        Vocabulary = 6,

        [Description("Câu hỏi về ngữ pháp (Grammar Question)")]
        Grammar = 7,

        [Description("Câu hỏi về phát âm (Pronunciation Question)")]
        Pronunciation = 8,
    }
}