using ChessCloneBack.BLL.Interfaces;
using System.Net;
using System.Net.Mail;

namespace ChessCloneBack.BLL
{
    public class EmailService : IEmailService
    {
        public void Send()
        {
            MailAddress sendAddress = new MailAddress("no-reply@chess.chess");
            NetworkCredential credentials = new NetworkCredential("705cd2c47444b2", "afe5a43f752846");
            MailMessage mailMessage = new MailMessage
            {
                From = sendAddress,
                Subject = "test",
                Body = "<h1>Hello<h1>",
                IsBodyHtml = true
            };
            mailMessage.To.Add("user@example.com");
            SmtpClient client = new SmtpClient("sandbox.smtp.mailtrap.io")
            {
                Port = 587,
                Credentials = credentials,
                EnableSsl = true
            };

            client.Send(mailMessage);

            client.Dispose();
        }
    }
}
