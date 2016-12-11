#if WPF
using TemperatureConversionWpf
#endif

#if WINDOWS_UWP
using TemperatureConversionUwp
#endif

using System.Composition;

namespace TemperatureConversionShared
{
	[Export(typeof(ICalculatorExtension))]
	[CalculatorExtensionMetadata(
		Title = "Temperature Conversion",
		Description = "Temperature conversion",
		ImageUri = "Images/Temperature.png")]
	public class TemperatureConversionExtension : ICalculatorExtension
	{
		private object _control;
		public object UI => _control ?? (_control = new TemperatureConversionUserControl());
	}
}
