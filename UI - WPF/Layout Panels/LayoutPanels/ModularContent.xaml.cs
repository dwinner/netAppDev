using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LayoutPanels
{
   public partial class ModularContent
   {
      public ModularContent()
      {
         InitializeComponent();
         AddHandler(ToggleButton.CheckedEvent, new RoutedEventHandler(OnChecked));
         AddHandler(ToggleButton.UncheckedEvent, new RoutedEventHandler(OnUnchecked));
      }

      private void OnChecked(object sender, RoutedEventArgs e)
      {
         var chk = (CheckBox)e.OriginalSource;
         DependencyObject obj = LogicalTreeHelper.FindLogicalNode(this, chk.Content.ToString());
         var frameworkElement = (FrameworkElement)obj;
         if (frameworkElement != null)
            frameworkElement.Visibility = Visibility.Visible;
      }

      private void OnUnchecked(object sender, RoutedEventArgs e)
      {
         var chk = (CheckBox)e.OriginalSource;
         DependencyObject obj = LogicalTreeHelper.FindLogicalNode(this, chk.Content.ToString());
         var frameworkElement = (FrameworkElement)obj;
         if (frameworkElement != null)
            frameworkElement.Visibility = Visibility.Collapsed;
      }
   }
}