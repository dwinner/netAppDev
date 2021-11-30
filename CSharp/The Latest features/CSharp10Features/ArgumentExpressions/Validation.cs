namespace CSharp10Features.ArgumentExpressions;

public static class Validation
{
   public static void ValidateArgument(string parameterName,
      bool condition,
      [CallerArgumentExpression("condition")] string? message = null)
   {
      if (!condition)
      {
         throw new ArgumentException($"Argument failed validation: <{message}>", parameterName);
      }
   }

   public static IEnumerable<T> Sample<T>(this IEnumerable<T> sequence,
      int frequency,
      [CallerArgumentExpression("sequence")] string? message = null)
   {
      if (sequence.Count() < frequency)
      {
         throw new ArgumentException($"Expression doesn't have enough elements: {message}", nameof(sequence));
      }

      int i = 0;
      foreach (T item in sequence)
      {
         if (i++ % frequency == 0)
         {
            yield return item;
         }
      }
   }
}