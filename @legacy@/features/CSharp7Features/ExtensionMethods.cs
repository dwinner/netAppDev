namespace CSharp7Features
{
   public static class ExtensionMethods
   {
      /// <summary>
      ///    Illustrate the use of a discard variable
      /// </summary>
      /// <param name="value">The string to check</param>
      /// <returns>True is a valid Integer, else False</returns>
      public static bool ToInt(this string value) => int.TryParse(value, out var _);
   }
}