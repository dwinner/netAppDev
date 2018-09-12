using System.Windows;
using AppDevUnited.SelfTester.UI;

namespace AppDevUnited.SelfTester.Runner
{
   public class SelfTesterApplication : Application
   {
      public Window SelfTesterMainWindow { get; set; }

      public SelfTesterApplication()
      {
         SelfTesterMainWindow = new MainWindow();
      }
   }
}