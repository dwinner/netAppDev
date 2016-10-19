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

using FCSlib;
using FCSlib.Data;

namespace MapReduceSample1 {
  class Program {
    static void Main(string[] args) {
      // Do it manually

      // Step 1: Map      
      var pairs = Functional.Collect(
        text => Functional.Map(
          word => Tuple.Create(word, 1),
          text.Split(new[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)),
        new[] { hamlet });

      // Step 1a, intermediate: Group
      var groups = Group(pair => pair.Item1, pairs);

      // Step 2: Reduce each group
      var results = Functional.Map(
        g => Tuple.Create(g.Key,
          Functional.FoldL((r, v) => r + v.Item2, 0, g.Values)),
        groups);

      foreach (var t in results.OrderBy(t => t.Item1))
        Console.WriteLine("{0}: {1}", t.Item1, t.Item2);
      Console.WriteLine("{0} results", results.Count( ));

      // Use an abstract function
      var newResults =
        MapReduce(
          text => Functional.Map(
            word => Tuple.Create(word, 1),
            text.Split(new[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)),
          (r, v) => r + v.Item2, 0,
          new[] { hamlet });
      Console.WriteLine("---------------------------------------------");

      foreach (var t in newResults.OrderBy(t => t.Item1))
        Console.WriteLine("{0}: {1}", t.Item1, t.Item2);
      Console.WriteLine("{0} results", newResults.Count( ));

    }

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

    const string hamlet = @"Though yet of Hamlet our dear brother's death
The memory be green, and that it us befitted
To bear our hearts in grief and our whole kingdom
To be contracted in one brow of woe,
Yet so far hath discretion fought with nature
That we with wisest sorrow think on him,
Together with remembrance of ourselves.
Therefore our sometime sister, now our queen,
The imperial jointress to this warlike state,
Have we, as 'twere with a defeated joy,--
With an auspicious and a dropping eye,
With mirth in funeral and with dirge in marriage,
In equal scale weighing delight and dole,--
Taken to wife: nor have we herein barr'd
Your better wisdoms, which have freely gone
With this affair along. For all, our thanks.
Now follows, that you know, young Fortinbras,
Holding a weak supposal of our worth,
Or thinking by our late dear brother's death
Our state to be disjoint and out of frame,
Colleagued with the dream of his advantage,
He hath not fail'd to pester us with message,
Importing the surrender of those lands
Lost by his father, with all bonds of law,
To our most valiant brother. So much for him.
Now for ourself and for this time of meeting:
Thus much the business is: we have here writ
To Norway, uncle of young Fortinbras,--
Who, impotent and bed-rid, scarcely hears
Of this his nephew's purpose,--to suppress
His further gait herein; in that the levies,
The lists and full proportions, are all made
Out of his subject: and we here dispatch
You, good Cornelius, and you, Voltimand,
For bearers of this greeting to old Norway;
Giving to you no further personal power
To business with the king, more than the scope
Of these delated articles allow.
Farewell, and let your haste commend your duty.";
  }

}

