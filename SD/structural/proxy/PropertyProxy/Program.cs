namespace PropertyProxy;

internal static class Program
{
   private static void Main()
   {
      var creature = new Creature
      {
         AgilityProperty = 12
      };
      Console.WriteLine(creature);
   }
}