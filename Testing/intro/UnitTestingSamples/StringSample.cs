using System;

namespace UnitTestingSamples;

public class StringSample(string init)
{
   private readonly string _init = init ?? throw new ArgumentNullException(nameof(init));

   public string GetStringDemo(string first, string second)
   {
      ArgumentNullException.ThrowIfNull(first, nameof(first));
      ArgumentException.ThrowIfNullOrEmpty("empty string is allowed", first);
      if (second is null)
      {
         throw new ArgumentNullException(nameof(second));
      }

      if (second.Length > first.Length)
      {
         throw new ArgumentOutOfRangeException(nameof(second),
            "must be shorter than second");
      }

      var startIndex = first.IndexOf(second, StringComparison.Ordinal);
      if (startIndex < 0)
      {
         return $"{second} not found in {first}";
      }

      if (startIndex < 5)
      {
         var result = first.Remove(startIndex, second.Length);
         return $"removed {second} from {first}: {result}";
      }

      return _init.ToUpperInvariant();
   }
}