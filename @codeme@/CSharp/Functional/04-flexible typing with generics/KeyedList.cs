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

namespace chapter4 {
  public class KeyedList<K, V>
    where K : IComparable
    where V : class {
    // ...    

    public void Add(K key, V value) {
    }

    public void Add<KA, VA>(KeyedList<KA, VA> addList)
      where KA : K
      where VA : class, V {
      // adding the content of addList to this list
      // is now completely type safe
    }

    // ...    
  }
}
