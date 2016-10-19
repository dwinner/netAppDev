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
using System.Drawing;
using System.Diagnostics;
using FCSlib;
using FCSlib.Data;

namespace chapter10 {
  class Program {
    static void Main(string[] args) {
      ImperativePrecomputation( );

      ObjectOrientedPrecomputation( );

      FunctionalPrecomputationListLookup( );

      FunctionalPrecomputationPointRotation( );

      AutomaticMemoization( );
    }

    #region Imperative Precomputation
    private static void ImperativePrecomputation( ) {
      double[] sines = new double[360];
      for (int i = 0; i < 360; i++) {
        sines[i] = Math.Sin(i);
      }

      // Now your algorithm is just going to look up the sine
      // values in 'sines' instead of having to calculate them.
    }
    #endregion

    #region Object oriented precomputation
    private static void ObjectOrientedPrecomputation( ) {
      Console.WriteLine("===== Object oriented precomputation");

      var rotator = new PointRotator( );

      Console.WriteLine(PointRotator.RotatePoint(new PointF(4, 3), 3));
      Console.WriteLine(rotator.RotatePointWithLookups(new PointF(4, 3), 3));

      Console.WriteLine(PointRotator.RotatePoint(new PointF(10, 25), 74));
      Console.WriteLine(rotator.RotatePointWithLookups(new PointF(10, 25), 74));
    }
    #endregion

    #region Functional Precomputation List Lookup
    private static void FunctionalPrecomputationListLookup( ) {
      Console.WriteLine("===== Functional precomputation list lookup");

      DumbDemo1( );
      DumbDemo2( );
      CleverDemo( );
    }

    private static void DumbDemo1( ) {
      Console.WriteLine("======== Dumb demo 1");

      string[] strings =
					{
						"One", "Two", "Three", "Four", "Five",
						"Six", "Seven", "Eight", "Nine", "Ten"
					};

      // Even though the IsInListDumb function uses an efficient
      // lookup data structure, it computes that data structure
      // on every call, eliminating the benefits.
      Console.WriteLine(IsInListDumb(strings, "Three"));
      Console.WriteLine(IsInListDumb(strings, "NotInList"));
    }

    static void DumbDemo2( ) {
      Console.WriteLine("======== Dumb demo 2");

      string[] strings =
					{
						"One", "Two", "Three", "Four", "Five",
						"Six", "Seven", "Eight", "Nine", "Ten"
					};

      // We're trying to be clever and pass in the list first, 
      // thinking that then the lookup structure will be calculated
      // just once. But the structure of the IsInListDumb function is 
      // not right for that sort of an approach - it still
      // computes the lookup structures on each of the lookup calls.
      var curriedLookup = Functional.Curry<IEnumerable<string>, string, bool>(IsInListDumb<string>);

      var precomputedLookup = curriedLookup(strings);
      Console.WriteLine(precomputedLookup("Three"));
      Console.WriteLine(precomputedLookup("NotInList"));
    }

    static bool IsInListDumb<T>(IEnumerable<T> list, T item) {
      var hashSet = new HashSet<T>(list);
      Console.WriteLine("(Dumb) Hash set constructed");
      Console.WriteLine("(Dumb) Performing lookup");
      return hashSet.Contains(item);
    }

    private static void CleverDemo( ) {
      Console.WriteLine("======== Clever demo");

      string[] strings =
					{
						"One", "Two", "Three", "Four", "Five",
						"Six", "Seven", "Eight", "Nine", "Ten"
					};

      // The CleverListLookup function takes only one parameter and 
      // uses it to compute the efficient lookup structure. It returns
      // a new function, and that function can then becalled as often
      // as necessary, using the same lookup data.
      var lookup = CleverListLookup(strings);
      Console.WriteLine(lookup("Three"));
      Console.WriteLine(lookup("NotInList"));
    }

    static Func<T, bool> CleverListLookup<T>(IEnumerable<T> list) {
      // This is really a variation of a function in general curried
      // format. It takes just one parameter at a time, returning a new 
      // function to take the second parameter. But it behaves intelligently
      // by using the first parameter as soon as it is passed in and 
      // precomputing the efficient lookup structure. 
      // The lambda expression that is returned from the function uses
      // the hashSet, which the compiler stores in a closure.

      var hashSet = new HashSet<T>(list);
      Console.WriteLine("(Clever) Hash set constructed");
      return item => {
        Console.WriteLine("(Clever) Performing lookup");
        return hashSet.Contains(item);
      };
    }
    #endregion

