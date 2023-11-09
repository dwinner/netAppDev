using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using MoreLinq;
using ShoppyExample.Domain;

namespace ShoppyExample;

public class Shoppy
{
   public static void TestShoppy()
   {
      StoreIconExample();
      ConnectivityExample();
   }

   [Description("Shows how you can combine multiple observables")]
   private static void StoreIconExample()
   {
      const double minIconSize = 20;
      const double maxIconSize = 32;

      var myLocation = CreateDummyLocations();
      var stores = CreateDummyStores();

      var storeLocation = stores.ToObservable()
         .SelectMany(store => myLocation, (store, currentLocation) => new { store, currentLocation });

      var iconSize = from store in stores.ToObservable()
         from currentLocation in myLocation
         let dist = Position.Distance(store.Location, currentLocation)
         where dist < 5
         let calcSize = 5 / dist * minIconSize
         let sizeOrMax = Math.Min(calcSize, maxIconSize)
         let sizeOrMin = Math.Max(sizeOrMax, minIconSize)
         select
            new
            {
               store.Name,
               StoreX = store.Location.X,
               StoreY = store.Location.Y,
               MeX = currentLocation.X,
               MeY = currentLocation.Y,
               dist,
               calcSize,
               sizeOrMin,
               sizeOrMax
            };

      iconSize.Subscribe(d => Debug.WriteLine(d));
   }

   [Description("Shows how asynchrnous code execution get be part of the observable pipline")]
   private static void ConnectivityExample()
   {
      var myConnectivity = Observable.Empty<Connectivity>();
      IObservable<IEnumerable<Discount>> newDiscounts =
         from connectivity in myConnectivity
         where connectivity == Connectivity.Online
         from discounts in GetDiscountsAsync()
         select discounts;

      newDiscounts.Subscribe(RefreshView);
   }

   #region Helper Methods

   private static IObservable<Position> CreateDummyLocations() =>
      Observable.Range(1, 50).Select(i => new Position
      {
         X = i,
         Y = i * 2
      });

   private static Store[] CreateDummyStores() =>
      new[]
      {
         new Store
         {
            Location = new Position { X = 10, Y = 15 },
            Name = "ShopA"
         },

         new Store
         {
            Location = new Position { X = 2, Y = 3 },
            Name = "ShopB"
         },

         new Store
         {
            Location = new Position { X = 7, Y = 12 },
            Name = "ShopC"
         }
      };

   private static void RefreshView(IEnumerable<Discount> discounts) => discounts.ForEach(Console.WriteLine);

   private static Task<IEnumerable<Discount>> GetDiscountsAsync() =>
      //Sends request to the server and recieves the collection of discounts
      Task.FromResult(Enumerable.Empty<Discount>());

   #endregion
}