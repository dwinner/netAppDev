namespace SpecifyingState_Better;

internal static class Program
{
   private static void Main()
   {
      var task = Task.Factory.StartNew(_ => Greet("Hello"), "Greeting");
      Console.WriteLine(task.AsyncState); // Greeting
   }

   private static void Greet(string message)
   {
      Console.WriteLine(message);
   }
}