/**
 * Fluent builder pattern
 */

using System.Net.Mail;
using System.Text;

namespace FluentBuilder
{
   internal static class Program
   {
      private static void Main()
      {
         var mailMessage = new MailMessageBuilder("dwinner@inbox.ru")
            .To("st@unknown.com")
            .Cc("my_boss@unknown.com")
            .Subject("Msdn is down")
            .Body("Please fix!", Encoding.UTF8)
            .Build();
         var client = new SmtpClient();
         client.Send(mailMessage);
      }
   }
}