using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using MailKit.Net.Smtp;
using MimeKit;
using System.Configuration;
using System.Net.Mail;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string toEmail, string subject, string body)
        {
            var fromEmail = ConfigurationManager.AppSettings["SmtpEmail"];
            var password = ConfigurationManager.AppSettings["SmtpPassword"];
            var smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            var port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect(smtpServer, port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(fromEmail, password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}