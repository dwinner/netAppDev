using System.Collections.Generic;
using System.Net;
using System.Windows;

namespace FaviconBrowser
{
   /// <summary>
   /// Логика для MainWindow.xaml. Асинхронная версия
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

      private void GetButton_OnClick(object sender, RoutedEventArgs e)
      {
         foreach (string domain in SDomains)
         {
            AddAFavicon(domain);
         }
      }

      private async void AddAFavicon(string domain)
      {
         using (WebClient webClient = new WebClient())
         {
            byte[] bytes = await webClient.DownloadDataTaskAsync(string.Format("http://{0}/favicon.ico", domain));
            MWrapPanel.Children.Add(ImageUtilities.MakeImageControl(bytes));
         }
      }
   }
}
