using MvvmPrimer.Annotations;
using MvvmPrimer.Infrastructure;
using MvvmPrimer.Models;

namespace MvvmPrimer.ViewModels
{
	public sealed class PhoneViewModel : ViewModelBase
	{
		private string _company;
		private int _price;
		private string _title;		

		public PhoneViewModel([NotNull] Phone phone)
		{
			Title = phone.Title;
			Company = phone.Company;
			Price = phone.Price;
		}

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				OnPropertyChanged();
			}
		}

		public string Company
		{
			get { return _company; }
			set
			{
				_company = value;
				OnPropertyChanged();
			}
		}

		public int Price
		{
			get { return _price; }
			set
			{
				_price = value;
				OnPropertyChanged();
			}
		}
	}
}