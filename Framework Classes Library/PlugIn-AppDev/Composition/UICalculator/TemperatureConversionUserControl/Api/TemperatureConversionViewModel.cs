using System;
using System.Collections.Generic;
using System.Globalization;
using CalculatorUtils;

namespace TemperatureConversionUserControl.Api
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

		public DelegateCommand CalculateCommand { get; }

		public IEnumerable<string> TemperatureConversionTypes => Enum.GetNames(typeof (TempConversionType));

		public void OnCalculate()
			=>
				ToValue =
					FromCelsiusTo(ToCelsiusFrom(double.Parse(FromValue), FromType), ToType).ToString(CultureInfo.CurrentCulture);

		private double ToCelsiusFrom(double temp, TempConversionType conversionType)
		{
			switch (conversionType)
			{
				case TempConversionType.Celsius:
					return temp;
				case TempConversionType.Fahrenheit:
					return (temp - 32)/1.8;
				case TempConversionType.Kelvin:
					return temp - 273.15;
				default:
					throw new ArgumentOutOfRangeException(nameof(conversionType), conversionType, null);
			}
		}

		private double FromCelsiusTo(double temp, TempConversionType conversionType)
		{
			switch (conversionType)
			{
				case TempConversionType.Celsius:
					return temp;
				case TempConversionType.Fahrenheit:
					return temp*1.8 + 32;
				case TempConversionType.Kelvin:
					return temp + 273.15;
				default:
					throw new ArgumentOutOfRangeException(nameof(conversionType), conversionType, null);
			}
		}
	}
}