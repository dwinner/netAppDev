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

namespace chapter9 {
  using FCSlib;
  
  class Program {
    static void Main(string[] args) {
      PassingFunctions( );

      ExplicitLaziness( );

      PassingFunctionsWithReallyBadPerformance( );
    }

    private static void PassingFunctions( ) {
      Console.WriteLine("==== Standard DoSomething");
      DoSomething(0, BigCalculation( ));
      Console.WriteLine("\n==== HigherOrderDoSomething");
      HigherOrderDoSomething(( ) => 0, BigCalculation);
    }

    private static void DoSomething(int a, int b) {
      Console.WriteLine("DoSomething called");

      if (a != 0) {
        // This statement symbolizes "doing something" with b
        Console.WriteLine(b);
      }
    }

    private static int BigCalculation( ) {
      Console.WriteLine("BigCalculation called");

      // This is the type of function that goes away for a long
      // time to perform a great big calculation and then 
      // comes back and returns 42.

      return 42;
    }

    private static void HigherOrderDoSomething(Func<int> a, Func<int> b) {
      Console.WriteLine("HigherOrderDoSomething called");

      if (a() != 0) {
        // This statement symbolizes "doing something" with b
        Console.WriteLine(b());
      }
    }

    private static void ExplicitLaziness( ) {
      Console.WriteLine("===== LazyDoSomething");
      LazyDoSomething(new Lazy<int>(0), new Lazy<int>(BigCalculation));
      // Alternative syntax
      LazyDoSomething(Functional.Lazy(0), Functional.Lazy<int>(BigCalculation));

      Console.WriteLine("===== LazyAdd");
      Func<int, Func<int, Lazy<int>>> lazyAdd = 
        x => y => Functional.Lazy(() => x + y);

      Console.WriteLine("      Getting result");
      var result = LazyAdd(37, 5);
      Console.WriteLine("     Accessing the result");
      Console.WriteLine(result.Value);
      Console.WriteLine("     Accessing the result a second time");
      Console.WriteLine(result.Value);
    }

    private static void LazyDoSomething(Lazy<int> a, Lazy<int> b) {
      Console.WriteLine("LazyDoSomething called");

      if (a.Value != 0) {
        // This statement symbolizes "doing something" with b
        Console.WriteLine(b.Value);
      }
    }

    private static Lazy<int> LazyAdd(int x, int y) {
      Console.WriteLine("Calling LazyAdd");
      return Functional.Lazy(( ) => { 
        Console.WriteLine("Performing calculation"); 
        return x + y; 
      });
    }

    private static void PassingFunctionsWithReallyBadPerformance( ) {
      Console.WriteLine("===== Doing something really bad");
      DoSomethingBad(( ) => true, BigCalculation);
    }

    private static void DoSomethingBad(Func<bool> squareFirst, Func<int> bigCalculation) {
      Console.WriteLine("Result: {0}",
        squareFirst() ? bigCalculation( ) * bigCalculation( ) : bigCalculation( ));
    }
  }
}
