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
   /// Логика для MainWindow.xaml. Мануальная асинхронная версия, управляемая событиями
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
         using (WebClient client = new WebClient())
         {
            client.DownloadDataCompleted += client_DownloadDataCompleted;
            client.DownloadDataAsync(new Uri("http://" + domain + "/favicon.ico"));
         }
      }

      private void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
      {
         Image imageControl = MakeImageControl(e.Result);
         MWrapPanel.Children.Add(imageControl);
      }

      private static Image MakeImageControl(byte[] bytes)
      {
         Image imageControl = new Image();
         var bitmapImage = MakeBitmapImage(bytes);
         imageControl.Source = bitmapImage;
         imageControl.Width = 16;
         imageControl.Height = 16;
         return imageControl;
      }

      private static BitmapImage MakeBitmapImage(byte[] bytes)
      {
         BitmapImage bitmapImage = new BitmapImage();
         bitmapImage.BeginInit();
         bitmapImage.StreamSource = new MemoryStream(bytes);
         bitmapImage.EndInit();
         return bitmapImage;
      }
   }
}
