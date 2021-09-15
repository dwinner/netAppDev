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

namespace ManglerDemo {
  using Compiled;

  public class ManglerTesterCompiled {
    public static void ShowIt( ) {
      int val = 42;

      // result will be 42*42 here
      int result = val.Mangle(x => x * x);
      Console.WriteLine(result);
    }
  }
}

namespace ManglerDemo {
  using Expressions;

  public class ManglerTesterExpressions {
    public static void ShowIt( ) {
      int val = 42;

      // result will be 42*42 + 23 here - but 
      // my own code is exactly the same as before!!
      int result = val.Mangle(x => x * x);
      Console.WriteLine(result);
    }
  }
}
