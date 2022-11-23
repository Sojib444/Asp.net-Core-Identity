using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace IdentityFrameworkWebApllication.Service
{
    public class SendEmail : ISendEmail
    {
        public SendEmail(IOptions<EmailService> options)
        {
            Options = options;
        }

        public IOptions<EmailService> Options { get; }

        public async Task SendAsync(string from, string to, string sub, string body)
        {
            var massage = new MailMessage(from, to, sub, body);

            using (var emailClient = new SmtpClient(Options.Value.Domain, Options.Value.port))
            {
                emailClient.Credentials = new NetworkCredential(Options.Value.Email, Options.Value.Password);

                await emailClient.SendMailAsync(massage);
            }

        }
    }
}
