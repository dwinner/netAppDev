using System.Net;
using System.Net.Mail;

namespace Samples.MailService
{
   internal class MailService
   {
      private readonly ILogger _logger;
      private readonly string _sender;
      private SmtpClient _client;

      public MailService(MailServerConfig config, ILogger logger)
      {
         _logger = logger;
         InitializeClient(config);
         _sender = config.SenderEmail;
      }

      public void SendMail(string address, string subject, string body)
      {
         _logger.Log("Initializing...");
         var mail = new MailMessage(_sender, address)
         {
            Subject = subject,
            Body = body
         };
         _logger.Log("Sending message...");
         _client.Send(mail);
         _logger.Log("Message sent successfully.");
      }

      private void InitializeClient(MailServerConfig config)
      {
         _client = new SmtpClient
         {
            Host = config.SmtpServer,
            Port = config.SmtpPort,
            EnableSsl = true,
            Credentials = new NetworkCredential {UserName = config.SenderEmail, Password = config.SenderPassword}
         };
      }
   }
}