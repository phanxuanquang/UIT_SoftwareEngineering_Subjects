using Entities.Enums;

namespace EngAce.Api.DTO
{
    public class GenerateComment
    {
        public string Content { get; set; }
        public EnglishLevel UserLevel { get; set; }
    }
}
