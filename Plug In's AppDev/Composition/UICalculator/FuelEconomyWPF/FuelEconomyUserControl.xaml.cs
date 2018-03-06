using FuelEconomyUWP;

namespace FuelEconomyWPF
{	
	public partial class FuelEconomyUserControl
	{
		public FuelEconomyUserControl()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		public FuelEconomyViewModel ViewModel { get; } = new FuelEconomyViewModel();
	}
}
