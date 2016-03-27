/**
 * Блоки трансформации
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageViewer
{
   public class ImageProcessor
   {
      private readonly TransformManyBlock<string, string> _imageCollectionBlock;
      private readonly TransformBlock<string, BitmapSource> _loadAndToGreyBlock;
      private readonly ActionBlock<BitmapSource> _publishImageBlock;

      public ImageProcessor()
      {
         _imageCollectionBlock =
            new TransformManyBlock<string, string>((Func<string, IEnumerable<string>>) FindImagesInDirectory);
         _loadAndToGreyBlock = new TransformBlock<string, BitmapSource>(
            (Func<string, BitmapSource>) LoadAndToGrayScale,
            new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = 4});
         _publishImageBlock = new ActionBlock<BitmapSource>((Action<BitmapSource>) PublishImage,
            new ExecutionDataflowBlockOptions {TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()});

         _imageCollectionBlock.LinkTo(_loadAndToGreyBlock, u => u.EndsWith(".jpg"));
         _imageCollectionBlock.LinkTo(_imageCollectionBlock);
         _loadAndToGreyBlock.LinkTo(_publishImageBlock);
      }

      public event EventHandler<ProcessedImageEventArgs> ProducedGrayScaleImage = delegate { };

      public void ProcessFile(string filename) => _loadAndToGreyBlock.Post(filename);

      public void ProcessDirectory(string dir) => _imageCollectionBlock.Post(dir);

      private IEnumerable<string> FindImagesInDirectory(string directory)
      {
         var dir = new DirectoryInfo(directory);
         return dir
            .GetFiles("*.jpg")
            .Select(file => file.FullName)
            .Concat(dir.GetDirectories().Select(di => di.FullName));
      }

      private void PublishImage(BitmapSource img) => ProducedGrayScaleImage(this, new ProcessedImageEventArgs(img));

      private static BitmapSource LoadAndToGrayScale(string imagePath)
      {
         var img = new BitmapImage(new Uri(imagePath));
         return ToGrayScale(img);
      }

      private static BitmapSource ToGrayScale(BitmapSource bitmapSource)
      {
         var img = bitmapSource;

         var width = (int) img.Width;
         var height = (int) img.Height;
         var dpi = img.DpiX;

         var pixelData = new byte[(int) img.Width*(int) img.Height*4];
         var outputData = new byte[width*height];
         img.CopyPixels(pixelData, (int) img.Width*4, 0);

         for (var i = 0; i < 1; i++)
         {
            for (var x = 0; x < img.Width; x++)
            {
               for (var y = 0; y < img.Height; y++)
               {
                  var red = pixelData[y*(int) img.Width*4 + x*4 + 1];
                  var green = pixelData[y*(int) img.Width*4 + x*4 + 2];
                  var blue = pixelData[y*(int) img.Width*4 + x*4 + 3];

                  var gray = (byte) (red*0.299 + green*0.587 + blue*0.114);

                  outputData[y*width + x] = gray;
               }
            }
         }

         var grey = BitmapSource.Create(width, height, dpi, dpi, PixelFormats.Gray8, null, outputData, width);
         grey.Freeze();

         return grey;
      }

      public async Task ShutdownAsync()
      {
         _imageCollectionBlock.Complete();
         await _imageCollectionBlock.Completion.ConfigureAwait(false);

         _loadAndToGreyBlock.Complete();         
         await _loadAndToGreyBlock.Completion.ConfigureAwait(false);

         _publishImageBlock.Complete();
         await _publishImageBlock.Completion.ConfigureAwait(false);

         ProducedGrayScaleImage = null;
      }
   }
}