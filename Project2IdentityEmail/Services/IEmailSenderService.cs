namespace Project2IdentityEmail.Services
{
    public interface IEmailSenderService
    {
        Task SendAsync(string recipientEmail, string subject, string body);
    }
}
