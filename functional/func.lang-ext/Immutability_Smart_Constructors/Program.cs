using System;

namespace Immutability_Smart_Constructors;

// This tutorial demonstrates creating immutable objects using Smart Constructors, and using them as you would use any other OOP objects.
// However, this object is immutable in as much as its specifically designed not to have its state changed by operations
// The operations that it does have, create a new object with the modification and leaves the original object untouched.
internal static class Program
{
   private static void Main()
   {
      // Note we cannot create a person through a conventional constructor
      // we use a smart constructor ie Of() to perform validation on construction and return an Option<Person> depending the construction
      // of the person is valid - no exceptions are thrown when they are not - which is usually what happens with normal constructors.

      var originalMe = Person.Of("Steward", "Mathews");


      // Perform a state change on the person, however we enforce creation of a new copy of person, pre-initialized with previous data. 
      // We therefore don't modify state - particularly of the person that we are about to modify - this is not a member function that changes te object as is traditionally done
      var changedMe =
         originalMe.Bind(person =>
            person.Rename("Steward Rob Charles",
               "Mathews")); // Remember we're binding here as Rename returns a Monad already if it didn't we'd use Map()

      // We can do this declaratively also
      var stateChangedMe1 = from person in originalMe
         from renamed in person.Rename("Steward Rob Charles", "Mathews")
         select renamed;

      // We can also chain the modifications using the extension method

      var lastChange = changedMe
         .Rename("Stuart", "Mathews")
         .Rename("Stuart Robert Charles", "Mathews");

      Console.WriteLine($"First I was {originalMe}, then ultimately I was {lastChange}");
   }
}