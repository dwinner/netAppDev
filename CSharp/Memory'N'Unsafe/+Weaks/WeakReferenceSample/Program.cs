namespace WeakReferenceSample;

internal static class Program
{
   private static void Main()
   {
      AddWidgets(); // In another method so they go out of scope

      Console.WriteLine("Before GC:");
      Widget.ListAllWidgets();

      GC.Collect();

      Console.WriteLine("After GC:");
      Widget.ListAllWidgets();
   }

   private static void AddWidgets()
   {
      new Widget("foo");
      new Widget("bar");
   }
}