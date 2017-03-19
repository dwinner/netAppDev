using System.Collections.Generic;
using System.Globalization;
using CalculatorUtils;

namespace FuelEconomyUWP
{
	public class FuelEconomyViewModel : BindableBase
	{
		private string _distance;
		private string _fuel;
		private string _result;
		private FuelEconomyType _selectedFuelEcoType;

		public FuelEconomyViewModel()
		{
			InitializeFuelEcoTypes();
			CalculateCommand = new DelegateCommand(OnCalculate);
		}

		public DelegateCommand CalculateCommand { get; }

		public List<FuelEconomyType> FuelEcoTypes { get; } = new List<FuelEconomyType>();

		public FuelEconomyType SelectedFuelEcoType
		{
			get { return _selectedFuelEcoType; }
			set { SetProperty(ref _selectedFuelEcoType, value); }
		}

		public string Fuel
		{
			get { return _fuel; }
			set { SetProperty(ref _fuel, value); }
		}

		public string Distance
		{
			get { return _distance; }
			set { SetProperty(ref _distance, value); }
		}

		public string Result
		{
			get { return _result; }
			set { SetProperty(ref _result, value); }
		}

		public void OnCalculate()
		{
			var fuel = double.Parse(Fuel);
			var distance = double.Parse(Distance);
			var ecoType = SelectedFuelEcoType;
			double result = 0;
			switch (ecoType.Id)
			{
				case "lpk":
					result = fuel / (distance / 100);
					break;
				case "mpg":
					result = distance / fuel;
					break;
			}

			Result = result.ToString(CultureInfo.InvariantCulture);
		}


		private void InitializeFuelEcoTypes()
		{
			FuelEcoTypes.AddRange(new[]
			{
				new FuelEconomyType
				{
					Id = "lpk",
					Text = "L/100 km",
					DistanceText = "Distance (kilometers)",
					FuelText = "Fuel used (liters)"
				},
				new FuelEconomyType
				{
					Id = "mpg",
					Text = "Miles per gallon",
					DistanceText = "Distance (miles)",
					FuelText = "Fuel used (gallons)"
				}
			});
		}
	}
}