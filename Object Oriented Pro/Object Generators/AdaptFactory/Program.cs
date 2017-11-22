/**
 * Adapted factory method for transfering Comparison To Comparer
 */

using System;
using System.Collections.Generic;
using static AdaptFactory.ComparerFactory;

namespace AdaptFactory
{
   internal static class Program
   {
      private static void Main()
      {
         ISet<Employee> set = new SortedSet<Employee>(Create<Employee>((x, y) => x.Id.CompareTo(y.Id)));
         set.Add(new Employee {Id = 1, Name = "Den"});
         foreach (var employee in set)
            Console.WriteLine(employee);
      }
   }
}