using System.Collections.Generic;
using CalculatorUtils;

namespace FuelEconomy
{
   /// <summary>
   ///    Модель представления над типом экономии топлива
   /// </summary>
   public class FuelEconomyViewModel : BindableBase
   {
      public List<FuelEconomyType> FuelEcoTypes { get; private set; }

      public FuelEconomyViewModel()
      {
         InitializeFullEcoTypes();
      }      

      private void InitializeFullEcoTypes()
      {
         var firstEconomyType = new FuelEconomyType
         {
            Id = "lpk",
            Text = "L/100 km",
            DistanceText = "Distance (kilometers)",
            FuelText = "Fuel used (liters)"
         };
         var secondEconomyType = new FuelEconomyType
         {
            Id = "mpg",
            Text = "Miles per gallon",
            DistanceText = "Distance (miles)",
            FuelText = "Fuel used (gallons)"
         };
         FuelEcoTypes = new List<FuelEconomyType>
         {
            firstEconomyType,
            secondEconomyType
         };
      }

      private FuelEconomyType _selectedFuelEcoType;

      public FuelEconomyType SelectedFuelEcoType
      {
         get { return _selectedFuelEcoType; }
         set { SetProperty(ref _selectedFuelEcoType, value); }
      }

      private string _fuel;

      public string Fuel
      {
         get { return _fuel; }
         set { SetProperty(ref _fuel, value); }
      }

      private string _distance;

      public string Distance
      {
         get { return _distance; }
         set { SetProperty(ref _distance, value); }
      }

      private string _result;

      public string Result
      {
         get { return _result; }
         set { SetProperty(ref _result, value); }
      }
   }
}