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
using FCSColl = FCSlib.Data.Collections;

namespace chapter16 {
  class Program {
    static void Main(string[] args) {
      SimpleChange( );
      MoreComplexChange( );
      AutomaticChange( );

      MyListStep1Test( );
      MyListStep2Test( );
      MyListStep3Test( );
      MyListStep4Test( );

      ListDemo( );

      QueueDemo( );

      UnbalancedBinaryTreeDemo( );

      RedBlackTreeDemo( );
    }

    static void SimpleChange( ) {
      var chicken = new PersistentProduct("Rubber Chicken", 10.99m);
      var discountedChicken = new PersistentProduct(chicken.Name, 8.49m);
    }

    static void MoreComplexChange( ) {
      var chicken = new PersistentProduct("Rubber Chicken", 10.99m);

      // this orderLine relates to the unchanged chicken
      var orderLine = new PersistentOrderLine(chicken, 5);

      // now the chicken "changes" - in this case the local reference to the 
      // "old" chicken is actually dropped
      chicken = new PersistentProduct(chicken.Name, 8.49m);

      // the old orderline needs to be cloned as well
      // (assuming your business logic dictates that the order should be 
      // updated with the price change)
      orderLine = new PersistentOrderLine(chicken, orderLine.Count);
    }

    static void AutomaticChange( ) {
      var chicken = new PersistentProduct("Rubber Chicken", 10.99m);
      var orderLine = new PersistentOrderLine(chicken, 5);

      chicken = chicken.CloneWith(new Dictionary<string, object>{
         {"Price", 8.49m}
      });
      orderLine = orderLine.CloneWith(new Dictionary<string, object>{
        {"Product", chicken}
      });
    }

    static void MyListStep1Test( ) {
      var list = new MyListStep1<int>(10, new MyListStep1<int>(20, MyListStep1<int>.Empty));
    }

    static void MyListStep2Test( ) {
      var list = new MyListStep2<int>(10).Cons(20);
    }

    static void MyListStep3Test( ) {
      var list = new MyListStep3<int>(10, 20, 30, 40);

      var normalDotNetList = new List<int> { 10, 20, 30, 40 };
      var listFromList = new MyListStep3<int>(normalDotNetList);
    }

    static void MyListStep4Test( ) {
      var list = new MyListStep4<int>(10, 20, 30, 40);

      foreach (int i in list)
        Console.WriteLine(i);

      Console.WriteLine(list);

      var normalDotNetList = new List<int>(list);
    }

    static void ListDemo( ) {
      // this is the list I begin with
      var list = new FCSColl::List<int>(10, 20, 30);
      Console.WriteLine(list);

      // adding an item is a function call:
      var addedList = list.Cons(40);
      Console.WriteLine(addedList);

      // removing an item is also a function call:
      var removedList = addedList.Remove(20);
      Console.WriteLine(removedList);
    }

    
    static void QueueDemo( ) {
      // this illustrates the exact steps shown in figure TODO
      // in the beginning, the queue should have this data:
      //   [[1 2] [4 3]]
      // Here's how you may have arrived at that point:

      var q = FCSColl::Queue<int>.Empty;
      Console.WriteLine(q);
      q = q.Snoc(11);
      Console.WriteLine(q);
      q = q.Snoc(1);
      Console.WriteLine(q);
      q = q.Snoc(2);
      Console.WriteLine(q);
      q = q.Tail;
      Console.WriteLine(q);
      q = q.Snoc(3);
      Console.WriteLine(q);
      q = q.Snoc(4);
      Console.WriteLine(q);

      // Now we have reached the state the figure shows in step 1
      // For step 2, an item is added
      q = q.Snoc(5);
      Console.WriteLine(q);

      // Step 3 removes an item
      q = q.Tail;
      Console.WriteLine(q);

      // Step 4 removes another item
      q = q.Tail;
      Console.WriteLine(q);
    }

    static void UnbalancedBinaryTreeDemo( ) {
      var tree = FCSColl::UnbalancedBinaryTree<int>.Empty;
      tree = tree.Insert(10);
      tree = tree.Insert(5);
      tree = tree.Insert(15);
      tree = tree.Insert(1);
      tree = tree.Insert(10);
      Console.WriteLine(tree);
    }

    static void RedBlackTreeDemo( ) {
      var tree = FCSColl::RedBlackTree<int>.Empty;
      tree = tree.Insert(10);
      tree = tree.Insert(5);
      tree = tree.Insert(15);
      tree = tree.Insert(1);
      tree = tree.Insert(10);
      Console.WriteLine(tree);
    }
  }

}
