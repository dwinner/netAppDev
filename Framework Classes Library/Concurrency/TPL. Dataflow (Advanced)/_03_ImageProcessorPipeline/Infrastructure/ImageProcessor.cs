using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Media.Imaging;

namespace _03_ImageProcessorPipeline.Infrastructure
{
   public sealed class ImageProcessor
   {
      private readonly Func<string, IEnumerable<string>> _findImagesFunc = dir =>
      {
         var dirInfo = new DirectoryInfo(dir);
         return dirInfo.GetFiles(".jpg")
            .Select(fileInfo => fileInfo.FullName)
            .Concat(dirInfo.GetDirectories()
               .Select(info => info.FullName));
      };

      private readonly Func<string, BitmapSource> _loadAndToGrayScaleFunc =
         imagePath => new BitmapImage(new Uri(imagePath)).ToGrayScale();

      private readonly TransformManyBlock<string, string> _imageCollectionBlock;
      private readonly TransformBlock<string, BitmapSource> _loadAndToGreyBlock;
      private readonly ActionBlock<BitmapSource> _publishImageBlock;

      public ImageProcessor()
      {
         // Creating the dataflow blocks
         _imageCollectionBlock = new TransformManyBlock<string, string>(_findImagesFunc);
         _loadAndToGreyBlock = new TransformBlock<string, BitmapSource>(_loadAndToGrayScaleFunc,
            new ExecutionDataflowBlockOptions {MaxDegreeOfParallelism = 4});
         _publishImageBlock = new ActionBlock<BitmapSource>((Action<BitmapSource>) PublishImage,
            new ExecutionDataflowBlockOptions {TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()});

         // Linking the dataflow blocks
         _imageCollectionBlock.LinkTo(_loadAndToGreyBlock, file => file.EndsWith(".jpg"));
         _imageCollectionBlock.LinkTo(_imageCollectionBlock); // NOTE: ?!
         _loadAndToGreyBlock.LinkTo(_publishImageBlock);
      }

      public event EventHandler<ProcessedImageEventArgs> ProducedGrayScaleImage;

      private void PublishImage(BitmapSource bitmapSource) =>
         OnProducedGrayScaleImage(new ProcessedImageEventArgs(bitmapSource));

      public void ProcessFile(string fileName) => _loadAndToGreyBlock.Post(fileName);

      public void ProcessDirectory(string dir) => _imageCollectionBlock.Post(dir);

      private void OnProducedGrayScaleImage(ProcessedImageEventArgs e) => ProducedGrayScaleImage?.Invoke(this, e);

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