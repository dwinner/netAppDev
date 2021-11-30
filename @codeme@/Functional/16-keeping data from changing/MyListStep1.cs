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
using NUnit.Framework;
using FCSlib.Data;

namespace chapter16 {
  public class MyListStep1<T> {
    public readonly T Head;
    public readonly MyListStep1<T> Tail;
    public readonly bool IsEmpty;

    public static readonly MyListStep1<T> Empty = new MyListStep1<T>( );

    private MyListStep1( ) {
      IsEmpty = true;
    }

    public MyListStep1(T head, MyListStep1<T> tail) {
      this.Head = head;
      if (tail.IsEmpty)
        this.Tail = MyListStep1<T>.Empty;
      else
        this.Tail = tail;
    }

    public MyListStep1(T head) : this(head, MyListStep1<T>.Empty) { }
  }
}
