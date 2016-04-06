//------------------------------------------------------------------------------
// <copyright file="SampleToolWindowCommand.cs" company="Hewlett-Packard Company">
//     Copyright (c) Hewlett-Packard Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace ToolWindowSample
{
   /// <summary>
   ///    Command handler
   /// </summary>
   internal sealed class SampleToolWindowCommand
   {
      /// <summary>
      ///    Command ID.
      /// </summary>
      private const int CommandId = 0x0100;

      /// <summary>
      ///    Command menu group (command set GUID).
      /// </summary>
      private static readonly Guid CommandSet = new Guid("9b2d5972-4e5f-44c1-a6ea-13d6cec3dc2c");

      /// <summary>
      ///    VS Package that provides this command, not null.
      /// </summary>
      private readonly Package _package;

      /// <summary>
      ///    Initializes a new instance of the <see cref="SampleToolWindowCommand" /> class.
      ///    Adds our command handlers for menu (commands must exist in the command table file)
      /// </summary>
      /// <param name="package">Owner _package, not null.</param>
      private SampleToolWindowCommand(Package package)
      {
         if (package == null)
         {
            throw new ArgumentNullException(nameof(package));
         }

         _package = package;

         var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
         if (commandService != null)
         {
            var menuCommandId = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(ShowToolWindow, menuCommandId);
            commandService.AddCommand(menuItem);
         }
      }

      /// <summary>
      ///    Gets the instance of the command.
      /// </summary>
      private static SampleToolWindowCommand Instance { get; set; }

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
         Instance = new SampleToolWindowCommand(package);
      }

      /// <summary>
      ///    Shows the tool window when the menu item is clicked.
      /// </summary>
      /// <param name="sender">The event sender.</param>
      /// <param name="e">The event args.</param>
      private void ShowToolWindow(object sender, EventArgs e)
      {
         // Get the instance number 0 of this tool window. This window is single instance so this instance
         // is actually the only one.
         // The last flag is set to true so that if the tool window does not exists it will be created.
         var window = _package.FindToolWindow(typeof(SampleToolWindow), 0, true);
         if (window?.Frame == null)
         {
            throw new NotSupportedException("Cannot create tool window");
         }

         var windowFrame = (IVsWindowFrame)window.Frame;
         ErrorHandler.ThrowOnFailure(windowFrame.Show());
      }
   }
}