using System.Configuration;
using static System.Configuration.ConfigurationManager;

namespace Trackerbird.Config
{
   public sealed class TrackerbirdSection : ConfigurationSection
   {
      private const string BuildNumberPropertyName = "buildNumber";
      private const string CallhomeUrlPropertyName = "callhomeUrl";
      private const string ProductIdPropertyName = "productId";
      private const string ProductVersionPropertyName = "productVersion";
      private const string MultipleSessionsEnabledPropertyName = "multipleSessionsEnabled";

      public static TrackerbirdSection Config
         => (GetSection("trackerBird") as TrackerbirdSection) ?? new TrackerbirdSection();

      [ConfigurationProperty(BuildNumberPropertyName, IsRequired = true)]
      public int BuildNumber
      {
         get { return (int)this[BuildNumberPropertyName]; }
         set { this[BuildNumberPropertyName] = value; }
      }

      [ConfigurationProperty(CallhomeUrlPropertyName, IsRequired = true)]
      public string CallHomeUrl
      {
         get { return (string) this[CallhomeUrlPropertyName]; }
         set { this[CallhomeUrlPropertyName] = value; }
      }

      [ConfigurationProperty(ProductIdPropertyName, IsRequired=true)]
      public long ProductId {
         get { return (long) this[ProductIdPropertyName]; }
         set { this[ProductIdPropertyName] = value; }
      }

      [ConfigurationProperty(ProductVersionPropertyName, IsRequired = true)]
      public string ProductVersion
      {
         get { return (string) this[ProductVersionPropertyName]; }
         set { this[ProductVersionPropertyName] = value; }
      }

      [ConfigurationProperty(MultipleSessionsEnabledPropertyName, IsRequired = false, DefaultValue = false)]
      public bool MultipleSessionsEnabled
      {
         get { return (bool) this[MultipleSessionsEnabledPropertyName]; }
         set { this[MultipleSessionsEnabledPropertyName] = value; }
      }
   }
}