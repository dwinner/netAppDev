namespace EnumToIntergralConversion;

internal static class Program
{
   private static void Main()
   {
      var i = (int)BorderSides.Top; // i == 4
      var side = (BorderSides)i; // side == BorderSides.Top

      var integralValue = GetIntegralValue(BorderSides.Top);
      Console.WriteLine(integralValue);

      var anyIntegralValue = GetAnyIntegralValue(BorderSides.Top);
      Console.WriteLine(anyIntegralValue);

      var result = GetBoxedIntegralValue(BorderSides.Top);
      Console.WriteLine(result); // 4
      Console.WriteLine(result.GetType()); // System.Int32

      var integralValueAsString = GetIntegralValueAsString(BorderSides.Top);
      Console.WriteLine(integralValueAsString);
   }

   private static int GetIntegralValue(Enum anyEnum)
      => (int)(object)anyEnum;

   private static decimal GetAnyIntegralValue(Enum anyEnum)
      => Convert.ToDecimal(anyEnum);

   private static object GetBoxedIntegralValue(Enum anyEnum)
   {
      var integralType = Enum.GetUnderlyingType(anyEnum.GetType());
      return Convert.ChangeType(anyEnum, integralType);
   }

   private static string GetIntegralValueAsString(Enum anyEnum)
      => anyEnum.ToString("D"); // returns something like "4"
}

[Flags]
public enum BorderSides
{
   Left = 1,
   Right = 2,
   Top = 4,
   Bottom = 8
}