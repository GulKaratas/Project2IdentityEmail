using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewModels
{
    public class SendMessageViewModel
    {
        public List<Message> Messages { get; set; }
        public List<Category> Categories { get; set; }
    }
}