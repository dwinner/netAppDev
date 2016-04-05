using System;

namespace CarDelegate
{
   public class Car
   {
      // Свойства.
      public int CurrentSpeed { get; set; }
      public int MaxSpeed { get; set; }
      public string PetName { get; set; }

      // "Жива" ли машина?
      private bool carIsDead;

      // Конструкторы.
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

      #region Архитектура делегата для класса
      // 1) Определяем тип делегата.
      public delegate void CarEngineHandler(string msgForCaller);

      // 2) Определяем поле класса делегата.
      private CarEngineHandler listOfHandlers;

      // 3) Добавляем регистрационную функцию для вызова/вызовов.
      public void RegisterWithCarEngine(CarEngineHandler methodToCall)
      {
         // listOfHandlers = methodToCall;
         // listOfHandlers += methodToCall; 
         // listOfHandlers += methodToCall; 
         if (listOfHandlers == null)
            listOfHandlers = methodToCall;
         else
            Delegate.Combine(listOfHandlers, methodToCall);
      }

      public void UnRegisterWithCarEngine(CarEngineHandler methodToCall)
      {
         listOfHandlers -= methodToCall;
      }

      // 4) Реализуем метод Accelerate() для обратных вызовов методов делегата.
      public void Accelerate(int delta)
      {
         // Если машина "мертва", послать сообщение.
         if (carIsDead)
         {
            if (listOfHandlers != null)
               listOfHandlers("Sorry, this car is dead...");
         }
         else
         {
            CurrentSpeed += delta;

            // Если машина на грани "смерти"
            if (10 == (MaxSpeed - CurrentSpeed) && listOfHandlers != null)
            {
               listOfHandlers("Careful buddy!  Gonna blow!");
            }

            if (CurrentSpeed >= MaxSpeed)
               carIsDead = true;
            else
               Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
         }
      }
      #endregion
   }
}
