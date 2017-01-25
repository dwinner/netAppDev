using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Windows.Resources
{
   public partial class CustomWindowChrome
   {
      private bool _isResizing;
      private ResizeType _resizeType;

      public CustomWindowChrome()
      {
         InitializeComponent();
      }

      private void OnInitiateResizeWe(object sender, MouseEventArgs e)
      {
         _isResizing = true;
         _resizeType = ResizeType.Width;
      }

      private void OnInitiateResizeNs(object sender, MouseEventArgs e)
      {
         _isResizing = true;
         _resizeType = ResizeType.Height;
      }

      private void OnEndResize(object sender, MouseEventArgs e)
      {
         _isResizing = false;

         // Make sure capture is released.
         var rect = (Rectangle) sender;
         rect.ReleaseMouseCapture();
      }

      private void OnResize(object sender, MouseEventArgs e)
      {
         var rect = (Rectangle) sender;
         var win = (Window) rect.TemplatedParent;

         if (_isResizing)
         {
            rect.CaptureMouse();
            if (_resizeType == ResizeType.Width)
            {
               var width = e.GetPosition(win).X + 5;
               if (width > 0)
               {
                  win.Width = width;
               }
            }

            if (_resizeType == ResizeType.Height)
            {
               var height = e.GetPosition(win).Y + 5;
               if (height > 0)
               {
                  win.Height = height;
               }
            }
         }
      }

      private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         var win = (Window) ((FrameworkElement) sender).TemplatedParent;
         win.DragMove();
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         var win = (Window) ((FrameworkElement) sender).TemplatedParent;
         win.Close();
      }

      [Flags]
      private enum ResizeType
      {
         Width,
         Height
      }
   }
}