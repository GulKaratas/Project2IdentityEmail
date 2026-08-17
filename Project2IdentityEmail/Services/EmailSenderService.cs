using MailKit.Net.Smtp;
using MimeKit;

namespace Project2IdentityEmail.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string recipientEmail, string subject, string body)
        {
            var host = _configuration["EmailSettings:Host"] ?? "smtp.gmail.com";
            var port = _configuration.GetValue<int?>("EmailSettings:Port") ?? 587;
            var senderName = _configuration["EmailSettings:SenderName"] ?? "Admin";
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var password = _configuration["EmailSettings:Password"];

            var missingKeys = new List<string>();

            if (string.IsNullOrWhiteSpace(senderEmail))
            {
                missingKeys.Add("EmailSettings:SenderEmail");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                missingKeys.Add("EmailSettings:Password");
            }

            if (missingKeys.Count > 0)
            {
                throw new InvalidOperationException(
                    $"SMTP yapılandırması eksik: {string.Join(", ", missingKeys)}. " +
                    "Bu değeri User Secrets ile tanımlayın: " +
                    "dotnet user-secrets set \"EmailSettings:Password\" \"<gmail-uygulama-sifresi>\"");
            }

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(senderName, senderEmail));
            mimeMessage.To.Add(new MailboxAddress("User", recipientEmail));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new BodyBuilder { TextBody = body }.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(host, port, false);
            await client.AuthenticateAsync(senderEmail, password);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}
