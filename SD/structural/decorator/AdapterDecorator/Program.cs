using static System.Console;

namespace AdapterDecorator;

internal static class Program
{
   private static void Main()
   {
      MyStringBuilder stringBuilder = "hello ";
      stringBuilder += "world"; // will work even without op+ in MyStringBuilder
      // why? you figure it out!
      WriteLine(stringBuilder);
   }
}