namespace ImageViewer
{   
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
         Content = new ViewModels.ImageViewerViewModel();
      }
   }
}
