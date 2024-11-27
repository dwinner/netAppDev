namespace DynamicLoadExtension
{
   /// <summary>
   ///    These are templates to use for IL generation and
   ///    are not called directly.
   /// </summary>
   public class Templates
   {
      public static object CreateNewExtensionTemplate() => new Extension();

      public static bool CallMethodTemplate(object ExtensionObj, string argument)
      {
         var extension = (Extension)ExtensionObj;
         return extension.DoWork(argument);
      }
   }
}