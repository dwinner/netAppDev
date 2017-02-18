using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmPrimer.Infrastructure;
using MvvmPrimer.Models;

namespace MvvmPrimer.ViewModels
{
	public sealed class ApplicationViewModel : ViewModelBase
	{
		private PhoneViewModel _selectedPhone;

		public ObservableCollection<PhoneViewModel> Phones { get; } =
			new ObservableCollection<PhoneViewModel>(new List<PhoneViewModel>
			{
				new PhoneViewModel(new Phone {Title = "iPhone 7", Company = "Apple", Price = 56000}),
				new PhoneViewModel(new Phone {Title = "Galaxy S7 Edge", Company = "Samsung", Price = 60000}),
				new PhoneViewModel(new Phone {Title = "Elite X3", Company = "HP", Price = 56000}),
				new PhoneViewModel(new Phone {Title = "Mi5S", Company = "Xiaomi", Price = 35000})
			});

		public PhoneViewModel SelectedPhone
		{
			get { return _selectedPhone; }
			set
			{
				_selectedPhone = value;
				OnPropertyChanged();
			}
		}
	}
}