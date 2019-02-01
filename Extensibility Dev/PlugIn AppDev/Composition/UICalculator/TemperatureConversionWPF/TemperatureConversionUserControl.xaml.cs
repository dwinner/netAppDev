using TemperatureConversionUWP;

namespace TemperatureConversionWPF
{
	public partial class TemperatureConversionUserControl
	{
		public TemperatureConversionUserControl()
		{
			InitializeComponent();
			DataContext = this;
		}

		public TemperatureConversionViewModel ViewModel { get; } = new TemperatureConversionViewModel();
	}
}