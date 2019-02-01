using System;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace AccessingSolutionObjects.Commands
{
   internal sealed class AccessingTextDocument
   {
      public const int CommandId = 0x0300;
      private readonly Package _package;

      private AccessingTextDocument(Package package)
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
            var menuItem = new MenuCommand(OnAccessDocument, menuCommandId);
            commandService.AddCommand(menuItem);
         }
      }

      public IServiceProvider ServiceProvider => _package;

      public static AccessingTextDocument Instance { get; private set; }

      public static void Initialize(Package package) => Instance = new AccessingTextDocument(package);

      private void OnAccessDocument(object sender, EventArgs e)
      {
         var vsImpl = (DTE) ServiceProvider.GetService(typeof (DTE));

         // Grab references to the active window; we assume, for this example, that the window is a text window
         var window = vsImpl.ActiveWindow;

         // Grab a TextWindow instance that maps to our active window
         var txtWindow = (TextWindow) window.Object;

         // Get the active pane from the text window
         var pane = txtWindow.ActivePane;

         // Select active line
         pane.Selection.SelectLine();
      }
   }
}