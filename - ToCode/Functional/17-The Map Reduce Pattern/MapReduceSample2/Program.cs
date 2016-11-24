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
using FCSlib.Data;
using FCSlib;

namespace MapReduceSample2 {
  class Program {
    static void Main(string[] args) {
      var orders = InitOrders( );

      var orderValues = MapReduce(
        o => Functional.Map(ol => Tuple.Create(o.Name, ol.ProductPrice * ol.Count), o.Lines),
        (r, t) => r + t.Item2, 0m, 
        orders);

      foreach (var result in orderValues)
        Console.WriteLine("Order: {0}, Value: {1}", result.Item1, result.Item2);

      var orderLineCounts = MapReduce(
        o => Functional.Map(ol => Tuple.Create(o.Name, 1), o.Lines),
        (r, t) => r + 1, 0,
        orders);

      foreach (var result in orderLineCounts)
        Console.WriteLine("Order: {0}, Lines: {1}", result.Item1, result.Item2);
    }

    static List<Order> InitOrders( ) {
      return new List<Order> {
        new Order {
          Name = "Customer 1 Order",
          Lines = new List<OrderLine> {
            new OrderLine{ ProductName = "Rubber Chicken", ProductPrice=8.95m, Count=5},
            new OrderLine{ ProductName = "Pulley", ProductPrice=0.99m, Count=5 }
          }
        },
        new Order {
          Name = "Customer 2 Order",
          Lines = new List<OrderLine> {
            new OrderLine{ ProductName = "Canister of Grog", ProductPrice=13.99m, Count=10}
          }
        }
      };
    }

    #region MapReduce and Group functions
    static IEnumerable<Tuple<TKey, TReduceResult>> MapReduce<TKey, TValue, TReduceInput, TReduceResult>(
      Converter<TValue, IEnumerable<Tuple<TKey, TReduceInput>>> mapStep,
      Func<TReduceResult, Tuple<TKey, TReduceInput>, TReduceResult> reduceStep,
      TReduceResult reduceStartVal,
      IEnumerable<TValue> list) {
      var pairs = Functional.Collect<TValue, Tuple<TKey, TReduceInput>>(mapStep, list);
      var groups = Group(pair => pair.Item1, pairs);
      return Functional.Map(
        g => Tuple.Create(g.Key,
          Functional.FoldL(reduceStep, reduceStartVal, g.Values)), groups);
    }

    static IEnumerable<Group<TKey, TValue>> Group<TKey, TValue>(
      Converter<TValue, TKey> extractor, IEnumerable<TValue> list) {
      // This implementation of Group is based on a Group data type
      // that has a mutable list of Values.

      var dict = new Dictionary<TKey, Group<TKey, TValue>>( );
      foreach (TValue val in list) {
        var key = extractor(val);
        if (!dict.ContainsKey(key))
          dict[key] = new Group<TKey, TValue>(key);
        dict[key].Values.Add(val);
      }
      return dict.Values;
    }
    #endregion

  }

  public class Order {
    public string Name { get; set; }
    public List<OrderLine> Lines { get; set; }
  }

  public class OrderLine {
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int Count { get; set; }
  }
}
