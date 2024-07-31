using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class JoinStrangerRoomDto
    {
        [Required]
        public Guid RoomId { get; set; }

        public string? SecurityCode { get; set; } = null!;
    }
}
