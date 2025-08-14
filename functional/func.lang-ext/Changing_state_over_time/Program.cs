using System;
using System.Collections.Generic;

namespace Changing_state_over_time;

// This tutorial shows how you can use the Fold() function in languageExt
// to change the state of an object over time
internal static class Program
{
   private static void Main()
   {
      var years = new List<int>
      {
         1987, 1988, 1989, 1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000,
         2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014,
         2015, 2016, 2017, 2018, 2019
      };

      var events = new List<Event<Person>>
      {
         new ChangeNameEvent("Stuart"),
         new ChangeExpertiseEvent("Programming"),
         new ChangeNameEvent("Stuart Mathews"),
         new ChangeExpertiseEvent("Running"),
         new ChangeAgeEvent(33)
      };

      var person = new Person();

      // A state (InitialResult) changes over time, and it changes using results of the previous change.
      // It uses an item from the array in changing the state each time.
      // The state changes the number of elements in the IEnumerable
      // For a Lst which has multiple items in it, the state will change that many times

      // NB: years represent the state changes that will occur
      // Apply some events to the person over time
      var changedPerson = events.Fold(person, (prev, evt) => evt.ApplyEventTo(prev));

      // View the changed person
      Console.WriteLine($"{changedPerson}");
      return;

      // local function
      Person ChangeState(int year, Person previousResult)
      {
         var updatedPerson = new Person(previousResult);
         updatedPerson.History.Add($"\nIn {year}, this person was {updatedPerson.Age} years old");
         updatedPerson.Age++;
         return updatedPerson;
      }
   }
}