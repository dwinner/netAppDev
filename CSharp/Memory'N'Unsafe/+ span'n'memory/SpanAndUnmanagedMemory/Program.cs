using System.Runtime.InteropServices;

namespace SpanAndUnmanagedMemory;

internal static class Program
{
   private static unsafe void Main()
   {
      var source = "The quick brown fox".AsSpan();
      var ptr = Marshal.AllocHGlobal(source.Length * sizeof(char));
      try
      {
         var unmanaged = new Span<char>((char*)ptr, source.Length);
         source.CopyTo(unmanaged);
         foreach (var word in unmanaged.Split())
         {
            Console.WriteLine(word.ToString());
         }
      }
      finally
      {
         Marshal.FreeHGlobal(ptr);
      }
   }
}