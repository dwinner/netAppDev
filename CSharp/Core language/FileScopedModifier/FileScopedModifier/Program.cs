namespace FileScopedModifier;

internal static class Program
{
   public static void Main()
   {
      Console.WriteLine($"{InternalUtils.ToStr(42)}");
   }
}

file static class InternalUtils
{
   internal static string ToStr(int param) => param.ToString();
}