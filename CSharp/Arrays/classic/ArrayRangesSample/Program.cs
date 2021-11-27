using System;

namespace ArrayRangesSample
{
   static class Program
   {
      static void Main()
      {
         string[] people = {"Tom", "Bob", "Sam", "Kate", "Alice"};

         // Get 2, 3, 4 elements
         var peopleRange = people[1..4];
         Array.ForEach(peopleRange, Console.WriteLine);

         Console.WriteLine();

         // Get all From start index to 4
         peopleRange = people[..4];
         Array.ForEach(peopleRange, Console.WriteLine);

         Console.WriteLine();

         // Get all from 1st index
         peopleRange = people[1..];
         Array.ForEach(peopleRange, Console.WriteLine);

         // два последних - Kate, Alice
         peopleRange = people[^2..];
         Array.ForEach(peopleRange, Console.WriteLine);

         // начиная с предпоследнего - Tom, Bob, Sam, Kate
         peopleRange = people[..^1];
         Array.ForEach(peopleRange, Console.WriteLine);

         // два начиная с предпоследнего - Sam, Kate
         peopleRange = people[^3..^1];
         Array.ForEach(peopleRange, Console.WriteLine);

         // Using Span<T>
         Span<string> peopleSpan = people;
         //var selectedPeopleSpan = peopleSpan[1..4];
         //foreach (var person in selectedPeopleSpan)
         //{
         //   Console.WriteLine(person);
         //}
      }
   }
}
