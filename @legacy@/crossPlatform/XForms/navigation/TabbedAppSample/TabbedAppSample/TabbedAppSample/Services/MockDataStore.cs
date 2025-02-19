﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabbedAppSample.Models;

namespace TabbedAppSample.Services
{
   public class MockDataStore : IDataStore<Item>
   {
      private readonly List<Item> _items;

      public MockDataStore()
      {
         _items = new List<Item>();
         var mockItems = new List<Item>
         {
            new Item
            {
               Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(), Text = "Second item", Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(), Text = "Third item", Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description = "This is an item description."
            },
            new Item
            {
               Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description = "This is an item description."
            },
            new Item {Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description = "This is an item description."}
         };

         foreach (var item in mockItems)
         {
            _items.Add(item);
         }
      }

      public Task<bool> AddItemAsync(Item item)
      {
         _items.Add(item);

         return Task.FromResult(true);
      }

      public async Task<bool> UpdateItemAsync(Item item)
      {
         var oldItem = _items.Where(arg => arg.Id == item.Id).FirstOrDefault();
         _items.Remove(oldItem);
         _items.Add(item);

         return await Task.FromResult(true);
      }

      public async Task<bool> DeleteItemAsync(string id)
      {
         var oldItem = _items.Where(arg => arg.Id == id).FirstOrDefault();
         _items.Remove(oldItem);

         return await Task.FromResult(true);
      }

      public async Task<Item> GetItemAsync(string id)
      {
         return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
      }

      public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false) => await Task.FromResult(_items);
   }
}