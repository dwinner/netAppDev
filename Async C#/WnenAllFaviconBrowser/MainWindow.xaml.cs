using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FaviconBrowser
{
   /// <summary>
   /// Логика для MainWindow.xaml. Версия с ожиданием завершения задач.
   /// </summary>
   public partial class MainWindow
   {
      private static readonly List<string> SDomains = new List<string>
                                                             {
                                                                 "google.com",
                                                                 "bing.com",
                                                                 "oreilly.com",
                                                                 "simple-talk.com",
                                                                 "microsoft.com",
                                                                 "facebook.com",
                                                                 "twitter.com",
                                                                 "reddit.com",
                                                                 "baidu.com",
                                                                 "bbc.co.uk"
                                                             };

      public MainWindow()
      {
         InitializeComponent();
      }

      private async void GetButton_OnClick(object sender, RoutedEventArgs e)
      {
         IEnumerable<Task<Image>> tasks = SDomains.Select(GetFavicon);

         /* Или так...
           IList<Task<Image>> tasks = new List<Task<Image>>();
           foreach (string domain in SDomains)
           {
              tasks.Add(GetFavicon(domain));
           }
          */

         // Task<Image[]> allTask = Task.WhenAll(tasks);
         Image[] images = await Task.WhenAll(tasks);  // Note: Вернем управление и дождемся завершения всех задач
         foreach (Image eachImage in images)
         {
            AddAFavicon(eachImage);
         }
      }

      private static async Task<Image> GetFavicon(string domain)
      {
         using (WebClient webClient = new WebClient())
         {
            byte[] bytes = await webClient.DownloadDataTaskAsync(string.Format("http://{0}/favicon.ico", domain));
            return MakeImageControl(bytes);
         }
      }

      private void AddAFavicon(UIElement uiElement)
      {
         MWrapPanel.Children.Add(uiElement);
      }

      private static Image MakeImageControl(byte[] bytes)
      {
         Image imageControl = new Image();
         BitmapImage bitmapImage = new BitmapImage();
         bitmapImage.BeginInit();
         bitmapImage.StreamSource = new MemoryStream(bytes);
         bitmapImage.EndInit();
         imageControl.Source = bitmapImage;
         imageControl.Width = 16;
         imageControl.Height = 16;
         return imageControl;
      }
   }
}
