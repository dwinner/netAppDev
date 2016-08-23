//------------------------------------------------------------------------------
// <copyright file="MyToolWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Windows;
using System.Windows.Input;
using static ColorSelector.UiUtilities;

namespace ColorSelector
{
   /// <summary>
   ///    Interaction logic for MyToolWindowControl.
   /// </summary>
   public partial class MyToolWindowControl
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="MyToolWindowControl" /> class.
      /// </summary>
      public MyToolWindowControl()
      {
         InitializeComponent();
      }

      private void PaletteImage_OnMouseMove(object sender, MouseEventArgs e)
      {
         // Get the color under the current pointer position         
         var color = GetPointColor(this);
         DisplayColor(RgbTextBlock, SelectedColorBorder, color);
         DisplayCode(CodeTextBlock, color, false);
      }

      private void PaletteImage_OnMouseDown(object sender, MouseButtonEventArgs e)
      {
         // On mouse click within the palette, copy the Color code to the Clipboard
         Clipboard.Clear();
         Clipboard.SetText(CodeTextBlock.Text);
      }
   }
}