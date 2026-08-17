namespace Project2IdentityEmail.Models
{
    public class MessageInfoDashboardViewModel
    {
        public int ReceivedCount { get; set; }
        public int SentCount { get; set; }

        public DateTime? LastReceivedDate { get; set; }
        public DateTime? LastSentDate { get; set; }
    }
}
