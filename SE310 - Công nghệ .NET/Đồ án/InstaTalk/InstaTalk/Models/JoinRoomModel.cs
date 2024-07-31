using System.ComponentModel.DataAnnotations;

namespace InstaTalk.Models
{
    public class JoinRoomModel
    {
        [Required]
        public Guid RoomId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string DisplayName { get; set; } = "Rommmmmmmm";

        public string? SecurityCode { get; set; } = null!;
    }
}
