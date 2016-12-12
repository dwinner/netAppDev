using System.Composition;
using CalculatorContract;
using CalculatorUtils;

namespace TemperatureConversionUserControl.Api
{
	[Export(typeof (ICalculatorExtension))]
	[CalculatorExtensionMetadata(
		Title = "Temperature Conversion",
		Description = "Temperature Conversion",
		ImageUri = "Images/Temperature.png")]
	public class TemperatureConversionExtension : ICalculatorExtension
	{
		private object _control;
		public object UI => _control ?? (_control = new TemperatureConversion());
	}
}