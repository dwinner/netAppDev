// Использование DLR Для извлечения парметров конфигурации

using System;
using static System.Console;

namespace DynConfReading
{
   internal static class Program
   {
      private static void Main()
      {
         dynamic configWrapper = new DynamicConfig();
         var buildNumber = int.Parse(configWrapper.BuildNumber.ToString());
         var callhomeUrl = (string)configWrapper.CallhomeUrl;
         var productId = Convert.ToInt64(configWrapper.ProductId);
         var productVersion = (string)configWrapper.ProductVersion;
         var multipleSessionsEnabled = bool.Parse(configWrapper.MultipleSessionsEnabled.ToString());

         WriteLine("Build number: {0}", buildNumber);
         WriteLine("Call home url: {0}", callhomeUrl);
         WriteLine("Product id: {0}", productId);
         WriteLine("Product version: {0}", productVersion);
         WriteLine("Multiple session enabled: {0}", multipleSessionsEnabled);
      }
   }
}