using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class EditRoomDto
    {
        [Required]
        public Guid RoomId { get; set; }

        [Required]
        public string RoomName { get; set; } = null!;

        public string? SecurityCode { get; set; }
    }
}
