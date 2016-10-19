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

namespace chapter17 {
  public class Maybe<T> {
    public static readonly Maybe<T> Empty = new Maybe<T>( );
    private readonly T val;
    public T Value { get { return val; } }
    private readonly bool isEmpty;
    public bool IsEmpty { get { return isEmpty; } }
    public bool HasValue { get { return !isEmpty; } }

    public Maybe(T val) {
      this.val = val;
    }

    private Maybe( ) {
      this.isEmpty = true;
    }

    public Maybe<R> Bind<R>(Func<T, Maybe<R>> g) {
      return IsEmpty ? Maybe<R>.Empty : g(Value);
    }
  }

}
