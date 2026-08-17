using Project2IdentityEmail.Dtos;

namespace Project2IdentityEmail.ViewModels
{
    public class ProfileViewModel
    {
        public UserEditDto Profile { get; set; } = new();

        public ChangePasswordDto Password { get; set; } = new();

        public string UserName { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool IsLockedOut { get; set; }

        public List<string> ProfileErrors { get; set; } = new();

        public List<string> PasswordErrors { get; set; } = new();

        public string FullName => $"{Profile.Name} {Profile.Surname}".Trim();
    }
}
