using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace WpfDirectX
{
   // Three PInvoke signatures for communicating with the unmanaged C++ DirectX Sample DLL

   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      protected override void OnSourceInitialized(EventArgs e)
      {
         base.OnSourceInitialized(e);
         // Now that we can get an HWND for the Window, force the initialization
         // that is otherwise done when the front buffer becomes available:
         d3dImage_IsFrontBufferAvailableChanged(this, new DependencyPropertyChangedEventArgs());
      }

      private void d3dImage_IsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
      {
         if (D3DImage.IsFrontBufferAvailable)
         {
            // (Re)initialization:
            var surface = NativeMethods.Initialize(new WindowInteropHelper(this).Handle, (int) Width, (int) Height);

            if (surface != IntPtr.Zero)
            {
               D3DImage.Lock();
               D3DImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface);
               D3DImage.Unlock();

               CompositionTarget.Rendering += CompositionTarget_Rendering;
            }
         }
         else
         {
            // Cleanup:
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
            NativeMethods.Cleanup();
         }
      }

      // Render the DirectX scene onto the D3DImage when WPF itself is ready to render
      private void CompositionTarget_Rendering(object sender, EventArgs e)
      {
         if (D3DImage.IsFrontBufferAvailable)
         {
            D3DImage.Lock();
            NativeMethods.Render();
            // Invalidate the whole area:
            D3DImage.AddDirtyRect(new Int32Rect(0, 0, D3DImage.PixelWidth, D3DImage.PixelHeight));
            D3DImage.Unlock();
         }
      }
   }
}