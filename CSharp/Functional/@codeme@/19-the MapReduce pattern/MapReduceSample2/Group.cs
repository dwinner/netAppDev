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
using FCSlib.Data;

namespace MapReduceSample2 {
  public class Group<TKey, TValue> : Tuple<TKey, List<TValue>> {
    public Group(TKey key) : base(key, new List<TValue>( )) {
    }

    public TKey Key {
      get {
        return base.Item1;
      }
    }

    public List<TValue> Values {
      get {
        return base.Item2;
      }
    }
  }
}
