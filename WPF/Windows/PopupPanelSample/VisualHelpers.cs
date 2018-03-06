using System.Windows;
using System.Windows.Media;

namespace PopupPanelSample
{
   public static class VisualHelpers
   {
      public static UIElement FindFirstFocusableChild(DependencyObject parent)
      {
         // Confirm parent is valid. 
         if (parent == null)
            return null;

         UIElement foundChild = null;
         var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
         for (var i = 0; i < childrenCount; i++)
         {
            var child = VisualTreeHelper.GetChild(parent, i) as UIElement;

            // This is returning me things like ContentControls, so for now filtering to buttons/textboxes only
            if (child != null && child.Focusable && child.IsVisible)
            {
               foundChild = child;
               break;
            }
            // recursively drill down the tree
            foundChild = FindFirstFocusableChild(child);

            // If the child is found, break so we do not overwrite the found child. 
            if (foundChild != null)
               break;
         }

         return foundChild;
      }

      public static T FindAncester <T>(DependencyObject current)
         where T : DependencyObject
      {
         // Need this call to avoid returning current object if it is the same type as parent we are looking for
         current = VisualTreeHelper.GetParent(current);

         while (current != null)
         {
            var ancester = current as T;
            if (ancester != null)
            {
               return ancester;
            }

            current = VisualTreeHelper.GetParent(current);
         }

         return null;
      }

      /// <summary>
      ///    Looks for a child control within a parent by name
      /// </summary>
      public static T FindChild <T>(DependencyObject parent, string childName)
         where T : DependencyObject
      {
         // Confirm parent and childName are valid. 
         if (parent == null) return null;

         T foundChild = null;

         var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
         for (var i = 0; i < childrenCount; i++)
         {
            var child = VisualTreeHelper.GetChild(parent, i);
            // If the child is not of the request child type child
            var childType = child as T;
            if (childType == null)
            {
               // recursively drill down the tree
               foundChild = FindChild<T>(child, childName);

               // If the child is found, break so we do not overwrite the found child. 
               if (foundChild != null) break;
            }
            else if (!string.IsNullOrEmpty(childName))
            {
               var frameworkElement = child as FrameworkElement;
               // If the child's name is set for search
               if (frameworkElement != null && frameworkElement.Name == childName)
               {
                  // if the child's name is of the request name
                  foundChild = (T) child;
                  break;
               }
               // recursively drill down the tree
               foundChild = FindChild<T>(child, childName);

               // If the child is found, break so we do not overwrite the found child. 
               if (foundChild != null) break;
            }
            else
            {
               // child element found.
               foundChild = (T) child;
               break;
            }
         }

         return foundChild;
      }
   }
}