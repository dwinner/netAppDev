//------------------------------------------------------------------------------
// <copyright file="CodeMetricsViewportAdornment.cs" company="Hewlett-Packard Company">
//     Copyright (c) Hewlett-Packard Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Windows.Controls;
using Microsoft.VisualStudio.Text.Editor;

namespace CodeMetricsAdornment
{
   /// <summary>
   ///    Adornment class that draws a square box in the top right hand corner of the viewport
   /// </summary>
   internal sealed class CodeMetricsViewportAdornment
   {
      /// <summary>
      ///    The layer for the adornment.
      /// </summary>
      private readonly IAdornmentLayer _adornmentLayer;

      private readonly CodeMetricsDisplayControl _displayControl = new CodeMetricsDisplayControl();

      /// <summary>
      ///    Text _view to add the adornment on.
      /// </summary>
      private readonly IWpfTextView _view;

      /// <summary>
      ///    Initializes a new instance of the <see cref="CodeMetricsViewportAdornment" /> class.
      ///    Creates a square _image and attaches an event handler to the layout changed event that
      ///    adds the the square in the upper right-hand corner of the TextView via the adornment layer
      /// </summary>
      /// <param name="view">The <see cref="IWpfTextView" /> upon which the adornment will be drawn</param>
      public CodeMetricsViewportAdornment(IWpfTextView view)
      {
         if (view == null)
         {
            throw new ArgumentNullException(nameof(view));
         }

         _view = view;
         _displayControl.LinesOfCode = Utilities.CountLoc(view);
         _displayControl.CommentLines = Utilities.CountCommentLines(view);
         _displayControl.WhitespaceLines = Utilities.CountWhitespaceLines(view);
         _adornmentLayer = view.GetAdornmentLayer(nameof(CodeMetricsViewportAdornment));
         _view.ViewportHeightChanged += OnSizeChanged;
         _view.ViewportWidthChanged += OnSizeChanged;
         _view.LayoutChanged += OnLayoutChanged;
      }

      private void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
      {
         _displayControl.LinesOfCode = Utilities.CountLoc(_view);
         _displayControl.CommentLines = Utilities.CountCommentLines(_view);
         _displayControl.WhitespaceLines = Utilities.CountWhitespaceLines(_view);
      }

      /// <summary>
      ///    Event handler for viewport height or width changed events. Adds adornment at the top right corner of the viewport.
      /// </summary>
      /// <param name="sender">Event sender</param>
      /// <param name="e">Event arguments</param>
      private void OnSizeChanged(object sender, EventArgs e)
      {
         // Clear the adornment layer of previous adornments
         _adornmentLayer.RemoveAllAdornments();
         const int buffer = 50;

         // Place the control in the top right hand corner of the Viewport
         Canvas.SetLeft(_displayControl, _view.ViewportRight - (_displayControl.ActualWidth + buffer));
         Canvas.SetTop(_displayControl, _view.ViewportTop + (_displayControl.ActualHeight + buffer));

         // Add the control to the adornment layer and make it relative to the viewport
         _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, _displayControl, null);
      }
   }
}