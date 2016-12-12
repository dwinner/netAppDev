using TemperatureConversionUserControl.Api;

namespace TemperatureConversionUserControl
{
	public partial class TemperatureConversion
	{
		public TemperatureConversion()
		{
			InitializeComponent();
			DataContext = this;
		}

		public TemperatureConversionViewModel ViewModel { get; } = new TemperatureConversionViewModel();
	}
}