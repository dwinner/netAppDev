using System;
using System.Windows;
using DynamicTab.Lib.Framework;
using DynamicTab.Lib.Services;
using DynamicTab.Lib.ViewModels;
using DynamicTabApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicTabApp
{
	public partial class App
	{
		public IServiceProvider Container { get; private set; }

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			RegisterServices();
		}

		private void RegisterServices()
		{
			var services = new ServiceCollection();
			services.AddSingleton<IItemsService, ItemsService>();
			services.AddSingleton<IEventAggregator, EventAggregator>();
			services.AddSingleton<IOpenItemsDetailService, OpenItemsDetailService>();
			services.AddSingleton<IItemDetailViewModelFactory, ItemDetailViewModelFactory>();
			services.AddTransient<ListViewModel>();
			services.AddTransient<TabViewModel>();
			services.AddTransient<ItemDetailViewModel>();

			Container = services.BuildServiceProvider();
		}
	}
}