    #region Function precomputation point rotation
    private static void FunctionalPrecomputationPointRotation( ) {
      Console.WriteLine("===== Functional precomputation point rotation");

      var rotate = CreateFunctionalRotator( );

      Console.WriteLine(rotate(new PointF(4, 3))(3));
      Console.WriteLine(rotate(new PointF(10, 25))(74));
    }

    private static Func<PointF, Func<double, PointF>> CreateFunctionalRotator( ) {
      Func<double, double> toRad = x => x * Math.PI / 180.0;
      Func<double, double> toDeg = x => x / (Math.PI / 180.0);

      float[] sines = Functional.InitArray<float>(360,
        i => (float) Math.Sin(toRad(i)));
      float[] cosines = Functional.InitArray<float>(360,
        i => (float) Math.Cos(toRad(i)));

      Func<int, int> normalize = v => ((v % 360) + 360) % 360;
      Func<int, float> sin = v => sines[normalize(v)];
      Func<int, float> cos = v => cosines[normalize(v)];
      Func<float, float> square = x => x * x;

      return p => a => {
        double polarLength = Math.Sqrt(square(p.X) + square(p.Y));
        double polarAngle = toDeg(Math.Atan(p.Y / p.X));
        int newAngleInt = (int) Math.Round(polarAngle + a);
        float cartesianX = (float) polarLength * cos(newAngleInt);
        float cartesianY = (float) polarLength * sin(newAngleInt);
        return new PointF(cartesianX, cartesianY);
      };
    }
    #endregion

    private static void InternalMemoization( ) {
      throw new NotImplementedException( );
    }

    private static int SquareSimple(int x) {
      return x * x;
    }

    static Dictionary<int, int> squareInternalMemory;
    private static int SquareInternalMemoized(int x) {
      if (squareInternalMemory == null)
        squareInternalMemory = new Dictionary<int, int>( );
      if (!squareInternalMemory.ContainsKey(x)) {
        int result = x * x;
        squareInternalMemory[x] = result;
        return result;
      }
      return squareInternalMemory[x];
    }

    static int SquareInternalAutoMemoized(int x) {
      var memory = Memoizer<int, int>.GetMemory("SquareInternalAutoMemoized");
      if (!memory.HasResultFor(x)) {
        int result = x * x;
        memory.Remember(x, result);
        return result;
      }
      return memory.ResultFor(x);
    }

    static void AlgorithmWithSquares( ) {
      var memoizedSquare = Functional.Memoize<int, int>(SquareSimple);
      var memoizedLambdaSquare = Functional.Memoize<int, int>(x => x * x);
      var memoizedDelegateSquare =
        Functional.Memoize(
        delegate(int x) {
          return x * x;
        });
    }

    private static void AutomaticMemoization( ) {
      FirstTryMultipleParameters( );
      ManualMultipleParameters( );
    }

    static Func<P, R> LoggingMemoize<P, R>(Func<P, R> f) {
      var memory = new Dictionary<P, R>( );

      return arg => {
        if (!memory.ContainsKey(arg)) {
          Console.WriteLine("Memory doesn't have result for {0}, calling function... ", arg);
          memory[arg] = f(arg);
          Console.WriteLine("  ... memorizing result {0}", memory[arg]);
          return memory[arg];
        }
        else {
          Console.WriteLine("Returning result {0} for {1} from memory", memory[arg], arg);
          return memory[arg];
        }
      };
    }

    static void FirstTryMultipleParameters( ) {
      Console.WriteLine("===== First try multiple parameters");
      Func<int, Func<int, int>> add = x => y => x + y;
      var memoizedAdd = LoggingMemoize(add);
      Console.WriteLine("Adding 10 + 3: {0}", memoizedAdd(10)(3));
      Console.WriteLine("Adding 10 + 4: {0}", memoizedAdd(10)(4));
      Console.WriteLine("Adding 10 + 3: {0}", memoizedAdd(10)(3));
    }

    private static void ManualMultipleParameters( ) {
      Console.WriteLine("===== Manual multiple parameters");
      var memoizedAdd =
        LoggingMemoize<int, Func<int, int>>(x => LoggingMemoize<int, int>(y => x + y));
      Console.WriteLine("Adding 10 + 3: {0}", memoizedAdd(10)(3));
      Console.WriteLine("Adding 10 + 4: {0}", memoizedAdd(10)(4));
      Console.WriteLine("Adding 10 + 3: {0}", memoizedAdd(10)(3));
    }

  }
}