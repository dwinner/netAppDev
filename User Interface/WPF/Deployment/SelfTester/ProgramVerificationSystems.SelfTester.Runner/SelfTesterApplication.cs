using ProgramVerificationSystems.SelfTester.UI;
using System.Windows;

namespace ProgramVerificationSystems.SelfTester.Runner
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