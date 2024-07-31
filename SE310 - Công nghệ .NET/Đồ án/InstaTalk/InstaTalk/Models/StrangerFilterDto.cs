namespace InstaTalk.Models
{
    public class StrangerFilterDto
    {
        public ICollection<string> FindGender { get; set; } = new List<string>();

        public int MinAge { get; set; } = 0;

        public int MaxAge { get; set; } = 100;

        public ICollection<string> FindRegion { get; set; } = new List<string>();

        //public ICollection<string> FindType { get; set; } = new List<string>();
    }
}
