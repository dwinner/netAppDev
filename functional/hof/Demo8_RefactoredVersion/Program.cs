using static System.Console;

var result = Transformer.MultiplyBy(5.5, 2);
WriteLine($"Result: {result}");

internal static class Transformer
{
   public static double MultiplyBy(
      double input, double multiplier) =>
      input * multiplier;
}