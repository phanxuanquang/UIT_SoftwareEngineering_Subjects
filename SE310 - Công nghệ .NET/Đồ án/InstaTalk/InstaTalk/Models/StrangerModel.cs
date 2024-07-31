namespace InstaTalk.Models
{
    public class StrangerModel : CreateRoomModel
    {
        public StrangerFilterDto? StrangerFilter { get; set; } = null;
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Nationality { get; set; }
    }
}
