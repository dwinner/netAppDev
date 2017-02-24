using System.Data.Entity;
using System.Windows;
using SQLiteApp.Infrastructure;
using SQLiteApp.Models;

namespace SQLiteApp
{
	public partial class MainWindow
	{
		private readonly ApplicationContext _appCtx;

		public MainWindow()
		{
			InitializeComponent();

			_appCtx = new ApplicationContext();
			_appCtx.Phones.Load();
			DataContext = _appCtx.Phones.Local.ToBindingList();
		}

		private async void OnAddAsync(object sender, RoutedEventArgs e) // Добавление
		{
			var phoneWindow = new PhoneWindow(new Phone());
			if (phoneWindow.ShowDialog() == true)
			{
				var newPhone = phoneWindow.Phone;
				_appCtx.Phones.Add(newPhone);
				await _appCtx.SaveChangesAsync().ConfigureAwait(true);
			}
		}

		private async void OnEditAsync(object sender, RoutedEventArgs e) // Редактирование
		{
			var selectedItem = PhonesListBox.SelectedItem;
			var phone = selectedItem as Phone;
			if (phone != null)
			{
				var phoneWindow = new PhoneWindow(new Phone
				{
					Id = phone.Id,
					Company = phone.Company,
					Price = phone.Price,
					Title = phone.Title
				});

				if (phoneWindow.ShowDialog() == true)
				{
					phone = await _appCtx.Phones.FindAsync(phoneWindow.Phone.Id).ConfigureAwait(true);
					if (phone != null)
					{
						phone.Company = phoneWindow.Phone.Company;
						phone.Title = phoneWindow.Phone.Title;
						phone.Price = phoneWindow.Phone.Price;
						_appCtx.Entry(phone).State = EntityState.Modified;
						await _appCtx.SaveChangesAsync().ConfigureAwait(true);
					}
				}
			}
		}

		private async void OnRemoveAsync(object sender, RoutedEventArgs e)
		{
			var phone = PhonesListBox.SelectedItem as Phone;
			if (phone != null)
			{
				_appCtx.Phones.Remove(phone);
				await _appCtx.SaveChangesAsync().ConfigureAwait(true);
			}
		}
	}
}