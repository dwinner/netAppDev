/*
 * Облегченный объект с динамическим поведением
 */

using System;
using System.Collections.Generic;
using System.Dynamic;
using DynamicSample;

namespace ExpandObjectDemo
{
   static class Program
   {
      static void Main()
      {
         dynamic expObject = new ExpandoObject();
         expObject.FirstName = "Daffy";
         expObject.LastName = "Duck";
         Console.WriteLine(expObject.FirstName + " " + expObject.LastName);
         Func<DateTime, string> getTomorrow = time => time.AddDays(1).ToShortDateString();
         expObject.GetTomorrowDate = getTomorrow;
         Console.WriteLine("Tomorrow is {0}", expObject.GetTomorrowDate(DateTime.Now));

         expObject.Friends = new List<Person>();
         expObject.Friends.Add(new Person { FirstName = "Bob", LastName = "Jones" });
         expObject.Friends.Add(new Person { FirstName = "Robert", LastName = "Jones" });
         expObject.Friends.Add(new Person { FirstName = "Bobby", LastName = "Jones" });

         foreach (Person friend in expObject.Friends)
         {
            Console.WriteLine(friend.FirstName + "  " + friend.LastName);
         }
      }
   }
}
