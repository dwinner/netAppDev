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
using System.Collections;
using System.IO;
using CompanyWideTools;

namespace chapter3 {
  class Program {
    static void Main(string[] args) {
      var sourceList = new int[] { 7, 6, 9, 8, 5, 10, 273, 15, 11 };
      Bubblesorter.Sort(sourceList);
      foreach (int item in sourceList)
        Console.Write("{0} ", item);
      Console.WriteLine( );

      var sourceObjects = new object[] { 7, 6, 9, 8, 5, 10, 273, 15, 11 };
      Bubblesorter.Sort(sourceObjects, IsAGreaterThanB_Int);
      foreach (object item in sourceObjects)
        Console.Write("{0} ", item);
      Console.WriteLine( );

      AnonymousMethods( );

      DeclaringLambdas( );

      ExtensionMethods( );

    }

    static bool IsAGreaterThanB_Int(object a, object b) {
      return ((int) a) > ((int) b);
    }

    static bool IsAGreaterThanB_String(object a, object b) {
      return ((string) a)[0] > ((string) b)[0];
    }

    static bool IsAGreaterThanB_Customer(object a, object b) {
      return IsAGreaterThanB_String(((Customer) a).Name, ((Customer) b).Name);
    }

    static void AnonymousMethods( ) {
      Bubblesorter.IsAGreaterThanBDelegate compareInt =
        delegate(object a, object b) {
          return ((int) a) > ((int) b);
        };

      Bubblesorter.IsAGreaterThanBDelegate compareInt2 =
        (object a, object b) => ((int) a) > ((int) b);

      Bubblesorter.IsAGreaterThanBDelegate compareInt3 =
        (a, b) => ((int) a) > ((int) b);

      Func<object, object, bool> compareInt4 =
        (a, b) => ((int) a) > ((int) b);
    }

    private static void DeclaringLambdas( ) {
      // This doesn't work
      //Func<int, int, int> add = (x, y) => x + y;
      //TakeMethod(add);

      // This doesn't work
      //var add = (x, y) => x + y;

      // This doesn't work either
      //var add = (int x, int y) => x + y;

    }

    delegate int IntIntIntDelegate(int x, int y);

    private static void TakeMethod(IntIntIntDelegate del) { 
    }

    public static void ExtensionMethods( ) {
      string[] strings = new[] {
        "to", "be", "or", "not", "to", "be"
      };

      Console.WriteLine(StringHelpers.Concat(strings, " "));
      Console.WriteLine(strings.Concat(" "));

      "String".PrintType( );
      var intVal = 42;
      intVal.PrintType( );

      Console.WriteLine(52.Square( ));
      int[] ints = new[] { 10, 20, 30, 40, 50 };
      Console.WriteLine(ints.SecondElement( ));
    }
  }

  public class Customer {
    public string Name { get; set; }
  }

  class Overloads {
    int Add(int x, int y) {
      return x + y;
    }

    int Add(int x, int y, int z) {
      return Add(x, y) + z;
    }

    double Add(double x, double y) {
      return x + y;
    }

    double Add(double x, double y, double z) {
      return Add(x, y) + z;
    }
  }

  public static class Bubblesorter {
    public static void Sort(int[] values) {
      bool swapped;

      do
      {
        swapped = false;
        // iterate over the whole list
        for (int i = 0; i < values.Length - 1; i++) {
          // if the current index value is greater than the one that follows it...
          if (values[i] > values[i + 1]) {
            // then the two are swapped
            int temp = values[i];
            values[i] = values[i + 1];
            values[i + 1] = temp;
            // remember that we did some swapping
            swapped = true;
          }
        }
        // and restart, as long as there was any swapping done on the last run
        // The whole thing ends when a run is completed without finding anything
        // that needs to be swapped.
      } while (swapped);
    }

    public delegate bool IsAGreaterThanBDelegate(object a, object b);

    public static void Sort(object[] values, IsAGreaterThanBDelegate isAGreaterThanB) {
      bool swapped;

      do {
        swapped = false;
        for (int i = 0; i < values.Length - 1; i++) {
          if (isAGreaterThanB(values[i], values[i + 1])) {
            object temp = values[i];
            values[i] = values[i + 1];
            values[i + 1] = temp;
            swapped = true;
          }
        }
      } while (swapped);
    }
  }
}

namespace CompanyWideTools {
  public static class StringHelpers {
    public static string Concat(this string[] strings, string separator) {
      bool first = true;
      var builder = new StringBuilder( );
      foreach (var s in strings) {
        if (!first)
          builder.Append(separator);
        else
          first = false;
        builder.Append(s);
      }

      return builder.ToString( );
    }
  }

  public static class OtherHelpers {
    public static void PrintType(this object thing) {
      Console.WriteLine(thing.GetType( ).FullName);
    }

    public static int Square(this int x) {
      return x * x;
    }

    public static T SecondElement<T>(this IList<T> collection) {
      return collection[1];
    }
  }

}
