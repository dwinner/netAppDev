﻿using System;

namespace CustomEnumerator
{
   internal class Car
   {
      // Constant for maximum speed.
      public const int MaxSpeed = 100;

      // Is the car still operational?
      private bool carIsDead;

      // A car has-a radio.
      private readonly Radio theMusicBox = new Radio();

      // Constructors.
      public Car()
      {
      }

      public Car(string name, int speed)
      {
         CurrentSpeed = speed;
         PetName = name;
      }

      // Car properties.
      public int CurrentSpeed { get; set; }
      public string PetName { get; set; }

      public void CrankTunes(bool state)
      {
         // Delegate request to inner object.
         theMusicBox.TurnOn(state);
      }

      // See if Car has overheated.
      public void Accelerate(int delta)
      {
         if (carIsDead)
         {
            Console.WriteLine("{0} is out of order...", PetName);
         }
         else
         {
            CurrentSpeed += delta;
            if (CurrentSpeed >= MaxSpeed)
            {
               carIsDead = true;
               CurrentSpeed = 0;

               // We need to call the HelpLink property, thus we need
               // to create a local variable before throwing the Exception object.
               var ex =
                  new Exception(string.Format("{0} has overheated!", PetName));
               ex.HelpLink = "http://www.CarsRUs.com";

               // Stuff in custom data regarding the error.
               ex.Data.Add("TimeStamp",
                  string.Format("The car exploded at {0}", DateTime.Now));
               ex.Data.Add("Cause", "You have a lead foot.");
               throw ex;
            }

            Console.WriteLine("=> CurrentSpeed = {0}", CurrentSpeed);
         }
      }
   }
}