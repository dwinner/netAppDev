using System.Windows;
using System.Windows.Controls;
using AdvancedOperations;
using CalculatorContract;
using CalculatorViewModels;
using FuelEconomyUserControl.Api;
using SimpleCalculator;
using TemperatureConversionUserControl.Api;

namespace WpfCalculatorHost
{
	public partial class CalculatorHostWindow
	{
		public CalculatorHostWindow()
		{
			InitializeComponent();
			DataContext = this;

			CalculatorViewModel = new CalculatorViewModel();
			CalculatorViewModel.Init(
				typeof (Calculator), typeof (SubtractOperation), typeof (SlowAddOperation), typeof (AddOperation));

			CalculatorExtensionViewModel = new CalculatorExtensionViewModel();
			CalculatorExtensionViewModel.Init(
				typeof (FuelCalculatorExtension), typeof (TemperatureConversionExtension));
		}

		public CalculatorViewModel CalculatorViewModel { get; }

		public CalculatorExtensionViewModel CalculatorExtensionViewModel { get; }

		private void OnNumberClick(object sender, RoutedEventArgs e)
		{
			var button = e.Source as Button;
			if (button != null)
			{
				CalculatorViewModel.Input += button.Content.ToString();
			}
		}

		private void OnDefineOperation(object sender, RoutedEventArgs e)
		{
			var button = e.Source as Button;
			if (button != null)
			{
				CalculatorViewModel.CurrentOperation = button.Tag as IOperation;
			}
		}
	}
}