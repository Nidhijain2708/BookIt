using System.Net;
using System.Net.Mail;

namespace BookIt.API.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpHost = configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            var smtpUser = configuration["EmailSettings:SmtpUser"];
            var smtpPass = configuration["EmailSettings:SmtpPass"];

            var client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUser, smtpPass)
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: smtpUser,
                    to: email,
                    subject: subject,
                    message
                )
            );
        }
    }
}

//using MailKit.Net.Smtp;
//using MimeKit;

//namespace BookIt.API.Services
//{
//    public class EmailSender : IEmailSender
//    {
//        private readonly IConfiguration configuration;

//        public EmailSender(IConfiguration configuration)
//        {
//            this.configuration = configuration;
//        }

//        public async Task SendEmailAsync(string email, string subject, string message)
//        {
//            var smtpHost = configuration["EmailSettings:SmtpHost"];
//            var smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
//            var smtpUser = configuration["EmailSettings:SmtpUser"];
//            var smtpPass = configuration["EmailSettings:SmtpPass"];

//            var emailMessage = new MimeMessage();
//            emailMessage.From.Add(new MailboxAddress("Nidhi Jain", smtpUser));
//            emailMessage.To.Add(new MailboxAddress("", email));
//            emailMessage.Subject = subject;

//            var bodyBuilder = new BodyBuilder { HtmlBody = message };
//            emailMessage.Body = bodyBuilder.ToMessageBody();

//            using (var client = new SmtpClient())
//            {
//                client.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
//                client.Authenticate(smtpUser, smtpPass);

//                await client.SendAsync(emailMessage);
//                client.Disconnect(true);
//            }
//        }
//    }
//}