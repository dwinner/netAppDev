namespace TypeSafetyEnumSample;

internal static class Program
{
   private static void Main()
   {
      for (var i = 0; i <= 16; i++)
      {
         var side = (BorderSides)i;
         Console.WriteLine(IsFlagDefined(side) + " " + side);
      }
   }

   private static bool IsFlagDefined(Enum e)
   {
      var enumDump = e.ToString();
      return !decimal.TryParse(enumDump, out _);
   }
}

[Flags]
public enum BorderSides
{
   Left = 0x1,
   Right = 0x2,
   Top = 0x4,
   Bottom = 0x8
}