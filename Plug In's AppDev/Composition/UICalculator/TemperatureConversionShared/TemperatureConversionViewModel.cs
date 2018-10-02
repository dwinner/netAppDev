using System;
using System.Collections.Generic;
using System.Globalization;
using CalculatorUtils;

namespace TemperatureConversionUWP
{
	public class TemperatureConversionViewModel : BindableBase
	{
		private TempConversionType _fromType;
		private string _fromValue;
		private TempConversionType _toType;
		private string _toValue;

		public TemperatureConversionViewModel()
		{
			CalculateCommand = new DelegateCommand(OnCalculate);
		}

		public DelegateCommand CalculateCommand { get; }

		public IEnumerable<string> TemperatureConversionTypes => Enum.GetNames(typeof(TempConversionType));

		public string FromValue
		{
			get { return _fromValue; }
			set { SetProperty(ref _fromValue, value); }
		}

		public string ToValue
		{
			get { return _toValue; }
			set { SetProperty(ref _toValue, value); }
		}

		public TempConversionType FromType
		{
			get { return _fromType; }
			set { SetProperty(ref _fromType, value); }
		}

		public TempConversionType ToType
		{
			get { return _toType; }
			set { SetProperty(ref _toType, value); }
		}

		private double ToCelsiusFrom(double t, TempConversionType conv)
		{
			switch (conv)
			{
				case TempConversionType.Celsius:
					return t;
				case TempConversionType.Fahrenheit:
					return (t - 32) / 1.8;
				case TempConversionType.Kelvin:
					return t - 273.15;
				default:
					throw new ArgumentException("invalid enumeration value");
			}
		}

		private double FromCelsiusTo(double t, TempConversionType conv)
		{
			switch (conv)
			{
				case TempConversionType.Celsius:
					return t;
				case TempConversionType.Fahrenheit:
					return t * 1.8 + 32;
				case TempConversionType.Kelvin:
					return t + 273.15;
				default:
					throw new ArgumentException("invalid enumeration value");
			}
		}

		public void OnCalculate()
		{
			var result = FromCelsiusTo(
				ToCelsiusFrom(double.Parse(FromValue), FromType), ToType);
			ToValue = result.ToString(CultureInfo.InvariantCulture);
		}
	}
}