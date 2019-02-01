using System;
using System.Windows;

namespace App.WindowTracker
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnCreate(object sender, RoutedEventArgs e)
      {
         var doc = new Document { Owner = this };
         doc.Show();
         ((TrackerApp)Application.Current).Documents.Add(doc);
      }

      private void OnUpdate(object sender, RoutedEventArgs e)
      {
         foreach (var doc in ((TrackerApp)Application.Current).Documents)
         {
            doc.SetContent(string.Format("Refreshed at {0}.", DateTime.Now.ToLongTimeString()));
         }
      }
   }
}