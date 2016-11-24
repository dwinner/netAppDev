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
  public static class MaybeHelpers {
    public static Maybe<T> ToMaybe<T>(this T val) {
      return new Maybe<T>(val);
    }

    public static Maybe<T> ToNotNullMaybe<T>(this T val) where T : class {
      return val != null ? val.ToMaybe( ) : Maybe<T>.Empty;
    }

    public static Maybe<R> SelectMany<T, R>(this Maybe<T> m, Func<T, Maybe<R>> g) {
      return m.Bind(g);
    }

    public static Maybe<R> SelectMany<T, TCollection, R>(
          this Maybe<T> source,
          Func<T, Maybe<TCollection>> collectionSelector,
          Func<T, TCollection, R> resultSelector) {
      if (source.IsEmpty)
        return Maybe<R>.Empty;
      var x = source.Bind(collectionSelector);
      if (x.IsEmpty)
        return Maybe<R>.Empty;
      var r = resultSelector(source.Value, x.Value);
      return r.ToMaybe( );
    }
  }
}
