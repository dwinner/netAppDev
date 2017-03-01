
using System.Composition;
#if WPF
using FuelEconomyWPF;
#endif
#if WINDOWS_UWP
using System.Composition;
#endif
using CalculatorContract;
using CalculatorUtils;

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
		public object UI => _control ?? (_control = new FuelEconomyUC());

	}
}
