using System.Threading;

namespace LazyInitialization
{
   internal class MyClass
   {
      private int[] _arr;

      public MyClass(int size) => _arr = new int[size];
   }

   internal static class Program
   {
      private static void Main()
      {
         MyClass obj = null;
         LazyInitializer.EnsureInitialized(ref obj, () => new MyClass(99));
      }
   }
}