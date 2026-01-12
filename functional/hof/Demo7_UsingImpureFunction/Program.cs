using static System.Console;

//Console.WriteLine("Demo 7.");
Transformer transformer = new(2);
var result = transformer.MultiplyBy(5.5);
WriteLine($"Result: {result}");

internal class Transformer
{
   private readonly double multiplier;

   public Transformer(double multiplier) => this.multiplier = multiplier;

   public double MultiplyBy(double input) => input * multiplier;
}