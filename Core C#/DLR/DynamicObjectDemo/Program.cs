/**
 * Объекты с динамическим поведением
 */

using System;
using System.Collections.Generic;
using DynamicSample;

namespace DynamicObjectDemo
{
   static class Program
   {
      static void Main()
      {
         dynamic dyn = new WroxDynamicObject();
         dyn.FirstName = "Bugs";
         dyn.LastName = "Bunny";
         Console.WriteLine(dyn.GetType());
         Console.WriteLine("{0} {1}", dyn.FirstName, dyn.LastName);

         dyn.MiddleName = "Rabbit";
         Console.WriteLine(dyn.MiddleName);
         Console.WriteLine(dyn.GetType());
         Console.WriteLine("{0} {1} {2}", dyn.FirstName, dyn.MiddleName, dyn.LastName);

         dyn.Friends = new List<Person>(new[]
         {
            new Person { FirstName = "Daffy", LastName = "Duck" },
            new Person { FirstName = "Porky", LastName = "Pig" },
            new Person { FirstName = "Tweety", LastName = "Bird" } 
         });

         foreach (var friend in dyn.Friends)
         {
            Console.WriteLine("{0} {1}", friend.FirstName, friend.LastName);
         }

         dyn.GetTomorrowDate = (Func<DateTime, string>)(today => today.AddDays(1).ToShortDateString());
         Console.WriteLine("Tomorrow is {0}", dyn.GetTomorrowDate(DateTime.Now));
      }
   }
}
