using System;

namespace CustomException
{
   class Car
   {
      public const int MaxSpeed = 100; // Максимальная скорость

      // Свойства автомобиля.
      public int CurrentSpeed { get; set; }
      public string PetName { get; set; }

      private bool carIsDead; // На ходу ли машина?

      private Radio theMusicBox = new Radio();  // У автомобиля есть радио.

      // Конструкторы.
      public Car() { }

      public Car(string name, int speed)
      {
         CurrentSpeed = speed;
         PetName = name;
      }

      public void CrankTunes(bool state)
      {
         theMusicBox.TurnOn(state);
      }

      // See if Car has overheated.
      public void Accelerate(int delta)
      {
         if (carIsDead)
            Console.WriteLine("{0} is out of order...", PetName);
         else
         {
            CurrentSpeed += delta;
            if (CurrentSpeed >= MaxSpeed)
            {
               carIsDead = true;
               CurrentSpeed = 0;

               // We need to call the HelpLink property, thus we need
               // to create a local variable before throwing the Exception object.
               CarIsDeadException ex =
                  new CarIsDeadException(string.Format("{0} has overheated!", PetName),
                                         "You have a lead foot",
                                         DateTime.Now);
               ex.HelpLink = "http://www.CarsRUs.com";
               throw ex;
            }
            else
               Console.WriteLine("=> CurrentSpeed = {0}", CurrentSpeed);
         }
      }


   }
}
