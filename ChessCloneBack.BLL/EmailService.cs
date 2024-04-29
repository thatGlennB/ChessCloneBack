using ChessCloneBack.BLL.Interfaces;
using ChessCloneBack.Templates.Interfaces;
using ChessCloneBack.Templates.Views.Emails.ConfirmAccount;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ChessCloneBack.BLL
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IRazorViewToStringRenderer _renderer;
        public EmailService(IConfiguration config, IRazorViewToStringRenderer renderer) { 
            _config = config; 
            _renderer = renderer;
        }
        private SmtpClient CreateSmtpClient() 
        {
            NetworkCredential credentials = new NetworkCredential(_config["SMTP:username"], _config["SMTP:password"]);
            int port;
            bool enableSsl;
            if (!int.TryParse(_config["SMTP:port"], out port)) 
                throw new Exception("Smtp client could not be created, because port value was not found");
            if (!bool.TryParse(_config["SMTP:enableSsl"], out enableSsl)) 
                throw new Exception("Smtp client could not be created, because enableSsl value was not found");
            return new SmtpClient(_config["SMTP:host"])
            {
                Port = port,
                Credentials = credentials,
                EnableSsl = enableSsl
            };
        }
        public void Send()
        {
            string destinationAddress = "user@example.com";


            MailAddress sendAddress = new MailAddress("no-reply@chess.chess");
            MailMessage mailMessage = new MailMessage
            {
                From = sendAddress,
                Subject = "test",
                Body = "<h1>Hello<h1>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(destinationAddress);


            using (SmtpClient client = CreateSmtpClient())
            {
                client.Send(mailMessage);
            }




        }

        public async Task SendAsync()
        {
            string destinationAddress = "user@example.com";


            ConfirmAccountEmailViewModel confirmAccountModel = new("www.google.com");
            string body = await _renderer.RenderViewToStringAsync("/Views/Emails/ConfirmAccount/ConfirmAccount.cshtml", confirmAccountModel);

            MailAddress sendAddress = new MailAddress("no-reply@chess.chess");
            MailMessage mailMessage = new MailMessage
            {
                From = sendAddress,
                Subject = "test",
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(destinationAddress);

            using (SmtpClient client = CreateSmtpClient())
            {
                client.Send(mailMessage);
            }
        }
    }
}
