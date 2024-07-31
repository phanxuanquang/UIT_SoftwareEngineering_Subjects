using System.ComponentModel.DataAnnotations;

namespace InstaTalk.Models
{
    public class CreateRoomModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string? RoomName { get; set; } = "Test room";

        [Required(ErrorMessage = "Phải có tên")]
        [Display(Name = "Nickname")]
        [StringLength(20, MinimumLength = 6)]
        public string? DisplayName { get; set; }

        public string? SecurityCode { get; set; }
    }
}
