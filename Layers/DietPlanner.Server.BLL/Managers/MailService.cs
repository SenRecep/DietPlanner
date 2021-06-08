using System.IO;
using System.Threading.Tasks;

using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.BLL.Models;
using DietPlanner.Server.BLL.Settings;

using MailKit.Net.Smtp;
using MailKit.Security;

using MimeKit;

namespace DietPlanner.Server.BLL.Managers
{
    public class MailService : IMessageService
    {
        private readonly IMailSettings _mailSettings;
        public MailService(IMailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendWelcomeAsync(WelcomeRequest request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            StreamReader str = new(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText
                .Replace("[FirstName]", request.FirstName)
                .Replace("[LastName]", request.LastName)
                .Replace("[Password]", request.Password);
            MimeMessage email = new()
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Hoşgeldiniz {request.FirstName} {request.LastName}";
            BodyBuilder builder = new()
            {
                HtmlBody = MailText
            };
            email.Body = builder.ToMessageBody();
            using SmtpClient smtp = new();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
