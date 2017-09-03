using _03_ImageProcessorPipeline.ViewModels;

namespace _03_ImageProcessorPipeline
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         Content = new ImageViewerViewModel();
      }
   }
}