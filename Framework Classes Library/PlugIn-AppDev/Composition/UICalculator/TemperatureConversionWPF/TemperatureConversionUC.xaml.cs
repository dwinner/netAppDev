using System.Windows.Controls;
using TemperatureConversionUWP;

namespace TemperatureConversionWPF
{
	/// <summary>
	/// Interaction logic for TemperatureConversionUC.xaml
	/// </summary>
	public partial class TemperatureConversionUC : UserControl
	{
		public TemperatureConversionUC()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		public TemperatureConversionViewModel ViewModel { get; } = new TemperatureConversionViewModel();
	}
}
