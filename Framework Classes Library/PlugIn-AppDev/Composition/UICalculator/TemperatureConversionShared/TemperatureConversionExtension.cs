using System.Composition;
using CalculatorContract;
using CalculatorUtils;
#if WPF
using TemperatureConversionWPF;
#endif
#if WINDOWS_UWP
using System.Composition;
#endif


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
		public object Ui => _control ?? (_control = new TemperatureConversionUserControl());
	}
}