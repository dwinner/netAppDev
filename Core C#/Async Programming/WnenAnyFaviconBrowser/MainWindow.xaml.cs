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
   /// Логика для MainWindow.xaml. Версия с первой завершившейся задачей.
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
         IEnumerable<Task<Image>> eachTasks = tasks as IList<Task<Image>> ?? tasks.ToList();
         Task<Task<Image>> anyTask = Task.WhenAny(eachTasks);
         Task<Image> winner = await anyTask;
         Image image = await winner;   // Note: Этот оператор всегда завершается синхронно, 
         // Note: поэтому можно было и так: Image image = winner.Result;

         AddAFavicon(image);

         foreach (Task<Image> eachTask in eachTasks.Where(eachTask => eachTask != winner && !eachTask.IsFaulted))
         {
            await eachTask;
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
