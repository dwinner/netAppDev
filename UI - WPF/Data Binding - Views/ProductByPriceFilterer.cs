using StoreDatabase;

namespace DataBinding
{
	public sealed class ProductByPriceFilterer
	{
		public ProductByPriceFilterer(decimal minimumPrice)
		{
			MinimumPrice = minimumPrice;
		}

		public decimal MinimumPrice { get; set; }

		public bool FilterItem(object item) => (item as Product)?.UnitCost > MinimumPrice;
	}
}