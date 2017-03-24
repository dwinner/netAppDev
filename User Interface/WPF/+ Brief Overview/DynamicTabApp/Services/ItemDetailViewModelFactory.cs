using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DynamicTab.Lib.Models;
using DynamicTab.Lib.Services;
using DynamicTab.Lib.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicTabApp.Services
{
	public class ItemDetailViewModelFactory : IItemDetailViewModelFactory
	{
		private readonly IOpenItemsDetailService _openItemsDetailService;

		public ItemDetailViewModelFactory(IOpenItemsDetailService openItemsDetailService)
		{
			_openItemsDetailService = openItemsDetailService;
		}

		public IEnumerable<ItemDetailViewModel> GetItemDetailViewModels(IEnumerable<ItemDetail> itemDetails)
		{
			var app = Application.Current as App;
			if (app == null)
				throw new InvalidOperationException("app cannot be null");

			var openedItemDetails = _openItemsDetailService.CurrentItemDetails.Select(vm => vm.ItemDetail);
			foreach (var itemDetail in itemDetails.Where(detail => !openedItemDetails.Contains(detail)))
			{
				var itemDetailViewModel = app.Container.GetService<ItemDetailViewModel>();
				if (itemDetailViewModel != null)
				{
					itemDetailViewModel.ItemDetail = itemDetail;
					yield return itemDetailViewModel;
				}
			}
		}
	}
}