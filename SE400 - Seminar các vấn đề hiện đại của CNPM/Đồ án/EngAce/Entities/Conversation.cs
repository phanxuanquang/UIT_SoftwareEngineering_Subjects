namespace Entities
{
    public class Conversation
    {
        public List<History> ChatHistory { get; set; } = new List<History>();
        public required string Question { get; set; }

        public class History
        {
            public bool FromUser { get; set; }
            public string Message { get; set; }
        }
    }
}
