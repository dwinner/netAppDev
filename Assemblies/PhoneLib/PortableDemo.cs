namespace PortableSharingCode
{
   public class PortableDemo
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
