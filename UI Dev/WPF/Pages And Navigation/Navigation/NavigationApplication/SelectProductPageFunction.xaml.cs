using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace NavigationApplication
{
   public partial class SelectProductPageFunction
   {
      public SelectProductPageFunction()
      {
         InitializeComponent();
      }

      private void OnOk(object sender, RoutedEventArgs e)
      {
         // Return the selection information.
         var item = (ListBoxItem) ProductListBox.SelectedItem;
         var product = new Product(item.Content.ToString());
         OnReturn(new ReturnEventArgs<Product>(product));
      }

      private void OnCancel(object sender, RoutedEventArgs e)
      {
         // Indicate that nothing was selected.
         OnReturn(null);
      }
   }

   public class Product
   {
      public Product(string name)
      {
         Name = name;
      }

      public string Name { get; private set; }
   }
}