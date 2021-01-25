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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace chapter5 {
  static class Program {
    static void Main(string[] args) {
      // Without interfaces
      //var list = new EndlessListWithoutInterfaces( );
      //foreach (var item in list) 
      //  Console.WriteLine(item);

      // With interfaces
      //var list = new EndlessListWithInterfaces( );
      //foreach (var item in list)
      //  Console.WriteLine(item);

      // With a separate enumerator
      //var list = new EndlessList( );
      //foreach (var item in list)
      //  Console.WriteLine(item);
    
      // Using an iterator function
      //var list = EndlessListFunction( );
      //foreach (var item in list)
      //  Console.WriteLine(item);

      // With debug output
      Console.WriteLine("Retrieving the list object");
      var list = ThreeNumbersDebug( );
      Console.WriteLine("Before the foreach loop");
      foreach (var item in list)
        Console.WriteLine("Got value {0}", item);

      // Take the first five elements
      var fiveElementList = Take(5, EndlessListFunction( ));
      foreach (var item in fiveElementList)
        Console.WriteLine(item);

      // Take elements from EndlessListFunction, apply a square
      // operation, divide by 2, then cut off the sequence after 
      // 10 elements.
      var results = Take(10,
        EndlessListFunction( ).
        Apply(Square).
        Apply(x => x / 2));
      foreach (var item in results)
        Console.WriteLine(item);
    }

    public static IEnumerable<int> EndlessListFunction( ) {
      int val = 0;
      while (true)
        yield return val++;
    }

    public static IEnumerable<int> ThreeNumbers( ) {
      yield return 3;
      yield return 11;
      yield return 27;
    }

    public static IEnumerable<int> ThreeNumbersDebug( ) {
      Console.WriteLine("Returning 3");
      yield return 3;
      Console.WriteLine("Returning 11");
      yield return 11;
      Console.WriteLine("Returning 27");
      yield return 27;
    }

    public static IEnumerable<int> Take(int count, IEnumerable<int> source) {
      int used = 0;
      foreach (var item in source)
        if (count > used++)
          yield return item;
        else
          yield break;
    }

    public static int Square(int x) {
      return x * x;      
    }

    public static IEnumerable<int> Square(IEnumerable<int> values) {
      foreach (int val in values)
        yield return Square(val);
    }

    public static IEnumerable<int> Apply(IEnumerable<int> values, Func<int, int> calculation) {
      foreach (int val in values)
        yield return calculation(val);
    }

    public static IEnumerable<T> Apply<T>(this IEnumerable<T> values, Func<T, T> calculation) {
      foreach (T val in values)
        yield return calculation(val);
    }
  }

  public class EndlessListWithoutInterfaces {
    public EndlessListWithoutInterfaces GetEnumerator( ) {
      return this;
    }

    public bool MoveNext( ) {
      return true;
    }

    public object Current {
      get { return "something"; }
    }
  }

  public class EndlessListWithInterfaces: IEnumerable, IEnumerator {
    public EndlessListWithInterfaces( ) {
    }

    public IEnumerator GetEnumerator( ) {
      return this;
    }

    public object Current {
      get { return "something"; }
    }

    public bool MoveNext( ) {
      return true;
    }

    public void Reset( ) {
    }
  }

  public class EndlessList : IEnumerable {
    public class Enumerator : IEnumerator {
      int val = -1;

      public object Current {
        get { return val; }
      }

      public bool MoveNext( ) {
        val++;
        return true;
      }

      public void Reset( ) {
        val = -1;
      }
    }

    public IEnumerator GetEnumerator( ) {
      return new Enumerator( );
    }
  }

  public class EndlessListWithIterators : IEnumerable<int> {
    IEnumerator<int> IEnumerable<int>.GetEnumerator( ) {
      int val = 0;
      while (true)
        yield return val++;
    }

    IEnumerator IEnumerable.GetEnumerator( ) {
      return ((IEnumerable<int>) this).GetEnumerator( );
    }
  }
}
