// Copyright (C) 2011 Oliver Sturm <oliver@oliversturm.com>
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, see <http://www.gnu.org/licenses/>.
  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chapter14 {
  using FCSlib;
  using FCSlib.Data;
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("----- Basic steps");
      int a = 10;
      int b = CalcB(a);
      int c = CalcC(b);
      Console.WriteLine("a={0}, b={1}, c={2}", a, b, c);

      Console.WriteLine("----- Algorithm");
      AlgorithmImplementation( );

      Console.WriteLine("----- Algorithm with internal Lambdas");
      AlgorithmImplementationLambdas( );

      Console.WriteLine("----- SquareList 1");
      SquareList1( );
      Console.WriteLine("----- SquareList 2");
      SquareList2( );
      Console.WriteLine("----- SquareList 3");
      SquareList3( );

      Console.WriteLine("----- SumList 1");
      SumList1( );
      Console.WriteLine("----- SumList 2");
      SumList2( );

      Console.WriteLine("----- CombinedApproaches");
      CombinedApproaches( );
    }

    static int CalcB(int a) {
      return a * 3;
    }

    static int CalcC(int b) {
      return b + 27;
    }


    static void AlgorithmImplementation( ) {
      Func<int, int> calcCFromA = a => CalcC(CalcB(a));
      Console.WriteLine("a={0}, c={1}", 10, calcCFromA(10));

      var calcCFromAauto = Functional.Compose<int, int, int>(CalcB, CalcC);
      Console.WriteLine("a={0}, c={1}", 10, calcCFromAauto(10));
    }

    static void AlgorithmImplementationLambdas( ) {
      Func<int, int> calcB = a => a * 3;
      Func<int, int> calcC = b => b + 27;
      var calcCFromA = Functional.Compose(calcB, calcC);
      // Alternatively:
      var calcCFromA_ = calcB.Compose(calcC);
      Console.WriteLine("a={0}, c={1}", 10, calcCFromA(10));
    }

    private static void SquareList1( ) {
      var curriedMap =
        Functional.Curry<Converter<int, int>, 
          IEnumerable<int>, IEnumerable<int>>(Functional.Map<int, int>);

      var squareList = curriedMap(x => x * x);
      
      var list = new int[] { 2, 3, 4 };
      var squaredList = squareList(list);
      foreach (var item in squaredList)
        Console.Write("{0} ", item);
      Console.WriteLine( );
    }

    private static void SquareList2( ) {
      var curriedMap =
        Functional.Curry(Functional.MapDelegate<int, int>());
      
      var squareList = curriedMap(x => x * x);
      
      var list = new int[] { 2, 3, 4 };
      var squaredList = squareList(list);
      foreach (var item in squaredList)
        Console.Write("{0} ", item);
      Console.WriteLine( );
    }

    private static void SquareList3( ) {
      var squareList = Functional.Apply(Functional.MapDelegate<int, int>( ), x => x * x);
      
      var list = new int[] { 2, 3, 4 };
      var squaredList = squareList(list);
      foreach (var item in squaredList)
        Console.Write("{0} ", item);
      Console.WriteLine( );
    }

    private static void SumList1( ) {
      var curriedFoldL = Functional.Curry(Functional.FoldLDelegate<int, int>());
      var sumList = curriedFoldL((r, v) => r + v)(0);
      
      var list = new int[] { 2, 3, 4 };
      var result = sumList(list);
      Console.WriteLine("Result is {0}", result);
    }

    private static void SumList2( ) {
      var sumList = Functional.Apply(Functional.FoldLDelegate<int, int>( ), (r, v) => r + v, 0);

      var list = new int[] { 2, 3, 4 };
      var result = sumList(list);
      Console.WriteLine("Result is {0}", result);
    }

    private static void CombinedApproaches( ) {
      // This is the task for which a reusable function shall be created:
      //
      //  - from a sequence of Customer objects (given as the first parameter to 
      //    the function), extract the SalesVolume field
      //  - sort descending and restrict to n elements, where n is the second
      //    parameter to the function
      //  - calculate the average of the values

      // The function salesVolumeExtractor receives a sequence of Customers and 
      // returns a sequence of SalesVolumes
      var salesVolumeExtractor = Functional.Apply(Functional.MapDelegate<Customer, decimal>( ), c => c.SalesVolume);

      // The function orderer orders the given sequence of decimal values descending
      Func<IEnumerable<decimal>, IEnumerable<decimal>> orderer =
        l => Enumerable.OrderByDescending(l, v => v);

      // The function avgCalculator receives a sequence of decimal values and calculates 
      // the average over the values
      var avgCalculator = Functional.Compose(Functional.Apply(Functional.FoldLDelegate<decimal, Tuple<decimal, decimal>>( ),
        (r, v) => Tuple.Create(r.Item1 + 1, r.Item2 + v), Tuple.Create(0m, 0m)), t => t.Item2 / t.Item1);

      // At this point, it is possible to use the constructed functions together
      // with a call to Take to calculate the result
      //var avg = avgCalculator(Functional.Take(3, orderer(salesVolumeExtractor(GetCustomers( )))));

      // getOrderedSalesVolumes combines salesVolumeExtractor and orderer to take a sequence
      // of Customers and return a sequence of SalesVolumes, in descending order
      var getOrderedSalesVolumes = Functional.Compose(salesVolumeExtractor, orderer);

      // take is a curried version of Functional.Take, with the parameter order swapped
      var take = Functional.Swap(Functional.Curry(Functional.TakeDelegate<decimal>( )));

      // Composing getOrderedSalesVolumes and take results in the function getRelevantSalesVolumes,
      // which takes a sequence of Customers and returns a function that takes the second 
      // parameter of the take function (the number of elements to take from the sequence).
      // In other words, this is a curried format function which takes two parameters, a sequence
      // of Customers and an int value for the number of elements to restrict to. It's overall
      // result value is a sequence of SalesVolumes, in descending order, with a number of elements
      // at most as large as given in the parameter.
      var getRelevantSalesVolumes = Functional.Compose(getOrderedSalesVolumes, take);

      // Uncurrying the above function results in a non-curried function with two parameters
      var uncurriedGetRelevantSalesVolumes = Functional.Uncurry(getRelevantSalesVolumes);

      // The final composition passes the sequence described above into the average calculator.
      // salesVolumeAverage is the function that was to be created, which takes a sequence of 
      // Customers and an int value, then calculates the average SalesVolume over the highest n 
      // SalesVolumes.
      var salesVolumeAverage = Functional.Compose(uncurriedGetRelevantSalesVolumes, avgCalculator);

      var avg = salesVolumeAverage(GetCustomers( ), 3);

      Console.WriteLine("Average of the highest three sales volumes: {0}", avg);
    }

    private static IEnumerable<Customer> GetCustomers( ) {
      yield return new Customer { Name = "Harry", SalesVolume = 3462.74m };
      yield return new Customer { Name = "Anna", SalesVolume = 112.9m };
      yield return new Customer { Name = "James", SalesVolume = 1269m };
      yield return new Customer { Name = "July", SalesVolume = 634.86m };
      yield return new Customer { Name = "Pete", SalesVolume = 17764.29m };
    }

    internal class Customer {
      public string Name { get; set; }
      public decimal SalesVolume { get; set; }
    }
  }
}
