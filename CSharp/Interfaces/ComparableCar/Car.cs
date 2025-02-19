using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace ComparableCar
{
   public class Car : IComparable
   {
      #region Member variables & Constructors

      // Constant for maximum speed.
      public const int MaxSpeed = 100;

      // Internal state data.
      public int CurrentSpeed { get; set; }      
      public string PetName { get; set; }      
      public int CarID { get; set; }

      // Is the car still operational?
      private bool carIsDead;

      // A car has-a radio.
      private Radio theMusicBox = new Radio();

      // Constructors.
      public Car() { }

      public Car(string name, int currSp, int id)
      {
         CurrentSpeed = currSp;
         PetName = name;
         CarID = id;
      }
      #endregion

      #region Methods
      public void CrankTunes(bool state)
      {
         // Delegate request to inner object.
         theMusicBox.TurnOn(state);
      }

      // This time, throw an exception if the user speeds up beyond MaxSpeed.
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
               Exception ex =
                 new Exception(string.Format("{0} has overheated!", PetName));
               ex.HelpLink = "http://www.CarsRUs.com";

               // Stuff in custom data regarding the error.
               ex.Data.Add("TimeStamp",
                 string.Format("The car exploded at {0}", DateTime.Now));
               ex.Data.Add("Cause", "You have a lead foot.");
               throw ex;
            }
            else
               Console.WriteLine("=> CurrSpeed = {0}", CurrentSpeed);
         }
      }
      #endregion

      #region IComparable Members
      //int IComparable.CompareTo(object obj)
      //{
      //    Car temp = obj as Car;
      //    if (temp != null)
      //    {
      //        if (this.CarID > temp.CarID)
      //            return 1;
      //        if (this.CarID < temp.CarID)
      //            return -1;
      //        else
      //            return 0;
      //    }
      //    else
      //        throw new ArgumentException("Parameter is not a Car!");
      //}

      int IComparable.CompareTo(object obj)
      {
         Car temp = obj as Car;
         if (temp != null)
            return CarID.CompareTo(temp.CarID);
         else
            throw new ArgumentException("Parameter is not a Car!");
      }
      #endregion

      // Property to return the SortByPetName comparer.
      public static IComparer SortByPetName
      {
         get
         {
            return (IComparer) new PetNameComparer();
         }
      }
   }
}
