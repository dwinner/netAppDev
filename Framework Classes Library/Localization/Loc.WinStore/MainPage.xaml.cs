using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Navigation;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace Loc.WinStore
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
         DemoTextBox.Text = DateTime.Today.ToString("D");
         var resourceLoader = new ResourceLoader("Messages");
         DemoTextBlock.Text = resourceLoader.GetString("Hello");
      }
   }
}