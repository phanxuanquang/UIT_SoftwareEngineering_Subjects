namespace InstaTalk.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime LastActive { get; set; }
    }
}
