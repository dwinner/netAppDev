using System.Reflection;
using System.Windows;
using System.Windows.Shell;

namespace JumpListApplicationTask
{
   public class WpfApp : Application
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         // Retrieve the current jump list.
         var jumpList = new JumpList();
         JumpList.SetJumpList(Current, jumpList);

         // Add a new JumpPath for an application task.            
         var jumpTask = new JumpTask
         {
            CustomCategory = "Tasks",
            Title = "Do Something",
            ApplicationPath = Assembly.GetExecutingAssembly().Location
         };
         jumpTask.IconResourcePath = jumpTask.ApplicationPath;
         jumpTask.Arguments = "@#StartOrder";
         jumpList.JumpItems.Add(jumpTask);

         // Update the jump list.
         jumpList.Apply();

         // Load the main window.
         var win = new Window1();
         win.Show();
      }

      public static void ProcessMessage(string message)
      {
         MessageBox.Show(string.Format("Message {0} received.", message));
      }
   }
}