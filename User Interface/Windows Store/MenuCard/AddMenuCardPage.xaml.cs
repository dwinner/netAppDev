using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Wrox.Metro.DataModel;
using Wrox.Metro.Storage;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Wrox.Metro
{
   /// <summary>
   /// A basic page that provides characteristics common to most applications.
   /// </summary>
   public sealed partial class AddMenuCardPage : Wrox.Metro.Common.LayoutAwarePage
   {
      private AddMenuCardInfo info = new AddMenuCardInfo();

      public AddMenuCardPage()
      {
         this.InitializeComponent();
      }

      /// <summary>
      /// Populates the page with content passed during navigation.  Any saved state is also
      /// provided when recreating a page from a prior session.
      /// </summary>
      /// <param name="navigationParameter">The parameter value passed to
      /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
      /// </param>
      /// <param name="pageState">A dictionary of state preserved by this page during an earlier
      /// session.  This will be null the first time a page is visited.</param>
      protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
      {
         this.DefaultViewModel["Item"] = info;
      }

      /// <summary>
      /// Preserves state associated with this page in case the application is suspended or the
      /// page is discarded from the navigation cache.  Values must conform to the serialization
      /// requirements of <see cref="SuspensionManager.SessionState"/>.
      /// </summary>
      /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
      protected async override void SaveState(Dictionary<String, Object> pageState)
      {
         var mc = new MenuCard
         {
            Title = info.Title,
            Description = info.Description,
            Image = info.Image,
            ImagePath = info.ImageFileName
         };
         mc.SetDirty();
         MenuCardFactory.Instance.Cards.Add(mc);

         var storage = new MenuCardStorage();
         await storage.WriteMenuCardsAsync(MenuCardFactory.Instance.Cards.ToList());
      }

      private async void OnUploadImage(object sender, RoutedEventArgs e)
      {
         var filePicker = new FileOpenPicker();
         filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
         filePicker.FileTypeFilter.Add(".jpg");
         filePicker.FileTypeFilter.Add(".png");
         StorageFile file = await filePicker.PickSingleFileAsync();
         if (file == null) return;

         var stream = await file.OpenAsync(FileAccessMode.Read);

         var image = new BitmapImage();

         image.SetSource(stream);
         image.ImageOpened += async (sender1, e1) =>
         {
            if (image.PixelHeight > image.PixelWidth)
            {
               image.DecodePixelHeight = 900;
            }
            else
            {
               image.DecodePixelWidth = 900;
            }
            stream.Seek(0);
            MenuCardImageStorage imageStorage = new MenuCardImageStorage();
            MenuCardStorage storage = new MenuCardStorage();
            info.ImageFileName = string.Format("{0}.jpg", Guid.NewGuid().ToString());

            await imageStorage.WriteImageAsync(stream, info.ImageFileName);
         };
         image.ImageFailed += (sender1, e1) =>
         {
            string s = e1.ErrorMessage;
         };


         info.Image = image;

      }

   }
}
