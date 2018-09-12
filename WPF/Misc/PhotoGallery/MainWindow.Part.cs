using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PhotoGallery
{
   partial class MainWindow
   {      
      private void OnSortByName(object sender, RoutedEventArgs e)
      {
         SortHelper("Name");
      }

      private void OnSortByDateTime(object sender, RoutedEventArgs e)
      {
         SortHelper("DateTime");
      }

      private void OnSortBySize(object sender, RoutedEventArgs e)
      {
         SortHelper("Size");
      }

      private void SortHelper(string propertyName)
      {
         // Get the default view
         var view = CollectionViewSource.GetDefaultView(_photos);

         // Check if the view is already sorted ascending by the current property
         if (view.SortDescriptions.Count > 0
             && view.SortDescriptions[0].PropertyName == propertyName
             && view.SortDescriptions[0].Direction == ListSortDirection.Ascending)
         {
            // Already sorted ascending, so "toggle" by sorting descending
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(
               propertyName, ListSortDirection.Descending));
         }
         else
         {
            // Sort ascending
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(
               propertyName, ListSortDirection.Ascending));
         }
      }
   }
}