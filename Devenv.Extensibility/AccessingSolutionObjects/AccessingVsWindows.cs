//------------------------------------------------------------------------------
// <copyright file="EnumerateObjectsCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace AccessingSolutionObjects
{
   /// <summary>
   ///    Command handler
   /// </summary>
   internal sealed class AccessingVsWindows
   {
      /// <summary>
      ///    Command ID.
      /// </summary>
      public const int CommandId = 0x0100;      

      /// <summary>
      ///    VS Package that provides this command, not null.
      /// </summary>
      private readonly Package _package;

      /// <summary>
      ///    Initializes a new instance of the <see cref="AccessingVsWindows" /> class.
      ///    Adds our command handlers for menu (commands must exist in the command table file)
      /// </summary>
      /// <param name="package">Owner _package, not null.</param>
      private AccessingVsWindows(Package package)
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
            var menuItem = new MenuCommand(OnAccessVsWindows, menuCommandId);
            commandService.AddCommand(menuItem);
         }
      }

      /// <summary>
      ///    Gets the instance of the command.
      /// </summary>
      public static AccessingVsWindows Instance { get; private set; }

      /// <summary>
      ///    Gets the service provider from the owner _package.
      /// </summary>
      private IServiceProvider ServiceProvider => _package;

      /// <summary>
      ///    Initializes the singleton instance of the command.
      /// </summary>
      /// <param name="package">Owner _package, not null.</param>
      public static void Initialize(Package package)
      {
         Instance = new AccessingVsWindows(package);
      }

      /// <summary>
      ///    This function is the callback used to execute the command when the menu item is clicked.
      ///    See the constructor to see how the menu item is associated with this function using
      ///    OleMenuCommandService service and MenuCommand class.
      /// </summary>
      /// <param name="sender">Event sender.</param>
      /// <param name="e">Event args.</param>
      private void OnAccessVsWindows(object sender, EventArgs e)
      {
         var devenvImpl = (DTE) ServiceProvider.GetService(typeof (DTE));
         if (devenvImpl != null)
         {
            var windows = devenvImpl.Windows;
            var count = windows.Count;
            string results = $"{count} windows open... {Environment.NewLine}";

            // Iterate the collection of windows
            for (var i = 1; i <= count; i++)
            {
               var window = windows.Item(i);
               var title = window.Caption;

               // If the window is hosting a document, a valid Document object will be returned through Window.Document
               results += window.Document != null
                  ? $"Window '{title}' is a document window{Environment.NewLine}"
                  : $"Window '{title}' is a tool window{Environment.NewLine}";
            }

            VsShellUtilities.ShowMessageBox(
               ServiceProvider,
               results,
               "Window dump",
               OLEMSGICON.OLEMSGICON_INFO,
               OLEMSGBUTTON.OLEMSGBUTTON_OK,
               OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
         }
      }
   }
}