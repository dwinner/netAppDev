using System.Runtime.CompilerServices;

internal class Message
{
   [OverloadResolutionPriority(1)]
   public void Print(string text)
   {
      Console.WriteLine("Overload Priority 1");
      Console.WriteLine(text);
   }

   [OverloadResolutionPriority(2)]
   public void Print(ReadOnlySpan<char> chars)
   {
      Console.WriteLine("Overload Priority 2");
      Console.WriteLine(chars.ToArray());
   }
}