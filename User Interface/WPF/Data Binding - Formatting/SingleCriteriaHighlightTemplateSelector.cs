using System.Windows;
using System.Windows.Controls;
using StoreDatabase;

namespace DataBinding
{
	public class SingleCriteriaHighlightTemplateSelector : DataTemplateSelector
	{
		public DataTemplate DefaultTemplate { get; set; }

		public DataTemplate HighlightTemplate { get; set; }

		public string PropertyToEvaluate { get; set; }

		public string PropertyValueToHighlight { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var product = (Product) item;
			var type = product?.GetType();
			var property = type?.GetProperty(PropertyToEvaluate);
			return property?.GetValue(product, null).ToString() == PropertyValueToHighlight ? HighlightTemplate : DefaultTemplate;
		}
	}
}