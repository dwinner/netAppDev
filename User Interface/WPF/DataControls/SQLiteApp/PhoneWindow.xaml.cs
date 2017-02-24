using System.Windows;
using SQLiteApp.Models;

namespace SQLiteApp
{
	public partial class PhoneWindow
	{
		public PhoneWindow(Phone phone)
		{
			InitializeComponent();

			Phone = phone;
			DataContext = Phone;
		}

		public Phone Phone { get; }

		private void OnAccept(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}