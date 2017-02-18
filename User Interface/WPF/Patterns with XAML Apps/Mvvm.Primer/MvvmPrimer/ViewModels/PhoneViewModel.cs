using MvvmPrimer.Infrastructure;
using MvvmPrimer.Models;

namespace MvvmPrimer.ViewModels
{
	public sealed class PhoneViewModel : ViewModelBase
	{
		public PhoneViewModel()
		{
			Phone = new Phone {Company = "New company", Price = 0, Title = "New title"};
		}

		public PhoneViewModel(Phone phone)
		{
			Phone = phone;
		}

		public string Title
		{
			get { return Phone.Title; }
			set
			{
				Phone.Title = value;
				OnPropertyChanged();
			}
		}

		public string Company
		{
			get { return Phone.Company; }
			set
			{
				Phone.Company = value;
				OnPropertyChanged();
			}
		}

		public int Price
		{
			get { return Phone.Price; }
			set
			{
				Phone.Price = value;
				OnPropertyChanged();
			}
		}

		public Phone Phone { get; }

		public override string ToString() => Phone.ToString();
	}
}