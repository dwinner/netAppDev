using System.Windows;
using System.Windows.Controls;
using AdvancedOperations;
using CalculatorContract;
using CalculatorViewModels;
using FuelEconomyUWP;
using SimpleCalculator;
using TemperatureConversionUWP;

namespace WPFCalculatorHost
{	
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;

			CalculatorViewModel = new CalculatorViewModel();
			CalculatorViewModel.Init(
				typeof(Calculator),
				typeof(SubtractOperation),
				typeof(SlowAddOperation),
				typeof(AddOperation));

			CalculatorExtensionsViewModel = new CalculatorExtensionsViewModel();
			CalculatorExtensionsViewModel.Init(
				typeof(FuelCalculatorExtension),
				typeof(TemperatureConversionExtension));
		}

		public CalculatorViewModel CalculatorViewModel { get; }

		public CalculatorExtensionsViewModel CalculatorExtensionsViewModel { get; }

		private void OnNumberClick(object sender, RoutedEventArgs e)
		{
			var b = e.Source as Button;
			if (b != null)
				CalculatorViewModel.Input += b.Content.ToString();
		}

		private void OnDefineOperation(object sender, RoutedEventArgs e)
		{
			var b = e.Source as Button;
			if (b != null)
				CalculatorViewModel.CurrentOperation = b.Tag as IOperation;
		}
	}
}