using System.Text;

namespace AsyncLocal_ShallowCopy;

internal static class Program
{
   private static readonly AsyncLocal<StringBuilder> AsyncLocalTest = new();

   private static void Main()
   {
      AsyncLocalTest.Value = new StringBuilder("test");
      var t = new Thread(AnotherMethod);
      t.Start();
      t.Join();
      Console.WriteLine(AsyncLocalTest.Value.ToString()); // test haha!
   }

   private static void AnotherMethod() => AsyncLocalTest.Value?.Append(" ha-ha!");
}