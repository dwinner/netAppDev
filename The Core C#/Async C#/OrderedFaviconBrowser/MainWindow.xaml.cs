using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FaviconBrowser
{
   /// <summary>
   /// Логика для MainWindow.xaml. EAP-версия, сохраняющая порядок загрузки списка
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
         AddRemainingFavicons(SDomains, 0);
      }

      private void AddRemainingFavicons(IList<string> domains, int i)
      {
         WebClient webClient = new WebClient();
         webClient.DownloadDataCompleted += (o, args) =>
            {
               Image imageControl = MakeImageControl(args.Result);
               MWrapPanel.Children.Add(imageControl);
               if (i + 1 < domains.Count)
               {
                  AddRemainingFavicons(domains, i + 1);
               }
            };
         webClient.DownloadDataAsync(new Uri(string.Format("http://{0}/favicon.ico", domains[i])));
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
