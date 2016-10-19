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
using FCSlib;

namespace chapter16 {
  public class MyListStep4<T> : System.Collections.Generic.IEnumerable<T> {
    public readonly T Head;
    public readonly MyListStep4<T> Tail;
    public readonly bool IsEmpty;

    public static readonly MyListStep4<T> Empty = new MyListStep4<T>( );

    private MyListStep4( ) {
      IsEmpty = true;
    }

    public MyListStep4(T head, MyListStep4<T> tail) {
      this.Head = head;
      if (tail.IsEmpty)
        this.Tail = MyListStep4<T>.Empty;
      else
        this.Tail = tail;
    }

    public MyListStep4(T head) : this(head, MyListStep4<T>.Empty) { }

    public static MyListStep4<T> Cons(T element, MyListStep4<T> list) {
      if (list.IsEmpty)
        return new MyListStep4<T>(element);
      else
        return new MyListStep4<T>(element, list);
    }

    public MyListStep4<T> Cons(T element) {
      return MyListStep4<T>.Cons(element, this);
    }

    public MyListStep4(T firstValue, params T[] values) {
      Head = firstValue;
      if (values.Length > 0) {
        MyListStep4<T> newtail = MyListStep4<T>.Empty;
        for (int i = values.Length - 1; i >= 0; i--)
          newtail = newtail.Cons(values[i]);
        Tail = newtail;
      }
      else
        Tail = MyListStep4<T>.Empty;
    }

    public MyListStep4(System.Collections.Generic.IEnumerable<T> source) {
      T[] sa = source.ToArray( );
      int sal = sa.Length;
      if (sal > 0) {
        Head = sa[0];
        Tail = MyListStep4<T>.Empty;
        for (int i = sal - 1; i > 0; i--)
          Tail = Tail.Cons(sa[i]);
      }
      else
        IsEmpty = true;
    }

    public System.Collections.Generic.IEnumerator<T> GetEnumerator( ) {
      for (var element = this; element != MyListStep4<T>.Empty; element = element.Tail)
        yield return element.Head;
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator( ) {
      return this.GetEnumerator( );
    }

    #region ToString
    public override string ToString( ) {
      var result = "[";
      if (!IsEmpty)
        result +=
        Functional.FoldL1(
          (r, x) => r + ", " + x,
          Functional.Map(x => x.ToString( ), this));
      result += "]";
      return result;
    }
    #endregion

  }
}
