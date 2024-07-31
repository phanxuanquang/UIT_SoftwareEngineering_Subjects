namespace API.Dtos
{
    public class MessageDto
    {
        public string SenderDisplayName { get; set; }
        public Guid SenderUserID { get; set; }
        public string Content { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
