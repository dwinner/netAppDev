using AutoMapper;
using SQLiteApp.Models;
using SQLiteApp.ViewModels;

namespace SQLiteApp.StartUp
{
	internal static class AutoMapperConfig
	{
		internal static void ConfigureViewModelMapping() => Mapper.Initialize(Config);

		private static void Config(IMapperConfigurationExpression expression)
		{
			#region Phone => PhoneViewModel mapping

			expression.CreateMap<Phone, PhoneViewModel>()
				.ForMember(phoneViewModel => phoneViewModel.Id,
					configExpr => configExpr.MapFrom(phone => phone.Id))
				.ForMember(phoneViewModel => phoneViewModel.Title,
					configExpr => configExpr.MapFrom(phone => phone.Title))
				.ForMember(phoneViewModel => phoneViewModel.Company,
					configExpr => configExpr.MapFrom(phone => phone.Company))
				.ForMember(phoneViewModel => phoneViewModel.Price,
					configExpr => configExpr.MapFrom(phone => phone.Price));

			#endregion

			#region PhoneViewModel => Phone mapping

			expression.CreateMap<PhoneViewModel, Phone>()
				.ForMember(phone => phone.Id,
					configExpr => configExpr.MapFrom(phoneViewModel => phoneViewModel.Id))
				.ForMember(phone => phone.Title,
					configExpr => configExpr.MapFrom(phoneViewModel => phoneViewModel.Title))
				.ForMember(phone => phone.Company,
					configExpr => configExpr.MapFrom(phoneViewModel => phoneViewModel.Company))
				.ForMember(phone => phone.Price,
					configExpr => configExpr.MapFrom(phoneViewModel => phoneViewModel.Price));

			#endregion
		}
	}
}