//------------------------------------------------------------------------------
// <copyright file="AccessingLoadedSolution.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace AccessingSolutionObjects.Commands
{
   /// <summary>
   ///    Command handler
   /// </summary>
   internal sealed class AccessingLoadedSolution
   {
      /// <summary>
      ///    Command ID.
      /// </summary>
      public const int CommandId = 0x0200;

      /// <summary>
      ///    VS Package that provides this command, not null.
      /// </summary>
      private readonly Package _package;

      /// <summary>
      ///    Initializes a new instance of the <see cref="AccessingLoadedSolution" /> class.
      ///    Adds our command handlers for menu (commands must exist in the command table file)
      /// </summary>
      /// <param name="package">Owner _package, not null.</param>
      private AccessingLoadedSolution(Package package)
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
            var menuItem = new MenuCommand(OnAccessSolutionObjects, menuCommandId);
            commandService.AddCommand(menuItem);
         }
      }

      /// <summary>
      ///    Gets the instance of the command.
      /// </summary>
      public static AccessingLoadedSolution Instance { get; private set; }

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
         Instance = new AccessingLoadedSolution(package);
      }
      
      private void OnAccessSolutionObjects(object sender, EventArgs e)
      {
         var devenvImpl = (DTE) ServiceProvider.GetService(typeof (DTE));
         var solution = devenvImpl.Solution;
         if (solution != null && solution.IsOpen)
         {
            var solutionName = solution.FullName;
            var projects = solution.Projects.Cast<Project>().ToArray();
            var projectNames = projects.Select(project => project.FullName).ToList();
            var messageBuilder = new StringBuilder($"Loaded solution: {solutionName}");
            foreach (var projectName in projectNames)
            {
               messageBuilder.AppendLine($"Loaded project: {projectName}");
            }

            VsShellUtilities.ShowMessageBox(
               ServiceProvider,
               messageBuilder.ToString(),
               "Solution information",
               OLEMSGICON.OLEMSGICON_INFO,
               OLEMSGBUTTON.OLEMSGBUTTON_OK,
               OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
         }
      }
   }
}