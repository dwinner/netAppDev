// Создание собственных разделов конфигурации

using System.Collections.Generic;
using System.Linq;
using static System.Array;
using static System.Configuration.ConfigurationManager;
using static System.Console;

namespace CustomConfigSectionSample
{
   internal static class Program
   {
      private static IEnumerable<EmailRecipient> EmailRecipients
      {
         get
         {
            var recipientsConfigSections = GetSection("EmailRecipients") as RecipientsConfigSections;
            if (recipientsConfigSections != null)
            {
               return (from RecipientElement recipient in recipientsConfigSections.Recipients
                       select new EmailRecipient
                       {
                          ToDisplayName = recipient.DisplayName,
                          ToAddress = recipient.Address
                       }).ToList();
            }

            throw new SocialConfigurationException("Section 'EmailRecipients' not registered in App.config");
         }
      }

      private static void Main()
      {
         var recipients = EmailRecipients;
         ForEach(recipients.ToArray(), recipient => WriteLine(recipient));
      }
   }
}