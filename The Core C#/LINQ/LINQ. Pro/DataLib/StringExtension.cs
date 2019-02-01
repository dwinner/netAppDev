namespace DataLib
{
   public static class StringExtension
   {
      public static string FirstName(this string name)
      {
         int index = name.LastIndexOf(' ');
         return name.Substring(0, index);
      }

      public static string LastName(this string name)
      {
         int index = name.LastIndexOf(' ');
         return name.Substring(index + 1);
      }
   }
}