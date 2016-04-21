using System;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectionWithCSharp
{
   /// <summary>
   ///    Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
   /// </summary>
   public sealed partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      /// <summary>
      ///    Вызывается перед отображением этой страницы во фрейме.
      /// </summary>
      /// <param name="e">
      ///    Данные о событиях, описывающие, каким образом была достигнута эта страница.  Свойство Parameter
      ///    обычно используется для настройки страницы.
      /// </param>
      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
      }

      private async void OnOpenImage(object sender, RoutedEventArgs e)
      {
         var picker = new FileOpenPicker
         {
            SuggestedStartLocation = PickerLocationId.PicturesLibrary
         };
         picker.FileTypeFilter.Add(".jpg");
         picker.FileTypeFilter.Add(".png");
         var file = await picker.PickSingleFileAsync();
         var image = new BitmapImage();
         image.SetSource(await file.OpenReadAsync());
         OpenedImage.Source = image;
      }
   }
}