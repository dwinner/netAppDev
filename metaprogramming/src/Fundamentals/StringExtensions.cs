namespace Fundamentals;

/// <summary>
///    Provides a set of extension methods to <see cref="string" />.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///    Convert a string into a camel cased string.
    /// </summary>
    /// <param name="stringToConvert">string to convert.</param>
    /// <returns>Converted string.</returns>
    public static string ToCamelCase(this string stringToConvert)
   {
      if (!string.IsNullOrEmpty(stringToConvert))
      {
         if (stringToConvert.Length == 1)
         {
            return stringToConvert.ToLowerInvariant();
         }

         var firstLetter = stringToConvert[0].ToString().ToLowerInvariant();
         return $"{firstLetter}{stringToConvert.Substring(1)}";
      }

      return stringToConvert;
   }

    /// <summary>
    ///    Convert a string into a pascal cased string.
    /// </summary>
    /// <param name="stringToConvert">string to convert.</param>
    /// <returns>Converted string.</returns>
    public static string ToPascalCase(this string stringToConvert)
   {
      if (!string.IsNullOrEmpty(stringToConvert))
      {
         if (stringToConvert.Length == 1)
         {
            return stringToConvert.ToUpperInvariant();
         }

         var firstLetter = stringToConvert[0].ToString().ToUpperInvariant();
         return $"{firstLetter}{stringToConvert.Substring(1)}";
      }

      return stringToConvert;
   }
}