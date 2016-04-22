/*
 * Сделано в SharpDevelop.
 * Пользователь: Denis
 * Дата: 30.11.2012
 * Время: 8:10
 *  
 */
using System;

namespace SimpleException
{
   public class Car
   {
      public const int MaxSpeed = 100; // Допустимая максимальная скорость

      #region Свойства автомобиля

      public int CurrentSpeed { get; set; }
      public string PetName { get; set; }

      #endregion

      private bool carIsDead; // Не вышел ли автомобиль из строя

      private Radio theMusicBox = new Radio();

      public Car() { }

      public Car(string name, int speed)
      {
         CurrentSpeed = speed;
         PetName = name;
      }

      public void CrankTunes(bool state)
      {
         // Запрос делегата к внутреннему объекту.
         theMusicBox.TurnOn(state);
      }

      // Проверка, не перегрелся ли автомобиль.
      public void Accelerate(int delta)
      {
         if (carIsDead)
            Console.WriteLine("{0} is out of order...",
                             PetName); // вышел из строя
         else
         {
            CurrentSpeed += delta;
            if (CurrentSpeed >= MaxSpeed)
            {
               CurrentSpeed = 0;
               carIsDead = true;

               // Создание локальной переменной перед выдачей объекта Exception
               // для получения возможности обращения к свойству HelpLink.
               Exception ex = new Exception(string.Format("{0} has overheated!", PetName));
               ex.HelpLink = "http://www.CarsUrl.com";

               // Вставка специальных дополнительных данных, имеющих отношение к ошибке.
               ex.Data.Add("Time stamp",
                           string.Format("The car exploded at {0}", DateTime.Now)); // Дата и время
               ex.Data.Add("Cause",
                           "You have a lead foot."); // Причина

               throw ex;
            }
            else
               Console.WriteLine("=> CurrentSpeed = {0}",
                                 CurrentSpeed); // Вывод текущей скорости.
         }
      }
   }
}
