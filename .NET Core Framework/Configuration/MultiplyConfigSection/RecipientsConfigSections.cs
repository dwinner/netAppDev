using System.Configuration;

namespace CustomConfigSectionSample
{
   public sealed class RecipientsConfigSections : ConfigurationSection
   {
      [ConfigurationProperty("Recipients")]
      public RecipientCollection Recipients => (RecipientCollection)base["Recipients"];
   }
}