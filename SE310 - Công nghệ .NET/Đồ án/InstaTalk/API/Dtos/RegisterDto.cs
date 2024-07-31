using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string RoomName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string DisplayName { get; set; }

        public string? SecurityCode { get; set; }
    }

    public class RegisterStrangerDto : RegisterDto
    {
        public StrangerFilterDto? StrangerFilter { get; set; } = null;
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Nationality { get; set; }
    }
}
