using System.Composition;
using CalculatorContract;
using CalculatorUtils;
#if WPF
using FuelEconomyWPF;
#endif
#if WINDOWS_UWP
using System.Composition;
#endif

namespace FuelEconomyUWP
{
	[Export(typeof(ICalculatorExtension))]
	[CalculatorExtensionMetadata(
		Title = "Fuel Economy",
		Description = "Calculate fuel economy",
		ImageUri = "Images/Fuel.png")]
	public class FuelCalculatorExtension : ICalculatorExtension
	{
		private object _control;
		public object Ui => _control ?? (_control = new FuelEconomyUserControl());
	}
}