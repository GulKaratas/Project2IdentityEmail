using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewModels
{
    public class HeaderUserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public int UnreadCount { get; set; }
        public List<Message> RecentMessages { get; set; } = new();

        public string FullName => $"{Name} {Surname}".Trim();
    }
}
