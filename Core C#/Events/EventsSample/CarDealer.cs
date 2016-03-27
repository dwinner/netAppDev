using System;

namespace EventsSample
{
   public class CarDealer
   {
      public event EventHandler<CarInfoEventArgs> NewCarInfo;

      public void NewCar(string car)
      {
         Console.WriteLine("Car Dealer, new car {0}", car);
         RaiseNewCarInfo(car);
      }

      protected virtual void RaiseNewCarInfo(string car)
      {
         EventHandler<CarInfoEventArgs> onNewCarInfo = NewCarInfo;
         if (onNewCarInfo != null)
         {
            onNewCarInfo(this, new CarInfoEventArgs(car));
         }
      }
   }
}