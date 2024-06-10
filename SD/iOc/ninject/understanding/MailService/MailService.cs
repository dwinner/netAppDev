using System.Net.Mail;

namespace Samples.MailService
{
   internal class MailService
   {
      private readonly ILogger _logger;

      public MailService(ILogger logger) => _logger = logger;

      public void SendMail(string address, string subject, string body)
      {
         _logger.Log("Creating mail message...");
         var mail = new MailMessage();
         mail.To.Add(address);
         mail.Subject = subject;
         mail.Body = body;
         var client = new SmtpClient();
         // Setup client with smtp server address and port here
         _logger.Log("Sending message...");
         client.Send(mail);
         _logger.Log("Message sent successfully.");
      }
   }
}