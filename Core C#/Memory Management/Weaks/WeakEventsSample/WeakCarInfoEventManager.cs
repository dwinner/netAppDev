using System.Windows;

namespace Wrox.ProCSharp.Delegates
{
   public class WeakCarInfoEventManager : WeakEventManager
   {
      public static void AddListener(object source, IWeakEventListener listener)
      {
         CurrentManager.ProtectedAddListener(source, listener);
      }

      public static void RemoveListener(object source, IWeakEventListener listener)
      {
         CurrentManager.ProtectedRemoveListener(source, listener);
      }

      public static WeakCarInfoEventManager CurrentManager
      {
         get
         {
            var manager = GetCurrentManager(typeof (WeakCarInfoEventManager)) as WeakCarInfoEventManager;
            if (manager != null)
               return manager;
            manager = new WeakCarInfoEventManager();
            SetCurrentManager(typeof(WeakCarInfoEventManager), manager);
            return manager;
         }
      }

      protected override void StartListening(object source)
      {
         var carDealer = source as CarDealer;
         if (carDealer != null)
            carDealer.NewCarInfo += CarDealer_NewCarInfo;
      }

      void CarDealer_NewCarInfo(object sender, CarInfoEventArgs e)
      {
         DeliverEvent(sender, e);
      }

      protected override void StopListening(object source)
      {
         var carDealer = source as CarDealer;
         if (carDealer != null)
            carDealer.NewCarInfo -= CarDealer_NewCarInfo;
      }
   }
}
