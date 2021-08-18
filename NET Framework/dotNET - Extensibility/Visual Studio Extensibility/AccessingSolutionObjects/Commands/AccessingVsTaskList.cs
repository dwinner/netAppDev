using System;
using System.ComponentModel.Design;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Constants = EnvDTE.Constants;

namespace AccessingSolutionObjects.Commands
{
   internal sealed class AccessingVsTaskList
   {
      public const int CommandId = 0x0400;
      private readonly Package _package;

      private AccessingVsTaskList(Package package)
      {
         if (package == null)
         {
            throw new ArgumentNullException(nameof(package));
         }

         _package = package;
         var commandService = ServiceProvider.GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
         if (commandService != null)
         {
            var menuCommandId = new CommandID(GuidList.TopMenuGroupGuid, CommandId);
            var menuItem = new MenuCommand(OnAccessTaskList, menuCommandId);
            commandService.AddCommand(menuItem);
         }
      }

      public IServiceProvider ServiceProvider => _package;

      public static AccessingVsTaskList Instance { get; private set; }

      private void OnAccessTaskList(object sender, EventArgs e)
      {
         var vsImpl = (DTE) ServiceProvider.GetService(typeof (DTE));
         var vsWindows = vsImpl.Windows;
         var taskListWindow = vsWindows.Item(Constants.vsWindowKindTaskList);
         taskListWindow.Visible = true;
         taskListWindow.Activate();
         var taskList = (TaskList) taskListWindow.Object;
         //taskList.TaskItems.Add(
         //   "Best practices",
         //   "Coding Style",
         //   "Use of brace indenting is inconsistent",
         //   vsTaskPriority.vsTaskPriorityMedium,
         //   vsTaskIcon.vsTaskIconUser,
         //   true,
         //   "Class1.cs",
         //   7);
         var taskItems = taskList.TaskItems.Cast<TaskItem>()
            .Aggregate(string.Empty, (current, taskItem) => current + $"{taskItem.Description}: {taskItem.Line}");

         VsShellUtilities.ShowMessageBox(
               ServiceProvider,
               taskItems,
               "Task items",
               OLEMSGICON.OLEMSGICON_INFO,
               OLEMSGBUTTON.OLEMSGBUTTON_OK,
               OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
      }

      public static void Initialize(Package package) => Instance = new AccessingVsTaskList(package);
   }
}