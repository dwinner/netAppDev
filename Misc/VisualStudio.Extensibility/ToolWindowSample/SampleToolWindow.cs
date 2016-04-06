//------------------------------------------------------------------------------
// <copyright file="SampleToolWindow.cs" company="Hewlett-Packard Company">
//     Copyright (c) Hewlett-Packard Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace ToolWindowSample
{
   /// <summary>
   ///    This class implements the tool window exposed by this package and hosts a user control.
   /// </summary>
   /// <remarks>
   ///    In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
   ///    usually implemented by the package implementer.
   ///    <para>
   ///       This class derives from the ToolWindowPane class provided from the MPF in order to use its
   ///       implementation of the IVsUIElementPane interface.
   ///    </para>
   /// </remarks>
   [Guid( "3d6db402-a3ed-49e6-9e85-caaabea381c3" )]
   public class SampleToolWindow : ToolWindowPane
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="SampleToolWindow" /> class.
      /// </summary>
      public SampleToolWindow( ) : base( null )
      {
         Caption = "SampleToolWindow";

         // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
         // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
         // the object returned by the Content property.
         Content = new SampleToolWindowControl();
      }
   }
}