using System;

namespace DeconstructionSample
{
   internal static class Program
   {
      private static void Main()
      {
         var joe = new Person("Joe Bloggs");
         var (first, last) = joe; // NOTE: Deconstruction
         Console.WriteLine(first);
         Console.WriteLine(last);
      }
   }

   internal class Person
   {
      private string _name;

      public Person(string name) => _name = name;

      public string Name
      {
         get => _name;
         set => _name = value ?? string.Empty;
      }

      ~Person() => Console.WriteLine("finalize");

      public void Deconstruct(out string firstName, out string lastName)
      {
         var spacePos = _name.IndexOf(' ');
         firstName = _name.Substring(0, spacePos);
         lastName = _name.Substring(spacePos + 1);
      }
   }
}