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
  public class MyListStep2<T> {
    public readonly T Head;
    public readonly MyListStep2<T> Tail;
    public readonly bool IsEmpty;

    public static readonly MyListStep2<T> Empty = new MyListStep2<T>( );

    private MyListStep2( ) {
      IsEmpty = true;
    }

    public MyListStep2(T head, MyListStep2<T> tail) {
      this.Head = head;
      if (tail.IsEmpty)
        this.Tail = MyListStep2<T>.Empty;
      else
        this.Tail = tail;
    }

    public MyListStep2(T head) : this(head, MyListStep2<T>.Empty) { }

    public static MyListStep2<T> Cons(T element, MyListStep2<T> list) {
      if (list.IsEmpty)
        return new MyListStep2<T>(element);
      else
        return new MyListStep2<T>(element, list);
    }

    public MyListStep2<T> Cons(T element) {
      return MyListStep2<T>.Cons(element, this);
    }

  }
}
