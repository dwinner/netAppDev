﻿//------------------------------------------------------------------------------
// <copyright file="CodeMetricsViewportAdornmentTextViewCreationListener.cs" company="Hewlett-Packard Company">
//     Copyright (c) Hewlett-Packard Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace CodeMetricsAdornment
{
   /// <summary>
   ///    Establishes an <see cref="IAdornmentLayer" /> to place the adornment on and exports the
   ///    <see cref="IWpfTextViewCreationListener" />
   ///    that instantiates the adornment on the event of a <see cref="IWpfTextView" />'s creation
   /// </summary>
   [Export(typeof(IWpfTextViewCreationListener))]
   [ContentType("text")]
   [TextViewRole(PredefinedTextViewRoles.Document)]
   internal sealed class CodeMetricsViewportAdornmentTextViewCreationListener : IWpfTextViewCreationListener
   {
      // Disable "Field is never assigned to..." and "Field is never used" compiler's warnings. Justification: the field is used by MEF.
#pragma warning disable 649, 169

      /// <summary>
      ///    Defines the adornment layer for the scarlet adornment. This layer is ordered
      ///    after the selection layer in the Z-order
      /// </summary>
      [Export(typeof(AdornmentLayerDefinition))]
      [Name(nameof(CodeMetricsViewportAdornment))]
      [Order(After = PredefinedAdornmentLayers.Caret)]
      private AdornmentLayerDefinition _editorAdornmentLayer;

#pragma warning restore 649, 169

      /// <summary>
      ///    Instantiates a CodeMetricsViewportAdornment manager when a textView is created.
      /// </summary>
      /// <param name="textView">The <see cref="IWpfTextView" /> upon which the adornment should be placed</param>
      public void TextViewCreated(IWpfTextView textView)
      {
         // The adorment will get wired to the text view events
         new CodeMetricsViewportAdornment(textView);
      }
   }
}