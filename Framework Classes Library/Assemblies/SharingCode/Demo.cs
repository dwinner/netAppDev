/**
 * Способ обеспечения переносимости за счет использования директив компилятора
 */

namespace SharingCode
{
   public class Demo
   {
      public string PlatformString()
      {
#if SILVERLIGHT
         return "Silverlight";
#elif WINDOWS_PHONE
         return "Windows 8 Metro";
#else
         return "Default";
#endif
      }
   }
}
