using System.Windows;
using System.Windows.Forms.Integration;

namespace PrimerInteroperability
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnDialogShow(object sender, RoutedEventArgs e)
		{
			var form = new ToHostForm();
			if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				MessageBox.Show("You clicked OK.");
		}

		private void OnModallyShow(object sender, RoutedEventArgs e)
		{
			var form = new ToHostForm();
			form.Show();
		}

		private void OnModelessEnable(object sender, RoutedEventArgs e)
			=> WindowsFormsHost.EnableWindowsFormsInterop();

		private void OnHostWinFormControl(object sender, RoutedEventArgs e)
		{
			var control = new HostWinFormControl();
			control.Show();
		}

		private void OnWpfMnemonicTest(object sender, RoutedEventArgs e)
		{
			var mnemonicTest = new WpfMnemonicTest();
			mnemonicTest.ShowDialog();
		}

		private void OnWinFormsMnemonicTest(object sender, RoutedEventArgs e)
		{
			var mnemonicTest = new WinFormsMnemonicTest();
			mnemonicTest.ShowDialog();
		}
	}
}