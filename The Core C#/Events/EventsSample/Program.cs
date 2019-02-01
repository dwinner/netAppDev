/**
 * События
 */

using System;

namespace EventsSample
{
   static class Program
   {
      static void Main()
      {
         var publisher = new CarDealer();

         var michaelConsumer = new Consumer("Michael");
         publisher.NewCarInfo += michaelConsumer.Notify;

         publisher.NewCar("Ferrari");

         var nickConsumer = new Consumer("Sebastian");
         publisher.NewCarInfo += nickConsumer.Notify;

         publisher.NewCar("Mersedes");

         publisher.NewCarInfo -= michaelConsumer.Notify;

         publisher.NewCar("Red Bull Racing");

         Console.ReadLine();
      }
   }
}
