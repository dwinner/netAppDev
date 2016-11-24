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
using System.Diagnostics;
using FCSlib;

namespace chapter8 {
  class Program {
    static void Main(string[] args) {
      CalculatorCalls( );

      CurryingActions( );

      LambdaFunction( );
    }

    private static void CalculatorCalls( ) {
      // Calculator
      // The results of these two calls are the same
      Console.WriteLine(Calculator.AddC(20)(30));
      Console.WriteLine(Calculator.Add(20, 30));

      // Calculator2
      // The results of these two calls are the same
      Console.WriteLine(Calculator2.AddC(20)(30));
      Console.WriteLine(Calculator2.Add(20, 30));

      // Calculator3
      Console.WriteLine(Calculator3.Add(20)(30));

      // Calculator4
      Console.WriteLine(Calculator4.Add(20)(30));
    }

    public class Assert {
      public static void Equals(int x, int y) {
        Debug.Assert(x == y);
      }
    }

    private static void CurryingActions( ) {
      var curriedAssertEquals = Functional.Curry<int, int>(Assert.Equals);
      var assertEquals5 = curriedAssertEquals(5);
    }

    private static void LambdaFunction( ) {
      // In simple cases, the Functional.Lambda function doesn't gain anything.
      // In fact, the "normal" syntax is shorter.
      var addL = Functional.Lambda<int, int, int>((x, y) => x + y);
      Func<int, int, int> add = (x, y) => x + y;

      // With functions in curried format, however, the syntax can be simpler
      // because it flattens the type parameters:
      var mult4L = Functional.Lambda<int, int, int, int, int>(a => b => c => d => a * b * c * d);
      Func<int, Func<int, Func<int, Func<int, int>>>> mult4 = a => b => c => d => a * b * c * d;
    }
  }
}
