//------------------------------------------------------------------------------
// <copyright file="HelloCommand.cs" company="Hewlett-Packard Company">
//     Copyright (c) Hewlett-Packard Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace HelloFromTheToolMenuSample
{
   /// <summary>
   ///    Command handler
   /// </summary>
   internal sealed class HelloCommand
   {
      /// <summary>
      ///    Command ID.
      /// </summary>
      private const int CommandId = 0x0100;

      /// <summary>
      ///    Command menu group (command set GUID).
      /// </summary>
      private static readonly Guid CommandSet = new Guid("c01497e2-bcc7-4830-a3f8-e9140a31408d");

      /// <summary>
      ///    VS Package that provides this command, not null.
      /// </summary>
      private readonly Package _package;

      /// <summary>
      ///    Initializes a new instance of the <see cref="HelloCommand" /> class.
      ///    Adds our command handlers for menu (commands must exist in the command table file)
      /// </summary>
      /// <param name="package">Owner _package, not null.</param>
      private HelloCommand(Package package)
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
            var menuItem = new MenuCommand(MenuItemCallback, menuCommandId);
            commandService.AddCommand(menuItem);
         }
      }

      /// <summary>
      ///    Gets the instance of the command.
      /// </summary>
      private static HelloCommand Instance { get; set; }

      /// <summary>
      ///    Gets the service provider from the owner _package.
      /// </summary>
      private IServiceProvider ServiceProvider => _package;

      /// <summary>
      ///    Initializes the singleton instance of the command.
      /// </summary>
      /// <param name="package">Owner _package, not null.</param>
      public static void Initialize(Package package) => Instance = new HelloCommand(package);

      /// <summary>
      ///    This function is the callback used to execute the command when the menu item is clicked.
      ///    See the constructor to see how the menu item is associated with this function using
      ///    OleMenuCommandService service and MenuCommand class.
      /// </summary>
      /// <param name="sender">Event sender.</param>
      /// <param name="e">Event args.</param>
      private void MenuItemCallback(object sender, EventArgs e)
      {
         var message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", GetType().FullName);
         const string title = "Command1";

         // Show a message box to prove we were here
         VsShellUtilities.ShowMessageBox(
            ServiceProvider,
            message,
            title,
            OLEMSGICON.OLEMSGICON_INFO,
            OLEMSGBUTTON.OLEMSGBUTTON_OK,
            OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
      }
   }
}