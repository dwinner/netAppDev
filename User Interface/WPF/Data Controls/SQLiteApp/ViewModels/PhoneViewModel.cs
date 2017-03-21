using AutoMapper;
using PostSharp.Patterns.Contracts;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SQLiteApp.Models;

namespace SQLiteApp.ViewModels
{
	public sealed class PhoneViewModel : ReactiveObject
	{
		public PhoneViewModel(Phone phone)
		{
			Id = phone.Id;
			Title = phone.Title;
			Company = phone.Company;
			Price = phone.Price;
		}

		public PhoneViewModel(PhoneViewModel phoneViewModel)
		{
			Id = phoneViewModel.Id;
			Title = phoneViewModel.Title;
			Company = phoneViewModel.Company;
			Price = phoneViewModel.Price;
		}

		public PhoneViewModel()
		{
		}

		public int Id { get; }

		[Required]
		[Reactive]
		public string Title { get; set; }

		[Required]
		[Reactive]
		public string Company { get; set; }

		[StrictlyPositive]
		[Reactive]
		public int Price { get; set; }

		public static implicit operator Phone(PhoneViewModel phoneViewModel)
			=> Mapper.Map<Phone>(phoneViewModel);
	}
}