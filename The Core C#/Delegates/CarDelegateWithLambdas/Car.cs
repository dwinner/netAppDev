#region Using directives

using System;


#endregion

namespace CarEventsWithLambdas
{
   public class Car
   {
      #region Basic Car state data / constructors
      // Internal state data.
      public int CurrentSpeed { get; set; }
      public int MaxSpeed { get; set; }
      public string PetName { get; set; }

      // Is the car alive or dead?
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
      #endregion

      public delegate void CarEngineHandler(object sender, CarEventArgs e);

      public event CarEngineHandler Exploded;
      public event CarEngineHandler AboutToBlow;

      public void Accelerate(int delta)
      {
         if (carIsDead)
         {
            if (Exploded != null)
               Exploded(this, new CarEventArgs("Sorry, this car is dead..."));
         }
         else
         {
            CurrentSpeed += delta;

            if (10 == MaxSpeed - CurrentSpeed && AboutToBlow != null)
            {
               AboutToBlow(this, new CarEventArgs("Careful buddy!  Gonna blow!"));
            }

            if (CurrentSpeed >= MaxSpeed)
               carIsDead = true;
            else
               Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
         }
      }
   }
}
