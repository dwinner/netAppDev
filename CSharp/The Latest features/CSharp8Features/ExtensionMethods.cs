using System.Reflection;

namespace CSharp8Features
{
   public static class ExtensionMethods
   {
      public static bool IsValid(this Student student) => student != null && !string.IsNullOrEmpty(student.FirstName);

      public static PropertyInfo[] GetProps<T>(this T obj) where T : unmanaged
      {
         var t = obj.GetType();
         return t.GetProperties();
      }
   }
}