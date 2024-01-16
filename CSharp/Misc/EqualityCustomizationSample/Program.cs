namespace EqualityCustomizationSample;

internal static class Program
{
   private static void Main()
   {
      var a1 = new Area(5, 10);
      var a2 = new Area(10, 5);
      Console.WriteLine(a1.Equals(a2)); // True
      Console.WriteLine(a1 == a2); // True
   }
}