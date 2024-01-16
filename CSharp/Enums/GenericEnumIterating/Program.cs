namespace GenericEnumIterating;

internal static class Program
{
   private static void Main()
   {
      var values = Enum.GetValues<BorderSides>();
      Array.ForEach(values, sideValue => Console.WriteLine(sideValue));
   }
}

[Flags]
public enum BorderSides
{
   Left = 1,
   Right = 2,
   Top = 4,
   Bottom = 8
}