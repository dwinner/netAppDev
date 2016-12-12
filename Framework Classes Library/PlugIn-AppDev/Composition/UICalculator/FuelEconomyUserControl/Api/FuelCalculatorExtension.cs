using System.Composition;
using CalculatorContract;
using CalculatorUtils;

namespace FuelEconomyUserControl.Api
{
	[Export(typeof (ICalculatorExtension))]
	[CalculatorExtensionMetadata(
		Title = "Fuel Economy",
		Description = "Calculate fuel economy",
		ImageUri = "Images/Fuel.png")]
	public class FuelCalculatorExtension : ICalculatorExtension
	{
		private object _control;

		public object UI => _control ?? (_control = new FuelEconomy());
	}
}