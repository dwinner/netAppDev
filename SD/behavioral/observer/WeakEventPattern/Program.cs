// an event subscription can lead to a memory
// leak if you hold on to it past the object's
// lifetime

// weak events help with this

using static System.Console;

namespace WeakEventPattern;

internal static class Program
{
   private static void Main()
   {
      var btn = new Button();
      var window = new Window2(btn);
      //var window = new Window(btn);
      var windowRef = new WeakReference(window);
      btn.Fire();

      WriteLine("Setting window to null");
      window = null;

      FireGc();
      WriteLine($"Is window alive after GC? {windowRef.IsAlive}");

      btn.Fire();

      WriteLine("Setting button to null");
      btn = null;

      FireGc();
   }

   private static void FireGc()
   {
      WriteLine("Starting GC");
      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();
      WriteLine("GC is done!");
   }
}