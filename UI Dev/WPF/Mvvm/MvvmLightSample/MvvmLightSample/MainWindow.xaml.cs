using MvvmLightSample.ViewModel;

namespace MvvmLightSample
{
   public partial class MainWindow
   {
      /// <summary>
      ///    Initializes a new instance of the MainWindow class.
      /// </summary>
      public MainWindow()
      {
         InitializeComponent();
         Closing += (s, e) => ViewModelLocator.Cleanup();
      }
   }
}