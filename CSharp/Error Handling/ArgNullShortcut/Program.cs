using System.Diagnostics;

namespace ArgNullShortcut;

internal class Program
{
   private static void Main(string[] args)
   {
      try
      {
         Display(null);
      }
      catch (ArgumentNullException ex)
      {
         Debug.WriteLine(ex.Message);
      }
   }

   private static void Display(string name)
   {
      ArgumentNullException.ThrowIfNull(name);
      Console.WriteLine(name);
   }
}