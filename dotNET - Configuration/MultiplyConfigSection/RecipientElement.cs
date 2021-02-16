using System.Configuration;

namespace CustomConfigSectionSample
{
   public sealed class RecipientElement : ConfigurationElement
   {
      [ConfigurationProperty("displayName", DefaultValue = "", IsKey = false, IsRequired = true)]
      public string DisplayName => (string)base["displayName"];

      [ConfigurationProperty("address", DefaultValue = "", IsKey = true, IsRequired = true)]
      public string Address => (string)base["address"];
   }
}