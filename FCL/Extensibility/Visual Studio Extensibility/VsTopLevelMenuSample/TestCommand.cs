﻿//------------------------------------------------------------------------------
// <copyright file="TestCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace VsTopLevelMenuSample
{
   /// <summary>
   ///    Command handler
   /// </summary>
   internal sealed class TestCommand
   {
      /// <summary>
      ///    Command ID.
      /// </summary>
      public const int CommandId = 0x0100;

      /// <summary>
      ///    Command menu group (command set GUID).
      /// </summary>
      public static readonly Guid CommandSet = new Guid("d7166fd5-38de-4c2e-b695-5a2555369e5a");

      /// <summary>
      ///    VS Package that provides this command, not null.
      /// </summary>
      private readonly Package package;

      /// <summary>
      ///    Initializes a new instance of the <see cref="TestCommand" /> class.
      ///    Adds our command handlers for menu (commands must exist in the command table file)
      /// </summary>
      /// <param name="package">Owner package, not null.</param>
      private TestCommand(Package package)
      {
         if (package == null)
         {
            throw new ArgumentNullException("package");
         }

         this.package = package;

         var commandService = ServiceProvider.GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
         if (commandService != null)
         {
            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
            commandService.AddCommand(menuItem);
         }
      }

      /// <summary>
      ///    Gets the instance of the command.
      /// </summary>
      public static TestCommand Instance { get; private set; }

      /// <summary>
      ///    Gets the service provider from the owner package.
      /// </summary>
      private IServiceProvider ServiceProvider
      {
         get { return package; }
      }

      /// <summary>
      ///    Initializes the singleton instance of the command.
      /// </summary>
      /// <param name="package">Owner package, not null.</param>
      public static void Initialize(Package package)
      {
         Instance = new TestCommand(package);
      }

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
         var title = "TestCommand";

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