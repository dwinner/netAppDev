using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImageViewer
{
   public partial class ImageViewerWindow
   {
      public static readonly DependencyProperty ImageInfoProperty;

      static ImageViewerWindow()
      {
         ImageInfoProperty = DependencyProperty.Register("ImageInfo",
            typeof(ImageInfoViewModel), typeof(ImageViewerWindow));
      }

      public ImageViewerWindow()
      {
         InitializeComponent();
      }

      public ImageInfoViewModel ImageInfo
      {
         get { return (ImageInfoViewModel) GetValue(ImageInfoProperty); }
         set
         {
            SetValue(ImageInfoProperty, value);
            DataContext = value;
         }
      }

      protected override void OnDragEnter(DragEventArgs e)
      {
         base.OnDragEnter(e);         
         e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)
            ? DragDropEffects.Copy
            : DragDropEffects.None;
      }

      protected override void OnDrop(DragEventArgs e)
      {
         base.OnDrop(e);
         if (e.Data.GetDataPresent(DataFormats.FileDrop))
         {
            var dropData = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (dropData != null)
               foreach (var path in dropData)
                  try
                  {
                     var image = new BitmapImage(new Uri(path));
                     var model = new ImageInfoViewModel(image);
                     ImageInfo = model;
                  }
                  catch (Exception)
                  {
                     // ignored
                  }
         }
      }

      private void OnTemplateOptionChecked(object sender, RoutedEventArgs e)
      {
         if (RadioButtonNoCaption != null && ControlDisplay != null)
            ControlDisplay.Template = RadioButtonNoCaption.IsChecked == true
               ? (ControlTemplate) FindResource("ImageTemplate")
               : (ControlTemplate) FindResource("ImageWithCaptionTemplate");
      }
   }
}