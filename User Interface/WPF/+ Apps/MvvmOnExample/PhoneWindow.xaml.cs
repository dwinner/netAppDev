using System.Windows;
using SQLiteApp.ViewModels;

namespace SQLiteApp
{
	public partial class PhoneWindow
	{
		public PhoneWindow(PhoneViewModel phoneViewModel)
		{
			InitializeComponent();

			PhoneViewModel = phoneViewModel;
			DataContext = PhoneViewModel;
		}

		public PhoneViewModel PhoneViewModel { get; }

		private void OnAccept(object sender, RoutedEventArgs e) => DialogResult = true;
	}
}