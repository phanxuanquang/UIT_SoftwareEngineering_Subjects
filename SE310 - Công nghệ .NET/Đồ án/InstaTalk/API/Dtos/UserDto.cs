namespace API.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime LastActive { get; set; }
        public StrangerFilterDto? StrangerFilter { get; set; } = null;
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Nationality { get; set; }
        //public ICollection<string> Type { get; set; } = new List<string>();
    }
}
