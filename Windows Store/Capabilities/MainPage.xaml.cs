using System;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CameraSample
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class MainPage : Page
   {
      public MainPage()
      {
         this.InitializeComponent();
      }

      /// <summary>
      /// Invoked when this page is about to be displayed in a Frame.
      /// </summary>
      /// <param name="e">Event data that describes how this page was reached.  The Parameter
      /// property is typically used to configure the page.</param>
      protected async override void OnNavigatedTo(NavigationEventArgs e)
      {
         CameraCaptureUI cam = new CameraCaptureUI();
         StorageFile file = await cam.CaptureFileAsync(CameraCaptureUIMode.Photo);
         if (file != null)
         {
            BitmapImage image = new BitmapImage(new Uri(file.Path));
            image1.Source = image;
         }
      }
   }
}
