namespace AsyncLocal_Inherited;

internal static class Program
{
   private static readonly AsyncLocal<string> AsyncLocalTest = new();

   private static void Main()
   {
      AsyncLocalTest.Value = "test";
      new Thread(AnotherMethod).Start();
   }

   private static void AnotherMethod() => Console.WriteLine(AsyncLocalTest.Value); // test
}