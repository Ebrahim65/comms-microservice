using EmailService.Config;
using EmailService.Models;
using EmailService.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EmailService.Services.Implementations
{
    public class SmtpEmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public SmtpEmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> SendEmailAsync(EmailMessage message)
        {
            try
            {
                using (var client = new SmtpClient(_settings.SmtpServer, _settings.Port))
                {
                    client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                        Subject = message.Subject,
                        Body = message.Body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(message.To);

                    await client.SendMailAsync(mailMessage);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
