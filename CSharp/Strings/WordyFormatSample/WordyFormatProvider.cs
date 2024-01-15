using System.Globalization;
using System.Text;

namespace WordyFormatSample;

public class WordyFormatProvider(IFormatProvider parent) : IFormatProvider, ICustomFormatter
{
   private const string DigitWords = "0123456789-.";

   private static readonly string[] NumberWords =
      "zero one two three four five six seven eight nine minus point".Split();

   public WordyFormatProvider() : this(CultureInfo.CurrentCulture)
   {
   }

   public string Format(string? format, object? arg, IFormatProvider? prov)
   {
      // If it's not our format string, defer to the parent provider:
      if (arg == null || format != "W")
      {
         var fmtStr = "{0:" + format + "}";
         return string.Format(parent, fmtStr, arg);
      }

      var result = new StringBuilder();
      var digitList = string.Format(CultureInfo.InvariantCulture, "{0}", arg);
      foreach (var idx in digitList.Select(digit => DigitWords.IndexOf(digit)).Where(i => i != -1))
      {
         if (result.Length > 0)
         {
            result.Append(' ');
         }

         result.Append(NumberWords[idx]);
      }

      return result.ToString();
   }

   public object? GetFormat(Type? formatType) => formatType == typeof(ICustomFormatter)
      ? this
      : null;
}