//------------------------------------------------------------------------------
// <copyright file="AccessingSlnObjectsPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.Internal.VisualStudio.Shell;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace AccessingSolutionObjects
{   
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(GuidList.PackageGuidString)]
   [ProvideMenuResource("Menus.ctmenu", 1)]
   public sealed class AccessingSlnObjectsPackage :
      Package,
      IVsShellPropertyEvents,
      IVsSolutionEvents,
      IVsUpdateSolutionEvents2      
   {
      private IVsSolution2 _solution;
      private uint _solutionEventsCookie;

      public int OnShellPropertyChange(int propid, object var) => VSConstants.S_OK;

      public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded) => VSConstants.S_OK;

      public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel) => VSConstants.S_OK;

      public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved) => VSConstants.S_OK;

      public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
      {
         object projectObject;
         pStubHierarchy.GetProperty(
            (uint) VSConstants.VSITEMID.Root, (int) __VSHPROPID.VSHPROPID_Name, out projectObject);
         // FIXME: Call out output window and write the name of the project
         var projectName = projectObject as string;

         return VSConstants.S_OK;
      }

      public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel) => VSConstants.S_OK;

      public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy) => VSConstants.S_OK;

      public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution) => VSConstants.S_OK;

      public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel) => VSConstants.S_OK;

      public int OnBeforeCloseSolution(object pUnkReserved) => VSConstants.S_OK;

      public int OnAfterCloseSolution(object pUnkReserved) => VSConstants.S_OK;

      int IVsUpdateSolutionEvents.UpdateSolution_Begin(ref int pfCancelUpdate) => VSConstants.S_OK;

      int IVsUpdateSolutionEvents2.UpdateSolution_Done(int fSucceeded, int fModified, int fCancelCommand)
         => VSConstants.S_OK;

      int IVsUpdateSolutionEvents2.UpdateSolution_StartUpdate(ref int pfCancelUpdate) => VSConstants.S_OK;

      int IVsUpdateSolutionEvents2.UpdateSolution_Cancel() => VSConstants.S_OK;

      int IVsUpdateSolutionEvents2.OnActiveProjectCfgChange(IVsHierarchy pIVsHierarchy) => VSConstants.S_OK;

      public int UpdateProjectCfg_Begin(IVsHierarchy pHierProj, IVsCfg pCfgProj, IVsCfg pCfgSln, uint dwAction,
         ref int pfCancel) => VSConstants.S_OK;

      public int UpdateProjectCfg_Done(IVsHierarchy pHierProj, IVsCfg pCfgProj, IVsCfg pCfgSln, uint dwAction,
         int fSuccess, int fCancel) => VSConstants.S_OK;

      int IVsUpdateSolutionEvents2.UpdateSolution_Begin(ref int pfCancelUpdate) => VSConstants.S_OK;

      int IVsUpdateSolutionEvents.UpdateSolution_Done(int fSucceeded, int fModified, int fCancelCommand)
         => VSConstants.S_OK;

      int IVsUpdateSolutionEvents.UpdateSolution_StartUpdate(ref int pfCancelUpdate) => VSConstants.S_OK;

      int IVsUpdateSolutionEvents.UpdateSolution_Cancel() => VSConstants.S_OK;

      int IVsUpdateSolutionEvents.OnActiveProjectCfgChange(IVsHierarchy pIVsHierarchy) => VSConstants.S_OK;      

      /// <summary>
      ///    Initialization of the package; this method is called right after the package is sited, so this is the place
      ///    where you can put all the initialization code that rely on services provided by VisualStudio.
      /// </summary>
      protected override void Initialize()
      {
         base.Initialize();
         EnumerateObjectsCommand.Initialize(this);         
         _solution = ServiceProvider.GlobalProvider.GetService(typeof(SVsSolution)) as IVsSolution2;
         if (_solution != null)
         {
            _solution.AdviseSolutionEvents(this, out _solutionEventsCookie);
         }

         var devenvImpl = (DTE) GetService(typeof (DTE));
         var loadedSolution = devenvImpl.Solution;
         if (loadedSolution != null)
         {
            var solutionName = loadedSolution.FullName;
            //var solutionFileName = loadedSolution.FileName;

            var projects = loadedSolution.Projects.Cast<Project>();
            var duckProjectList = new List<dynamic>();
            foreach (var project in projects)
            {
               var projectFullName = project.FullName;
               var projectFileName = project.FileName;
               duckProjectList.Add(new { projectFullName, projectFileName });
            }
         }
      }

      protected override void Dispose(bool disposing)
      {
         base.Dispose(disposing);

         // Unadvise events
         if (_solution != null && _solutionEventsCookie != 0)
         {
            _solution.UnadviseSolutionEvents(_solutionEventsCookie);
         }
      }
   }
}