//------------------------------------------------------------------------------
// <copyright file="MetabotPackage.cs" company="Hewlett-Packard Company">
//     Copyright (c) Hewlett-Packard Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;

namespace VsPackageSample
{
   /// <summary>
   ///    This is the class that implements the package exposed by this assembly.
   /// </summary>
   /// <remarks>
   ///    <para>
   ///       The minimum requirement for a class to be considered a valid package for Visual Studio
   ///       is to implement the IVsPackage interface and register itself with the shell.
   ///       This package uses the helper classes defined inside the Managed Package Framework (MPF)
   ///       to do it: it derives from the Package class that provides the implementation of the
   ///       IVsPackage interface and uses the registration attributes defined in the framework to
   ///       register itself and its components with the shell. These attributes tell the pkgdef creation
   ///       utility what data to put into .pkgdef file.
   ///    </para>
   ///    <para>
   ///       To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt;
   ///       in .vsixmanifest file.
   ///    </para>
   /// </remarks>
   [PackageRegistration(UseManagedResourcesOnly = true)]
   [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
   [Guid(PackageGuidString)]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
      Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
   public sealed class MetabotPackage : Package
   {
      /// <summary>
      ///    MetabotPackage GUID string.
      /// </summary>
      private const string PackageGuidString = "eb90b7f4-f7d7-4a6d-a716-3c1cecdb7576";

      #region Package Members

      /// <summary>
      ///    Initialization of the package; this method is called right after the package is sited, so this is the place
      ///    where you can put all the initialization code that rely on services provided by VisualStudio.
      /// </summary>
      protected override void Initialize()
      {
         var outputWindowPane = VsShellUtilities.GetOutputWindowPane(this, Guid.Parse(PackageGuidString));
         var activatedCode = outputWindowPane.Activate();
         if (activatedCode == VSConstants.S_OK)
         {
            outputWindowPane.OutputString("Hello, from package");
         }

         base.Initialize();
      }

      #endregion
   }
}