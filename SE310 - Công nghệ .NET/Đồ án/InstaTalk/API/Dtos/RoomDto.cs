namespace API.Dtos
{
    public class RoomDto
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public int CountMember { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool BlockedChat { get; set; } = false;
    }
}
