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

namespace chapter6 {
  class Program {
    static void Main(string[] args) {
      ReturningFunctions( );

      Closures( );

      DynamicAdd( );
    }

    private static void ReturningFunctions( ) {
      var add = GetAdd( );
      Console.WriteLine("Adding 10 and 20: {0}", add(10, 20));
    }

    static Func<int, int, int> GetAdd( ) {
      return (x, y) => x + y;
    }

    static void Closures( ) {
      Console.WriteLine(GetClosureFunction( )(30));
    }

    static Func<int, int> GetClosureFunction( ) {
      int val = 10;
      Func<int, int> internalAdd = x => x + val;

      Console.WriteLine(internalAdd(10));

      val = 30;
      Console.WriteLine(internalAdd(10));

      return internalAdd;
    }

    private static void DynamicAdd( ) {
      var add5 = GetAddX(5);
      var add10 = GetAddX(10);

      Console.WriteLine(add5(10));
      Console.WriteLine(add10(10));
    }

    private static Func<int, int> GetAddX(int staticVal) {
      return x => staticVal + x;
    }

  }
}
