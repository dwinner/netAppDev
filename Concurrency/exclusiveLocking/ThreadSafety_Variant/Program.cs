namespace ThreadSafety_Variant;

internal static class Program
{
   private static readonly List<string> List = new();

   private static void Main()
   {
      new Thread(AddItem).Start();
      new Thread(AddItem).Start();
   }

   private static void AddItem()
   {
      lock (List)
      {
         List.Add($"Item {List.Count}");
      }

      string[] items;
      lock (List)
      {
         items = List.ToArray();
      }

      foreach (var item in items)
      {
         Console.WriteLine(item);
      }
   }
}