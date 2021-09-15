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

namespace chapter13 {
  using FCSlib;
  using FCSlib.Data;
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("-------- Sequences");
      Console.WriteLine("------------------------- Odd Numbers");
      var oddNumbersFrom1To19 =
        Functional.Sequence(x => x + 2, 1, x => x >= 19);
      foreach (var item in oddNumbersFrom1To19)
        Console.WriteLine(item);

      Console.WriteLine("------------------------- Squares");
      var squares =
        Functional.Sequence(x => x * x, 2, x => x >= 10000);
      foreach (var item in squares)
        Console.WriteLine(item);

      Console.WriteLine("------------------------- Fibonacci");
      var fibonacci =
        Functional.Sequence(
          x => x.Item2 == 0 ? Tuple.Create(0, 1) :
            Tuple.Create(x.Item2, x.Item1 + x.Item2),
          Tuple.Create(0, 0), null).Select(t => t.Item2).Take(20);
      foreach (var item in fibonacci)
        Console.WriteLine(item);

      Console.WriteLine("-------- Ranges");
      Console.WriteLine("------------------------- 1 to 10");
      var oneToTen = Range.Create(1, 10);
      foreach (var item in oneToTen)
        Console.WriteLine(item);

      Console.WriteLine("------------------------- Odd Numbers");
      var oddNumbersFrom1To19Range =
        Range.Create(1, 19, x => x + 2);
      foreach (var item in oddNumbersFrom1To19Range)
        Console.WriteLine(item);

      Console.WriteLine("------------------------- Squares");
      var squaresRange =
        Range.Create(2, 10000, x => x * x);
      foreach (var item in squaresRange)
        Console.WriteLine(item);

      Console.WriteLine("------------------------- Fibonacci");
      var fibonacciRange =
        Range.Create(Tuple.Create(0, 0), Tuple.Create(0, 5000),
          x => x.Item2 == 0 ? Tuple.Create(0, 1) :
            Tuple.Create(x.Item2, x.Item1 + x.Item2),
            (one, other) => one.Item2 - other.Item2);

      foreach (var item in Functional.Map(t => t.Item2, fibonacciRange))
        Console.WriteLine(item);

      Console.WriteLine("------------------------- In range checks");
      if (oneToTen.Contains(5))
        Console.WriteLine("oneToTen contains 5");
      if (oddNumbersFrom1To19Range.Contains(10))
        Console.WriteLine("oddNumbersFrom1To19Range contains 10");

      Console.WriteLine("-------- Loops");
      Console.WriteLine("------------------------- Range loop");
      Range.Create(1, 3).Each(x => Console.WriteLine(x));

      Console.WriteLine("------------------------- for loop");
      for (int i = 1; i <= 3; i++)
        Console.WriteLine(i);

      Console.WriteLine("-------- Fibonacci Explicit");
      foreach (var item in FibonacciExplicit(5000)) {
        Console.WriteLine(item);
      }

      Console.WriteLine("-------- Fibonacci Explicit with TakeWhile");
      foreach (var item in Functional.TakeWhile(x => x <= 5000, FibonacciExplicit( ))) {
        Console.WriteLine(item);
      }
    }

    static IEnumerable<int> FibonacciExplicit(int max) {
      int first = 0;
      int second = 1;
      do
      {
        yield return first;
        int temp = first;
        first = second;
        second += temp;
      } while (max >= first);
    }

    static IEnumerable<int> FibonacciExplicit( ) {
      int first = 0;
      int second = 1;
      while (true) {
        yield return first;
        int temp = first;
        first = second;
        second += temp;
      }
    }
  }

}
