namespace API.Dtos
{
    public class UserConnectionInfo
    {
        public UserConnectionInfo(Guid userId, string displayName, Guid roomId)
        {
            UserID = userId;
            DisplayName = displayName;
            RoomId = roomId;
        }
        public Guid UserID { get; set; }
        public string DisplayName { get; set; }
        public Guid RoomId { get; set; }
    }
}
