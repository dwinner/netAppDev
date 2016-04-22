using System;

namespace CarEvents
{
   public class Car
   {
      public int CurrentSpeed { get; set; }
      public int MaxSpeed { get; set; }
      public string PetName { get; set; }

      private bool carIsDead;

      public Car()
      {
         MaxSpeed = 100;
      }

      public Car(string name, int maxSp, int currSp)
      {
         CurrentSpeed = currSp;
         MaxSpeed = maxSp;
         PetName = name;
      }

      #region Delegate / Event архитектура
      // 1) Определяем делегат.
      public delegate void CarEngineHandler(string msgForCaller);

      // Эта машина может посылать данные события.
      public event CarEngineHandler Exploded;
      public event CarEngineHandler AboutToBlow;

      public void Accelerate(int delta)
      {
         // Если машина мертва, генерируем событие Exploded
         if (carIsDead)
         {
            if (Exploded != null)
               Exploded("Sorry, this car is dead...");
         }
         else
         {
            CurrentSpeed += delta;

            // Почти мертва
            if ((MaxSpeed - CurrentSpeed) == 10 && AboutToBlow != null)
            {
               AboutToBlow("Careful buddy!  Gonna blow!");
            }

            // Пока всё норм
            if (CurrentSpeed >= MaxSpeed)
               carIsDead = true;
            else
               Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
         }
      }
      #endregion
   }
}
