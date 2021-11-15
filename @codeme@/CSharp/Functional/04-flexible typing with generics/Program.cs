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

namespace chapter4 {
  class Program {
    static void Main(string[] args) {
      SimpleFunctions( );
      MoreComplexFunctions( );

      ListTest( );

      Constraints( );

      CoContravariance( );

    }

    private static void SimpleFunctions( ) {
      OutputThing<string>("A string");
      OutputThing<int>(42);

      OutputThing("A different string");
      OutputThing(52);
    }

    static void OutputThing<T>(T thing) {
      Console.WriteLine("Thing: {0}", thing);
    }

    private static void MoreComplexFunctions( ) {
      var values = new List<int> { 10, 20, 35 };
      Apply(values, v => Console.WriteLine("Value: {0}", v));
    }

    static void Apply<T>(IEnumerable<T> sequence, Action<T> action) {
      foreach (T item in sequence) {
        action(item);
      }
    }

    private static void ListTest( ) {
      var intItem = new ListItem<int>(10);
      
      // Prepending further int values is possible
      var secondItem = intItem.Prepend(20);

      // Prepending other types is caught by the compiler
      //var thirdItem = secondItem.Prepend("string");
    }

    static void OutputValue<T>(T value) where T : ListItem<string> {
      Console.WriteLine("String list value: {0}", value.Value);
    }

    private static void Constraints( ) {
      var lis = new ListItem<string>("text");
      OutputValue(lis);
      var sli = new StringListItem("text");
      OutputValue(sli);

      var val = 42;
      // Now this doesn't work due to the constraint
      //OutputValue(val);
    }

    private static void CoContravariance( ) {
      ArrayAssignment( );
      DelegateContravariance( );
      GenericsVariance( );
    }

    private static void ArrayAssignment( ) {
      object[] objects = new object[3];
      objects[0] = new object( );
      objects[1] = "Just a string";
      objects[2] = 10;

      string[] strings = new string[] { "one", "two", "three" };
      objects = strings;


      // Runtime exception here - the array is still of type string[], ints can't be inserted
      // objects[2] = 10;

      // Compiler error here - covariance support in this scenario only covers reference 
      // types, and int is a value type
      // int[] ints = new int[] { 1, 2, 3 };
      // objects = ints;
    }

    private static void DelegateContravariance( ) {
      Woman woman = new Woman( );
      DoWork(woman, WorkWithWoman);
      DoWork(woman, WorkWithPerson);
    }

    static void WorkWithPerson(Person person) {
    }

    static void WorkWithWoman(Woman woman) {
    }

    delegate void AcceptWomanDelegate(Woman person);

    static void DoWork(Woman woman, AcceptWomanDelegate acceptWoman) {
      acceptWoman(woman);
    }

    private static void GenericsVariance( ) {
      // This does not work:
      // List<object> objectlist = new List<object>( );
      // List<string> stringlist = new List<string>( );
      // objectlist = stringlist;

      IEnumerable<object> objectSequence = new List<object>( );
      IEnumerable<string> stringSequence = new List<string>( );
      objectSequence = stringSequence;

    }
  }
}