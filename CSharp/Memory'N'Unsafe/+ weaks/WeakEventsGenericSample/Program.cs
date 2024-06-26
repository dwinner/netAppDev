﻿/**
 * Обобщенный диспетчер слабых событий (.NET Framework 4.5+)
 */

using System.Windows;

namespace Wrox.ProCSharp.Delegates
{
   static class Program
   {
      static void Main()
      {
         var dealer = new CarDealer();

         var michael = new Consumer("Michael");
         WeakEventManager<CarDealer, CarInfoEventArgs>.AddHandler(dealer, "NewCarInfo", michael.NewCarIsHere);

         dealer.NewCar("Mercedes");

         var sebastian = new Consumer("Sebastian");
         WeakEventManager<CarDealer, CarInfoEventArgs>.AddHandler(dealer, "NewCarInfo", sebastian.NewCarIsHere);

         dealer.NewCar("Ferrari");

         WeakEventManager<CarDealer, CarInfoEventArgs>.RemoveHandler(dealer, "NewCarInfo", michael.NewCarIsHere);

         dealer.NewCar("Red Bull Racing");
      }
   }
}
