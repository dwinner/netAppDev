﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AuthDemoXForms.Models;
using AuthDemoXForms.Views;
using Xamarin.Forms;

namespace AuthDemoXForms.ViewModels
{
   public class ItemsViewModel : BaseViewModel
   {
      public ItemsViewModel()
      {
         Title = "Browse";
         Items = new ObservableCollection<Item>();
         LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

         MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
         {
            var newItem = item;
            Items.Add(newItem);
            await DataStore.AddItemAsync(newItem);
         });
      }

      public ObservableCollection<Item> Items { get; set; }
      public Command LoadItemsCommand { get; set; }

      private async Task ExecuteLoadItemsCommand()
      {
         if (IsBusy)
         {
            return;
         }

         IsBusy = true;

         try
         {
            Items.Clear();
            var items = await DataStore.GetItemsAsync(true);
            foreach (var item in items)
            {
               Items.Add(item);
            }
         }
         catch (Exception ex)
         {
            Debug.WriteLine(ex);
         }
         finally
         {
            IsBusy = false;
         }
      }
   }
}