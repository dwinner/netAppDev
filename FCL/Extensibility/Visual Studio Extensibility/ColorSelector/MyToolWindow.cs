//------------------------------------------------------------------------------
// <copyright file="MyToolWindow.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace ColorSelector
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
   [Guid("b79b1b11-f55c-4b87-9f5e-5e41d7ac89ee")]
   public sealed class MyToolWindow : ToolWindowPane
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="MyToolWindow" /> class.
      /// </summary>
      public MyToolWindow() : base(null)
      {
         Caption = "Color selector tool window";
         BitmapResourceID = 301;
         BitmapIndex = 1;
         Content = new MyToolWindowControl();
      }
   }
}