using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StoreDatabase;

namespace DataBinding
{   
   public partial class ValidationTest
   {
      ICollection<Product> products;

      public ValidationTest()
      {
         InitializeComponent();
      }

      void OnGetProducts(object sender, RoutedEventArgs e)
      {
         products = App.StoreDb.GetProducts();
         lstProducts.ItemsSource = products;
      }

      void OnUpdateProduct(object sender, RoutedEventArgs e)
      {         
         FocusManager.SetFocusedElement(this, (Button) sender);
      }

      void OnValidationError(object sender, ValidationErrorEventArgs e)
      {
         if (e.Action == ValidationErrorEventAction.Added)
         {
            MessageBox.Show(e.Error.ErrorContent.ToString());
         }
      }

      void OnGetExceptions(object sender, RoutedEventArgs e)
      {
         var builder = new StringBuilder();
         GetErrors(builder, gridProductDetails);
         var message = builder.ToString();
         if (message != string.Empty)
            MessageBox.Show(message);
      }

      static void GetErrors(StringBuilder builder, DependencyObject obj)
      {
         foreach (var element in LogicalTreeHelper.GetChildren(obj).OfType<TextBox>())
         {
            if (Validation.GetHasError(element))
            {
               builder.AppendFormat("{0} has errors:{1}", element.Text, Environment.NewLine);
               foreach (var error in Validation.GetErrors(element))
               {
                  builder.AppendFormat("  {0}{1}", error.ErrorContent, Environment.NewLine);                  
               }
            }

            // Check the children of this object.
            GetErrors(builder, element);
         }
      }
   }
}