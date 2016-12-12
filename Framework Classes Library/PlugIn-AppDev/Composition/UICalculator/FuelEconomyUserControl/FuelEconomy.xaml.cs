using FuelEconomyUserControl.Api;

namespace FuelEconomyUserControl
{
	public partial class FuelEconomy
	{
		public FuelEconomy()
		{
			InitializeComponent();
			DataContext = this;
		}

		public FuelEconomyViewModel ViewModel { get; } = new FuelEconomyViewModel();
	}
}