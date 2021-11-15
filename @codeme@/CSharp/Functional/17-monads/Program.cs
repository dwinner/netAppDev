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

using FCSColl = FCSlib.Data.Collections;
using FCSlib;


namespace chapter17 {
  class Program {
    static void Main(string[] args) {
      AccessTreeStep1( );
      AccessTreeStep2( );
      AccessTreeStep3( );

      Logger( );

      EsotericMaybeDemos( );
    }


    #region Access tree, step 1
    private static void AccessTreeStep1( ) {
      var tree = new FCSColl::UnbalancedBinaryTree<string>( );
      tree = tree.Insert("Paul");
      tree = tree.Insert("Adam");
      tree = tree.Insert("Bernie");
      tree = tree.Insert("Willy");
      tree = tree.Insert("Suzie");
      Console.WriteLine(tree);

      // Try to get hold of the third "left" child from the top node.
      // This throws an exception, because tree.Left.Left.Left is null
      //Console.WriteLine(tree.Left.Left.Left.Value);

      // Problem: when accessing elements in a chain like this, any one of them
      // could be null. So, to be safe:
      Console.WriteLine(GetThirdLeftChild(tree));
    }

    static string GetThirdLeftChild(FCSColl::UnbalancedBinaryTree<string> tree) {
      if (tree != null) {
        if (tree.Left != null) {
          if (tree.Left.Left != null) {
            if (tree.Left.Left.Left != null) {
              return tree.Left.Left.Left.Value;
            }
            else {
              return "No such child";
            }
          }
          else {
            return "No such child";
          }
        }
        else {
          return "No such child";
        }
      }
      else {
        return "No such child";
      }
    }
    #endregion

    #region Access tree, step 2
    private static void AccessTreeStep2( ) {
      var tree = new FCSColl::UnbalancedBinaryTree<string>( );
      tree = tree.Insert("Paul");
      tree = tree.Insert("Adam");
      tree = tree.Insert("Bernie");
      tree = tree.Insert("Willy");
      tree = tree.Insert("Suzie");
      Console.WriteLine(tree);

      var thirdLeftChild = GetMaybeThirdLeftChild(tree);
      if (thirdLeftChild.HasValue)
        Console.WriteLine(thirdLeftChild.Value);
      else
        Console.WriteLine("Maybe is empty");
    }

    static Maybe<string> GetMaybeThirdLeftChild(FCSColl::UnbalancedBinaryTree<string> tree) {
      if (tree != null) {
        if (tree.Left != null) {
          if (tree.Left.Left != null) {
            if (tree.Left.Left.Left != null) {
              return tree.Left.Left.Left.Value.ToMaybe( );
            }
            else {
              return Maybe<string>.Empty;
            }
          }
          else {
            return Maybe<string>.Empty;
          }
        }
        else {
          return Maybe<string>.Empty;
        }
      }
      else {
        return Maybe<string>.Empty;
      }
    }

    #endregion
  
    #region Access tree, step 3
    private static void AccessTreeStep3( ) {
      var tree = new FCSColl::UnbalancedBinaryTree<string>( );
      tree = tree.Insert("Paul");
      tree = tree.Insert("Adam");
      tree = tree.Insert("Bernie");
      tree = tree.Insert("Willy");
      tree = tree.Insert("Suzie");
      Console.WriteLine(tree);

      var thirdLeftChild = GetMonadicThirdLeftChild(tree);
      if (thirdLeftChild.HasValue)
        Console.WriteLine(thirdLeftChild.Value.Value);
      else
        Console.WriteLine("Maybe is empty again");

      // Now we've seen all the failures, here's the same for the child one left one right from the top,
      // which should be Bernie:
      var bernie =
        tree.ToNotNullMaybe( ).
          Bind(t => t.Left.ToNotNullMaybe( )).
          Bind(t => t.Right.ToNotNullMaybe( ));
      Console.WriteLine(bernie.HasValue ? bernie.Value.Value : "No Bernie found");
    }

    static Maybe<FCSColl::UnbalancedBinaryTree<string>> GetMonadicThirdLeftChild(FCSColl::UnbalancedBinaryTree<string> tree) {
      return tree.ToNotNullMaybe( ).
        Bind(t => t.Left.ToNotNullMaybe( )).
        Bind(t => t.Left.ToNotNullMaybe( )).
        Bind(t => t.Left.ToNotNullMaybe( ));
    }

    #endregion

    #region Logger
    private static void Logger( ) {
      var orders = new List<Order> {
        new Order{ Date =new DateTime(2010,6,3), Value = 29.9m},
        new Order{ Date =new DateTime(2010,6,3), Value = 18.6m},
        new Order{ Date =new DateTime(2010,6,4), Value = 119.99m},
        new Order{ Date =new DateTime(2010,7,1), Value = 3.99m},
        new Order{ Date =new DateTime(2010,7,2), Value = 47.62m},
        new Order{ Date =new DateTime(2010,7,3), Value = 99.99m}
      };

      var average =
        orders.ToLogger("Starting with a list of {0} orders", orders.Count()).
        Bind(l => Functional.Filter(o => o.Date >= new DateTime(2010, 7, 1), l).ToLogger(
          "Got list with {0} items, filtering...", l.Count())).
        Bind(l => l.Average(o => o.Value).ToLogger(
          "Calculating average for list with remaining {0} items...", l.Count()));
      
      Console.WriteLine("Result: " + average.Value);

      Console.WriteLine("-------- Log Output:");
      Console.Write(average.LogOutput( ));
    }
    #endregion

    #region Esoteric Maybe Demos
    private static void EsotericMaybeDemos( ) {
      var result1 = 5.ToMaybe( ).
        Bind<int>(v => Maybe<int>.Empty.Bind<int>(v2 => (v + v2).ToMaybe( )));
      Console.WriteLine("Result 1: " + (result1.HasValue ? result1.Value.ToString( ) : "None"));

      var result2 = 5.ToMaybe( ).SelectMany<int, int>(
        v => Maybe<int>.Empty.SelectMany<int, int>(
          v2 => (v + v2).ToMaybe( )));
      Console.WriteLine("Result 2: " + (result2.HasValue ? result2.Value.ToString( ) : "None"));

      var result3 = 
        from x in 5.ToMaybe( )
        from y in Maybe<int>.Empty
        select (x + y);
      Console.WriteLine("Result 3: " + (result3.HasValue ? result3.Value.ToString( ) : "None"));

      var result4 =
        from x in Maybe<int>.Empty
        from y in 5.ToMaybe( )
        select (x + y);
      Console.WriteLine("Result 4: " + (result4.HasValue ? result4.Value.ToString( ) : "None"));

      var result5 =
        from x in 5.ToMaybe( )
        from y in 7.ToMaybe( )
        select (x + y);
      Console.WriteLine("Result 5: " + (result5.HasValue ? result5.Value.ToString( ) : "None"));
    }

    #endregion
  }
}
