using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CalculatorContract;
using CalculatorViewModels;
using SimpleCalculator;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPCalculatorHost
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
			ViewModel = new CalculatorViewModel();
			ViewModel.Init(typeof(Calculator));
		}

		public CalculatorViewModel ViewModel { get; }

		public void OnNumberClick(object sender, RoutedEventArgs e)
		{
			var b = sender as Button;
			if (b != null)
			{
				ViewModel.Input += b.Content.ToString();
			}
		}

		public void DefineOperation(object sender, RoutedEventArgs e)
		{
			var b = sender as Button;
			if (b != null)
			{
				ViewModel.CurrentOperation = b.Tag as IOperation;
			}
		}

	}
}
