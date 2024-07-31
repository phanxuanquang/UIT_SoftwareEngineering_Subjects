using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string DisplayName { get; set; }
        public bool Locked { get; set; } = false;// true = locked

        public string? PhotoUrl { get; set; }//Nullable<string>
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public StrangerFilter? StrangerFilter { get; set; } = null;
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Nationality { get; set; }
        //public ICollection<string> Type { get; set; } = new List<string>();
    }
}
