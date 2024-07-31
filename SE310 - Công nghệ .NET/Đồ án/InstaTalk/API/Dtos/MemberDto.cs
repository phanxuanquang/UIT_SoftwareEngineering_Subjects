namespace API.Dtos
{
    public class MemberDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
        public bool Locked { get; set; }
    }
}
