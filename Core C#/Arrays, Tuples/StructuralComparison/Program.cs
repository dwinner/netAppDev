/**
 * Структурное сравнение
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace StructuralComparison
{
   static class Program
   {
      static void Main()
      {
         // Note 1) Используем EqualityComparer<T>.Default
         var janet = new Person { FirstName = "Janet", LastName = "Jackson" };
         Person[] persons1 = { new Person { FirstName = "Michael", LastName = "Jackson" }, janet };
         Person[] persons2 = { new Person { FirstName = "Michael", LastName = "Jackson" }, janet };
         if (persons1 != persons2)  // true
         {
            Console.WriteLine("Not the same reference");
         }

         if (!persons1.Equals(persons2))  // true
         {
            Console.WriteLine("Equals returns false - not the same reference");
         }

         /*
          * Note: Структурное сравнение использует метод bool Equals(Person other) для своих целей и даcт true,
          * в противном случае IStructuralEquatable.Equals использует object.Equals
          */
         if ((persons1 as IStructuralEquatable).Equals(persons2, EqualityComparer<Person>.Default))   // true
         {
            Console.WriteLine("The same content");
         }

         // Note: 2) Задаем свой EqualityComparer
         var firstTuple = Tuple.Create(1, "Stephanie");
         var secondTuple = Tuple.Create(1, "Stephanie");
         if (!Equals(firstTuple, secondTuple))
            Console.WriteLine("not the same reference to the tuple");

         if (firstTuple.Equals(secondTuple))
            Console.WriteLine("equals returns true");

         var tupleComparer = new TupleComparer();

         if ((firstTuple as IStructuralEquatable).Equals(secondTuple, tupleComparer))
         {
            Console.WriteLine("yes, using TubpleComparer");
         }
      }
   }
}
