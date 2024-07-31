using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoomId { get; set; }

        public string? RoomName { get; set; }

        public string? SecurityCode { get; set; }

        public int CountMember { get; set; }

        public AppUser AppUser { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        [DefaultValue(false)]
        public bool BlockedChat { get; set; }

        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }

    public class Connection
    {
        public Connection() { }

        public Connection(string connectionId, Guid userId)
        {
            ConnectionId = connectionId;
            UserID = userId;
        }

        [Key]
        public string ConnectionId { get; set; }

        public Guid UserID { get; set; }
    }

    public class StrangerFilter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FilterID { get; set; }

        [NotMapped]
        public ICollection<string> FindGender { get => _FindGender.Split(','); set => _FindGender = string.Join(',', value); }

        public string _FindGender { get; set; } = string.Empty;

        [DefaultValue(0)]
        public int MinAge { get; set; }

        [DefaultValue(100)]
        public int MaxAge { get; set; }

        [NotMapped]
        public ICollection<string> FindRegion { get => _FindRegion.Split(','); set => _FindRegion = string.Join(',', value); }

        public string _FindRegion { get; set; } = string.Empty;

        public Room? CurrentRoom { get; set; }
    }
}
