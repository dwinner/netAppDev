using System.Configuration;

namespace CustomConfigSectionSample
{
   [ConfigurationCollection(typeof(RecipientElement))]
   public sealed class RecipientCollection : ConfigurationElementCollection
   {
      protected override ConfigurationElement CreateNewElement()
      {
         return new RecipientElement();
      }

      protected override object GetElementKey(ConfigurationElement element)
      {
         return ((RecipientElement)element).DisplayName;
      }
   }
}