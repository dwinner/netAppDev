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
using FCSlib.Data;

namespace chapter15 {
  class Program {
    static void Main(string[] args) {
      // Instantiate some options:
      var intOpt1 = new Option<int>(10);
      var intOpt2 = 10.ToOption( );
      var stringOpt1 = Option.Some("text");
      var stringOpt2 = "text".ToOption( );
      var noneOpt = Option.None;

      // Check for equality:
      Console.WriteLine("intOpt1 == intOpt2: " + (intOpt1 == intOpt2)); // True
      Console.WriteLine("stringOpt1 == stringOpt2: " + (stringOpt1 == stringOpt2)); // True
      Console.WriteLine("intOpt1 == noneOpt: " + (intOpt1 == noneOpt)); // False
      Console.WriteLine("stringOpt1 == noneOpt: " + (stringOpt2 == noneOpt)); // False

      // A null value can be a valid value:
      string nullString = null;
      var nullStringOpt1 = nullString.ToOption( );
      Console.WriteLine("nullStringOpt1 == noneOpt: " + (nullStringOpt1 == noneOpt)); // False

      // Or it can be considered equal to None:
      var nullStringOpt2 = nullString.ToNotNullOption( );
      Console.WriteLine("nullStringOpt2 == noneOpt: " + (nullStringOpt2 == noneOpt)); // True

    }
  }
}
