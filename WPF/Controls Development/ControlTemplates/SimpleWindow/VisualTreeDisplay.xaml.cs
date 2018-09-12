using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleWindow
{
   public partial class VisualTreeDisplay
   {
      public VisualTreeDisplay()
      {
         InitializeComponent();
      }

      public void ShowVisualTree(DependencyObject element)
      {
         // Clear the tree.
         ElementsTreeview.Items.Clear();

         // Start processing elements, begin at the root.
         ProcessElement(element, null);
      }

      private void ProcessElement(DependencyObject element, ItemsControl previousItem)
      {
         // Create a TreeViewItem for the current element.
         var item = new TreeViewItem
         {
            Header = element.GetType().Name,
            IsExpanded = true
         };

         // Check whether this item should be added to the root of the tree
         //(if it's the first item), or nested under another item.
         if (previousItem == null)
         {
            ElementsTreeview.Items.Add(item);
         }
         else
         {
            previousItem.Items.Add(item);
         }

         // Check if this element contains other elements.
         for (var i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
         {
            // Process each contained element recursively.
            ProcessElement(VisualTreeHelper.GetChild(element, i), item);
         }
      }
   }
}