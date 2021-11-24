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
using System.Linq.Expressions;

namespace Expressions {
  public static class IntExtensionsExpressions {
    public static int Mangle(this int value, Expression<Func<int, int>> mangler) {
      // let's mangle the mangler before we do anything with it
      // -- just because we can
      Func<int, int> mangledMangler =
        Expression.Lambda<Func<int, int>>(
          Expression.Add(mangler.Body, Expression.Constant(23)),
          mangler.Parameters).Compile( );
      return mangledMangler(value);
    }
  }
}
