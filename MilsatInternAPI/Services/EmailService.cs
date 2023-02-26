using MailKit.Net.Smtp;
using MailKit.Security;
using MilsatInternAPI.Interfaces;
using MimeKit;

namespace MilsatInternAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _iconfig;
        public EmailService(IConfiguration iconfig)
        {
            _iconfig = iconfig;
        }
        public async void SendEmail(string receivermail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_iconfig.GetSection("EmailService:username").Value));
            email.To.Add(MailboxAddress.Parse(receivermail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var client = new SmtpClient();
            client.Connect(
                _iconfig.GetSection("EmailService:host").Value,
                Int32.Parse(_iconfig.GetSection("EmailService:port").Value),
                SecureSocketOptions.SslOnConnect);
            client.Authenticate(
                _iconfig.GetSection("EmailService:username").Value,
                _iconfig.GetSection("EmailService:password").Value);
            await client.SendAsync(email);
            client.Disconnect(true);
        }
    }
}
