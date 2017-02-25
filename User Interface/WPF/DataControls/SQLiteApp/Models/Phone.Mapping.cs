using AutoMapper;
using SQLiteApp.ViewModels;

namespace SQLiteApp.Models
{
	public sealed partial class Phone
	{
		public static implicit operator PhoneViewModel(Phone phone)
			=> Mapper.Map<PhoneViewModel>(phone);
	}
}