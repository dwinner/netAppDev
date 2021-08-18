using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ListViewDnDSample
{
   public partial class MainWindow
   {
      private const string DragDropFormat = "myFormat";

      private readonly ObservableCollection<Contact> _sourceContacts =
         new ObservableCollection<Contact>(new List<Contact>
         {
            new Contact {FirstName = "Den", LastName = "Winner"},
            new Contact {FirstName = "Michail", LastName = "Lazarev"},
            new Contact {FirstName = "Vladimir", LastName = "Lankin"},
            new Contact {FirstName = "Vladimir", LastName = "Titov"},
            new Contact {FirstName = "Arseniy", LastName = "Martiavon"}
         });

      private readonly ObservableCollection<Contact> _targetContacts =
         new ObservableCollection<Contact>(new List<Contact>
         {
            new Contact {FirstName = "Maxim", LastName = "Natesov"}
         });

      private Point _startPoint;

      public MainWindow()
      {
         InitializeComponent();

         DragListView.ItemsSource = _sourceContacts;
         DropListView.ItemsSource = _targetContacts;
      }

      private void DragListView_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         // Store the mouse position
         _startPoint = e.GetPosition(null);
      }

      private void DragListView_OnPreviewMouseMove(object sender, MouseEventArgs e)
      {
         // Get the current mouse position
         var mousePosition = e.GetPosition(null);
         var diff = _startPoint - mousePosition;

         if (e.LeftButton == MouseButtonState.Pressed &&
             (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
              Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
         {
            // Get the dragged ListViewItem
            var dragSourceListView = (ListView) sender;
            var sourceListViewItem = FindAncestor<ListViewItem>((DependencyObject) e.OriginalSource);

            // Find the data behind the ListViewItem
            var contact = (Contact) dragSourceListView.ItemContainerGenerator.ItemFromContainer(sourceListViewItem);

            // Initialize the drag and drop operation
            var dragData = new DataObject(DragDropFormat, contact);
            DragDrop.DoDragDrop(sourceListViewItem, dragData, DragDropEffects.Move);
         }
      }

      private static T FindAncestor<T>(DependencyObject current)
         where T : DependencyObject
      {
         do
         {
            var ancestor = current as T;
            if (ancestor != null)
               return ancestor;

            current = VisualTreeHelper.GetParent(current);
         } while (current != null);

         return default(T);
      }

      private void DropListView_OnDrop(object sender, DragEventArgs e)
      {
         if (e.Data.GetDataPresent(DragDropFormat))
         {
            var contact = e.Data.GetData(DragDropFormat) as Contact;
            var targetListView = (ListView) sender;
            var contacts = targetListView.ItemsSource as ObservableCollection<Contact>;
            if (contacts != null)
               contacts.Add(contact);

            var sourceContacts = DragListView.ItemsSource as ObservableCollection<Contact>;
            if (sourceContacts != null)
               sourceContacts.Remove(contact);
         }
      }

      private void DropListView_OnDragEnter(object sender, DragEventArgs e)
      {
         if (!e.Data.GetDataPresent(DragDropFormat) || sender == e.Source)
            e.Effects = DragDropEffects.None;
      }
   }
}