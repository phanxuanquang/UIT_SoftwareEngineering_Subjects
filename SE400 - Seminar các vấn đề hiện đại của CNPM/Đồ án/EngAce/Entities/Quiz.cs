namespace Entities
{
    public class Quiz
    {
        public required string Question { get; set; }
        public required string[] Options { get; set; }
        public required sbyte RightOptionIndex { get; set; }
        public required string ExplanationInVietnamese { get; set; }
    }
}
