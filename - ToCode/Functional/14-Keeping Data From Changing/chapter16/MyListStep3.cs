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
  public class MyListStep3<T> {
    public readonly T Head;
    public readonly MyListStep3<T> Tail;
    public readonly bool IsEmpty;

    public static readonly MyListStep3<T> Empty = new MyListStep3<T>( );

    private MyListStep3( ) {
      IsEmpty = true;
    }

    public MyListStep3(T head, MyListStep3<T> tail) {
      this.Head = head;
      if (tail.IsEmpty)
        this.Tail = MyListStep3<T>.Empty;
      else
        this.Tail = tail;
    }

    public MyListStep3(T head) : this(head, MyListStep3<T>.Empty) { }

    public static MyListStep3<T> Cons(T element, MyListStep3<T> list) {
      if (list.IsEmpty)
        return new MyListStep3<T>(element);
      else
        return new MyListStep3<T>(element, list);
    }

    public MyListStep3<T> Cons(T element) {
      return MyListStep3<T>.Cons(element, this);
    }

    public MyListStep3(T firstValue, params T[] values) {
      Head = firstValue;
      if (values.Length > 0) {
        MyListStep3<T> newtail = MyListStep3<T>.Empty;
        for (int i = values.Length - 1; i >= 0; i--)
          newtail = newtail.Cons(values[i]);
        Tail = newtail;
      }
      else
        Tail = MyListStep3<T>.Empty;
    }

    public MyListStep3(System.Collections.Generic.IEnumerable<T> source) {
      T[] sa = source.ToArray( );
      int sal = sa.Length;
      if (sal > 0) {
        Head = sa[0];
        Tail = MyListStep3<T>.Empty;
        for (int i = sal - 1; i > 0; i--)
          Tail = Tail.Cons(sa[i]);
      }
      else
        IsEmpty = true;
    }
  }

}
