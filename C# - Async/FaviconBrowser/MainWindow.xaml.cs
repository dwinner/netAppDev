using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FaviconBrowser
{
   /// <summary>
   /// Логика для MainWindow.xaml. Синхронная версия
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

      private void AddAFavicon(string domain)
      {
         using (WebClient webClient = new WebClient())
         {
            byte[] bytes = webClient.DownloadData("http://" + domain + "/favicon.ico");
            Image imageControl = MakeImageControl(bytes);
            MWrapPanel.Children.Add(imageControl);
         }
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
