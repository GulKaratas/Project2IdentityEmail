using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewModels
{
    public class InboxViewModel
    {
        public List<Message> Messages { get; set; }
        public List<Category> Categories { get; set; }
    }
}