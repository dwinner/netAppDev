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
using FCSlib;

namespace PreferHigherOrderFunctions {
  class Program {
    static void Main(string[] args) {
      var ints = new int[] { 1, 2, 8, 93 };
      SumIntsImperative(ints);
      SumIntsFunctional(ints);

      // Square some values imperatively
      var squaredInts1 = new int[ints.Length];
      for (int i = 0; i < ints.Length; i++)
        squaredInts1[i] = ints[i] * ints[i];

      // Square some values functionally
      var squaredInts2 = 
        Functional.Map(x => x * x, ints).ToList( );

      // Or of course with LINQ - also functionally
      var squaredInts3 =
        (from x in ints
         select x * x).ToList( );
    }

    private static void SumIntsImperative(IEnumerable<int> ints) {
      int result = 0;
      foreach (int v in ints)
        result += v;
      Console.WriteLine(result);
    }

    private static void SumIntsFunctional(IEnumerable<int> ints) {
      var result = Functional.FoldL((s, v) => s + v, 0, ints);
      Console.WriteLine(result);
    }

  }
}
