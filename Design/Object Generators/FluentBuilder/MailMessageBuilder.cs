using System.Net.Mail;
using System.Text;

// NOTE: Также можно делегировать методы-строители через методы расширения
namespace FluentBuilder
{
   public sealed class MailMessageBuilder
   {
      private readonly MailMessage _mailMessage = new MailMessage();

      public MailMessageBuilder(string fromAddress) => _mailMessage.From = new MailAddress(fromAddress);

      public MailMessageBuilder To(string toAddress)
      {
         _mailMessage.To.Add(toAddress);
         return this;
      }

      public MailMessageBuilder Cc(string ccAddress)
      {
         _mailMessage.CC.Add(ccAddress);
         return this;
      }

      public MailMessageBuilder Subject(string subject)
      {
         _mailMessage.Subject = subject;
         return this;
      }

      public MailMessageBuilder Body(string body, Encoding encoding)
      {
         _mailMessage.Body = body;
         _mailMessage.BodyEncoding = encoding;
         return this;
      }

      public MailMessage Build() => _mailMessage;
   }
}