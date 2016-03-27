using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MVVM;

namespace ImageViewer.ViewModels
{
   public class ImageViewerViewModel
   {
      private readonly DelegateCommand _done;
      private readonly ImageProcessor _imageProcessor = new ImageProcessor();

      public ImageViewerViewModel()
      {
         GreyImages = new ObservableCollection<BitmapSource>();
         _imageProcessor.ProducedGrayScaleImage += (s, e) => GreyImages.Add(e.Img);
         AddImage = new DelegateCommand(ProcessImage);
         AddDirectory = new DelegateCommand(ProcessDirectory);
         _done = new DelegateCommand(EndProcessing);
      }

      public ObservableCollection<BitmapSource> GreyImages { get; }

      public ICommand AddImage { get; set; }
      public ICommand AddDirectory { get; set; }
      public ICommand Done => _done;

      public string ImageFilename { get; set; }
      public string ImageDirectory { get; set; }

      private async void EndProcessing(object obj)
      {
         _done.SetCanExecute(false);
         await _imageProcessor.ShutdownAsync();
      }

      private void ProcessDirectory(object obj)
      {
         _imageProcessor.ProcessDirectory(ImageDirectory);
      }

      private void ProcessImage(object obj)
      {
         _imageProcessor.ProcessFile(ImageFilename);
      }
   }
}