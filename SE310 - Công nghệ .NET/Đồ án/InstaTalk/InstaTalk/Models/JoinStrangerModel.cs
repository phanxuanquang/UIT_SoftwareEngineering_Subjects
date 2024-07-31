
using System.ComponentModel.DataAnnotations;

namespace InstaTalk.Models
{
    public class JoinStrangerModel
    {
        [Required]
        public Guid RoomId { get; set; }

        public string? SecurityCode { get; set; } = null!;
    }
}
