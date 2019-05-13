using System;
using System.Configuration;

namespace Samples.MailService
{
   internal class MailServerConfig
   {
      public string SmtpServer => ConfigurationManager.AppSettings[nameof(SmtpServer)];

      public int SmtpPort
      {
         get
         {
            var port = ConfigurationManager.AppSettings[nameof(SmtpPort)];
            return Convert.ToInt32(port);
         }
      }

      public string SenderEmail => ConfigurationManager.AppSettings[nameof(SenderEmail)];

      public string SenderPassword => ConfigurationManager.AppSettings[nameof(SenderPassword)];
   }
}