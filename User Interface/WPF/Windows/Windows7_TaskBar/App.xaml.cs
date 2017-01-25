using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Shell;

namespace Windows7_TaskBar
{
   public partial class App
   {
      private void Application_Startup(object sender, StartupEventArgs e)
      {
         // Retrieve the current jump list.
         var jumpList = new JumpList();
         JumpList.SetJumpList(Current, jumpList);

         // Add a new JumpPath for a file in the application folder.
         var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
         if (path != null)
         {
            path = Path.Combine(path, "readme.txt");
            if (File.Exists(path))
            {
               var jumpTask = new JumpTask
               {
                  CustomCategory = "Documentation",
                  Title = "Read the readme.txt",
                  ApplicationPath = @"c:\windows\notepad.exe",
                  IconResourcePath = @"c:\windows\notepad.exe",
                  Arguments = path
               };

               jumpList.JumpItems.Add(jumpTask);
            }
         }

         // Update the jump list.
         jumpList.Apply();
      }
   }
}