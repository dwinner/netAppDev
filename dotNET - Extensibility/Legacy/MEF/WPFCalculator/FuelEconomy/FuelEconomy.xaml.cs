using System;
using System.Windows;

namespace FuelEconomy
{
   /// <summary>
   ///    Элемент управления для подсчета расхода топлива
   /// </summary>
   public partial class FuelEconomy
   {
      private readonly FuelEconomyViewModel _viewModel = new FuelEconomyViewModel();

      public FuelEconomy()
      {
         InitializeComponent();
         DataContext = _viewModel;
      }

      private void CalculateButton_OnClick(object sender, RoutedEventArgs e)
      {
         double fuel, distance;
         if (!double.TryParse(_viewModel.Fuel, out fuel) || !double.TryParse(_viewModel.Distance, out distance))
            return;

         FuelEconomyType ecoType = _viewModel.SelectedFuelEcoType;
         double result = 0;
         switch (ecoType.Id)
         {
            case "lpk":
               if (Math.Abs(distance) > double.Epsilon)
                  result = fuel / (distance / 100);
               break;
            case "mpg":
               if (Math.Abs(fuel) > double.Epsilon)
                  result = distance / fuel;
               break;
         }
         _viewModel.Result = result.ToString("F");
      }
   }
}