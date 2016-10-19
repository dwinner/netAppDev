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
using System.Linq;
using System.Text;
using FCSlib;
using FCSlib.Data;
using System.Collections.Generic;

using FCSColl = FCSlib.Data.Collections;

namespace chapter12 {
  class Program {
    static void Main(string[] args) {
      var people = new List<Person> {
        new Person {Name = "Harry", Age = 32},
        new Person {Name = "Anna", Age = 45},
        new Person {Name = "Willy", Age = 43},
        new Person {Name = "Rose", Age = 37}
      };

      // extract names of people
      var names = Functional.Map(p => p.Name, people);

      // calculate square numbers
      var squares = Functional.Map(i => i * i, Enumerable.Range(1, 10));

      // find those people who are over 40
      var over40 = Functional.Filter(p => p.Age >= 40, people);

      // sum up the values from 1 to 10
      var sumOf1to10 = Functional.FoldL((r, v) => r + v, 0, Enumerable.Range(1, 10));

      // multiply the values from 1 to 10
      var productOf1to10 = Functional.FoldL((r, v) => r * v, 0, Enumerable.Range(1, 10));

      // calculate the average age of the people
      var acc = Functional.FoldL((r, v) => Tuple.Create(r.Item1 + v.Age, r.Item2 + 1), Tuple.Create(0, 0), people);
      var averageAge = (double) acc.Item1 / (double) acc.Item2;

      // recreate the list that is passed in
      var cloneList = Functional.FoldL((r, v) => r.Cons(v), FCSColl::List<int>.Empty, Enumerable.Range(1, 5));

      // test the implementations based on fold:
      var squares2 = FoldMap(v => v * v, Enumerable.Range(1, 5));
      foreach (var item in squares2) {
        Console.WriteLine(item);
      }

      var filteredSquares = FoldFilter(v => v > 10, squares2);
      foreach (var item in filteredSquares) {
        Console.WriteLine(item);
      }

      // some structured (relational) test data
      var rubberChicken = new Product { Name = "Rubber chicken", Price = 3.99m };
      var pulley = new Product { Name = "Pulley", Price = 0.99m };
      var bread = new Product { Name = "Bread", Price = 0.89m };
      var butter = new Product { Name = "Butter", Price = 1.19m };

      var orders = new List<Order> {
        new Order{ Date = DateTime.Now.Date,
          Lines = new List<OrderLine>{
            new OrderLine{Product = rubberChicken, Count=3},
            new OrderLine{Product = pulley, Count = 3}
          }
        },
        new Order{ Date = DateTime.Now.Date.Subtract(new TimeSpan(1,0,0,0)),
          Lines = new List<OrderLine>{
            new OrderLine{Product = bread, Count=1},
            new OrderLine{Product = butter, Count = 1}
          }
        }
      };

      // select those orders with a total value > 5.00
      var ordersWithValueGreater5 =
        Functional.Filter(v => v.Value > 5.00m,
          Functional.Map(o => new {
            Date = o.Date,
            Value = Functional.FoldL(
              (r, v) => r + v.Product.Price * v.Count,
              0m, o.Lines)
          }, orders));

      foreach (var item in ordersWithValueGreater5) {
        Console.WriteLine(item);
      }

      var ordersWithValueGreater5Linq1 =
        orders.Select(o => new {
          Date = o.Date,
          Value = o.Lines.Sum(ol => ol.Product.Price * ol.Count)
        }).Where(t => t.Value > 5.00m);

      var ordersWithValueGreater5Linq2 =
        from t in
          from o in orders
          select new {
            Date = o.Date,
            Value = o.Lines.Sum(ol => ol.Product.Price * ol.Count)
          }
        where t.Value > 5.00m
        select t;

      Console.WriteLine("Fold Left Recursive ==========================");
      var onetothree = new FCSColl::List<int>(Enumerable.Range(1, 3));
      Func<int, int, int> accD = (r, x) => {
        int sum = r + x;
        Console.WriteLine("accD({0}, {1}) ... returning {2}", r, x, sum);
        return sum;
      };
      var resultL = FoldLRecD(accD, 0, onetothree);
      Console.WriteLine(resultL);

      Console.WriteLine("Fold Right Recursive ==========================");

      Func<int, int, int> accRD = (x, r) => {
        int sum = r + x;
        Console.WriteLine("accRD({0}, {1}) ... returning {2}", r, x, sum);
        return sum;
      };
      var resultR = FoldRRecD(accRD, 0, onetothree);
      Console.WriteLine(resultR);
    }

    public static R FoldLRec<T, R>(Func<R, T, R> acc, R start, FCSColl::List<T> list) {
      if (list == FCSColl::List<T>.Empty)
        return start;
      return FoldLRec<T, R>(acc, acc(start, list.Head), list.Tail);
    }

    public static R FoldRRec<T, R>(Func<T, R, R> acc, R start, FCSColl::List<T> list) {
      if (list == FCSColl::List<T>.Empty)
        return start;
      return acc(list.Head, FoldRRec(acc, start, list.Tail));
    }

    public static R FoldLRecD<T, R>(Func<R, T, R> acc, R start, FCSColl::List<T> list) {
      Console.Write("flr(acc, {0}, {1})", start, list);
      if (list == FCSColl::List<T>.Empty) {
        Console.WriteLine(" ... returning {0}", start);
        return start;
      }
      else
        Console.WriteLine();
      return FoldLRecD<T, R>(acc, acc(start, list.Head), list.Tail);
    }

    public static R FoldRRecD<T, R>(Func<T, R, R> acc, R start, FCSColl::List<T> list) {
      Console.Write("frr(acc, {0}, {1})", start, list);
      if (list == FCSColl::List<T>.Empty) {
        Console.WriteLine(" ... returning {0}", start);
        return start;
      }
      else
        Console.WriteLine( );
      return acc(list.Head, FoldRRecD(acc, start, list.Tail));
    }

    // Just for fun: 
    // An implementation of Map on the basis of Fold:
    public static IEnumerable<R> FoldMap<T, R>(Converter<T, R> converter, IEnumerable<T> source) {
      return Functional.FoldL((r, v) =>
        r.Append(new FCSColl::List<R>(converter(v))), FCSColl::List<R>.Empty, source);
    }

    // And a corresponding implementation of Filter:
    public static IEnumerable<T> FoldFilter<T>(Predicate<T> predicate, IEnumerable<T> source) {
      return Functional.FoldL(
        (r, v) => r.Append(predicate(v) ? new FCSColl::List<T>(v) : FCSColl::List<T>.Empty),
        FCSColl::List<T>.Empty, source);
    }

  }

  public class Person {
    public string Name { get; set; }
    public int Age { get; set; }
  }

  public class Order {
    public DateTime Date { get; set; }
    public List<OrderLine> Lines { get; set; }
  }

  public class OrderLine {
    public Product Product {get; set;}
    public int Count {get; set;}
  }

  public class Product {
    public string Name { get; set; }
    public decimal Price { get; set; }
  }
}
