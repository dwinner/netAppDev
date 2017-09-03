using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using _03_ImageProcessorPipeline.Infrastructure;

namespace _03_ImageProcessorPipeline.ViewModels
{
   public sealed class ImageViewerViewModel
   {
      private readonly DelegateCommand _done;
      private readonly ImageProcessor _imageProcessor;

      public ImageViewerViewModel()
      {
         GreyImages = new ObservableCollection<BitmapSource>();
         _imageProcessor = new ImageProcessor();
         _imageProcessor.ProducedGrayScaleImage += (sender, args) => GreyImages.Add(args.Image);
         AddImage = new DelegateCommand(ProcessImage);
         AddDirectory = new DelegateCommand(ProcessDirectory);
         _done = new DelegateCommand(EndProcessing);
      }

      public string ImageDirectory { get; set; }

      public ICommand Done => _done;

      public string ImageFileName { get; set; }

      public ICommand AddImage { get; set; }

      public ICommand AddDirectory { get; set; }

      public ObservableCollection<BitmapSource> GreyImages { get; set; }

      private void EndProcessing(object obj)
      {
         _done.IsCanExecute = false;
         _imageProcessor.ShutdownAsync().ConfigureAwait(true);
      }

      private void ProcessDirectory(object obj) => _imageProcessor.ProcessDirectory(ImageDirectory);

      private void ProcessImage(object obj) => _imageProcessor.ProcessFile(ImageFileName);
   }
}