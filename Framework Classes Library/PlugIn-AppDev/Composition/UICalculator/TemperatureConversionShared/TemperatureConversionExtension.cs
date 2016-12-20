
using System.Composition;
#if WPF
using TemperatureConversionWPF;
#endif
#if WINDOWS_UWP
using System.Composition;
#endif
using CalculatorContract;
using CalculatorUtils;


namespace TemperatureConversionUWP
{
	[Export(typeof(ICalculatorExtension))]
	[CalculatorExtensionMetadata(
		Title = "Temperature Conversion",
		Description = "Temperature conversion",
		ImageUri = "Images/Temperature.png")]
	public class TemperatureConversionExtension : ICalculatorExtension
	{
		private object _control;
		public object UI => _control ?? (_control = new TemperatureConversionUC());

	}
}
