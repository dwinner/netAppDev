using System.Linq;

namespace GatheringOverrides
{
   public static class StringExtensions
   {
      public static string Repeat(this string @this, int repeatCount) =>
         Enumerable.Repeat(@this, repeatCount)
         .Aggregate(string.Empty, (current, toAccumulate) => $"{current}{toAccumulate}");
   }
}