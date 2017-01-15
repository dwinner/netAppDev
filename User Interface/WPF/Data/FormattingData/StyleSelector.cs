using System.Windows;
using System.Windows.Controls;
using StoreDatabase;

namespace DataBinding
{
/*
   public class ProductByCategoryStyleSelector : StyleSelector
   {
      public override Style SelectStyle(object item, DependencyObject container)
      {
         Product product = (Product)item;
         Window window = Application.Current.MainWindow;

         if (product.CategoryName == "Travel")
         {
            return (Style)window.FindResource("TravelProductStyle");
         }
         else
         {
            return (Style)window.FindResource("DefaultProductStyle");
         }
      }
   }
*/

	public sealed class SingleCriteriaHighlightStyleSelector : StyleSelector
	{
		public Style DefaultStyle { get; set; }

		public Style HighlightStyle { get; set; }

		public string PropertyToEvaluate { get; set; }

		public string PropertyValueToHighlight { get; set; }

		public override Style SelectStyle(object item, DependencyObject container)
		{
			var product = (Product) item;

			// Use reflection to get the property to check.
			var type = product.GetType();
			var property = type.GetProperty(PropertyToEvaluate);

			// Decide if this product should be highlighted
			// based on the property value.
			return property.GetValue(product, null).ToString() == PropertyValueToHighlight ? HighlightStyle : DefaultStyle;
		}
	}
}