using AutoMapper;
using SQLiteApp.ViewModels;

namespace SQLiteApp.Models
{
	public sealed partial class Phone
	{
		public Phone(PhoneViewModel phoneViewModel)
		{
			Id = phoneViewModel.Id;
			Title = phoneViewModel.Title;
			Company = phoneViewModel.Company;
			Price = phoneViewModel.Price;
		}

		public static implicit operator PhoneViewModel(Phone phone)
			=> Mapper.Map<PhoneViewModel>(phone);
	}
}