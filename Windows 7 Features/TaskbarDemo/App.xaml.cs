using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Shell;

namespace TaskBarDemo
{
   public partial class App
   {
      private void Application_Startup(object sender, StartupEventArgs e) // Настройка JumpList
      {
         var jumpItems = new List<JumpTask>();
         var workingDirectory = Environment.CurrentDirectory;
         var windowsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
         var notepadPath = Path.Combine(windowsDirectory, "Notepad.exe");
         jumpItems.Add(new JumpTask
         {
            CustomCategory = "Read me",
            Title = "Read me",
            Description = "Open readme in notepad",
            ApplicationPath = notepadPath,
            IconResourcePath = notepadPath,
            WorkingDirectory = workingDirectory,
            Arguments = "readme.txt"
         });

         var jumpList = new JumpList(jumpItems, true, true);
         jumpList.JumpItemsRejected += (o, args) =>
         {
            var stringBuilder = new StringBuilder();
            foreach (var item in args.RejectedItems)
            {
               var jumpPath = item as JumpPath;
               if (jumpPath != null)
               {
                  stringBuilder.Append(jumpPath.Path);
               }

               var jumpTask = item as JumpTask;
               if (jumpTask != null)
               {
                  stringBuilder.Append(jumpTask.ApplicationPath);
               }

               stringBuilder.AppendLine();
            }

            MessageBox.Show(stringBuilder.ToString());
         };

         JumpList.SetJumpList(Current, jumpList);
      }
   }
}