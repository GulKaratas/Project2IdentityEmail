namespace Project2IdentityEmail.ViewModels
{
    public class ProfileInfoDashboardViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool EmailConfirmed { get; set; }

        public string FullName => $"{Name} {Surname}".Trim();
    }
}