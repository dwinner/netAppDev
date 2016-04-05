using System.Windows;
using CalculatorContract;
using CalculatorUtils;

namespace FuelEconomy
{
   /// <summary>
   ///    Тип для экспорта пользовательского элемента управления
   /// </summary>
   [CalculatorExtensionExport(typeof (ICalculatorExtension), Title = "Fuel Economy",
      Description = "Calculate fuel economy", ImageUri = "Fuel.png")]
   public class FuelCalculatorExtension : ICalculatorExtension
   {
      private FrameworkElement _control;

      public FrameworkElement Ui
      {
         get { return _control ?? (_control = new FuelEconomy()); }
      }
   }
}