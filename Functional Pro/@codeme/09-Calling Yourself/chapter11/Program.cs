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
using System.Xml;

using FCSColl = FCSlib.Data.Collections;

namespace chapter11 {
  class Program {
    static void Main(string[] args) {
      // a simple direct recursion 
      SimpleRecursion( );

      // This will most probably crash the sample (on purpose!), 
      // so it's commented by default.
      // The example uses tail recursion, but on 32 bit, C# doesn't 
      // benefit from tail call optimization.
      //StackOverflow( );

      // Accumulator passing is a technique to allow structuring
      // for tail recursion.
      AccumulatorPassing( );

      // ... and continuation passing has the same benefits, but
      // is more flexible.
      ContinuationPassing( );

      // The ever-present odd/even example of simple mutual recursion
      OddEven( );

      // A more complex case of indirect recursion
      ReadUI( );

    }

    #region SimpleRecursion
    private static void SimpleRecursion( ) {
      Console.WriteLine("----- Simple Recursion");

      int fact5 = RecFact(5);
      Console.WriteLine(fact5);
    }

    static int RecFact(int x) {
      if (x == 0)
        return 1;
      return x * RecFact(x - 1);
    }
    #endregion

    #region StackOverflow
    private static void StackOverflow( ) {
      Console.WriteLine("----- Stack Overflow");

      Recurse(1);
    }

    static void Recurse(int x) {
      Console.WriteLine("Value: " + x);
      if (x == 100000000)
        return;
      Recurse(x + 1);
    }
    #endregion

    #region Accumulator Passing
    private static void AccumulatorPassing( ) {
      Console.WriteLine("----- Accumulator Passing");

      var list = new FCSColl::List<int>(1, 10, 12, 7, 25);
      
      int l1 = CalcLengthSimple(list);
      Console.WriteLine(l1);

      int l2 = CalcLengthAccumulator(list, 0);
      Console.WriteLine(l2);

      int l3 = CalcLengthAccWrapper(list);
      Console.WriteLine(l3);

      int fact5 = RecFactAcc(5);
      Console.WriteLine(fact5);
    }

    static int CalcLengthSimple<T>(FCSColl::List<T> list) {
      if (list.IsEmpty)
        return 0;
      return 1 + CalcLengthSimple(list.Tail);
    }

    static int CalcLengthAccumulator<T>(FCSColl::List<T> list, int accumulator) {
      return list.IsEmpty ? 
        accumulator : 
        CalcLengthAccumulator(list.Tail, accumulator + 1);
    }

    static int CalcLengthAccWrapper<T>(FCSColl::List<T> list) {
      Func<FCSColl::List<T>, int, int> accumulator = null;
      accumulator = 
        (l, acc) => l.IsEmpty ? acc : accumulator(l.Tail, acc + 1);
      return accumulator(list, 0);
    }

    static int RecFactAcc(int x) {
      Func<int, int, int> acc = null;
      acc = (val, accVal) => val == 0 ? accVal : acc(val - 1, accVal * val);
      return acc(x, 1);
    }
    #endregion

    #region Continuation Passing
    private static void ContinuationPassing( ) {
      Console.WriteLine("----- Continuation Passing");

      // a very simple example of a process with multiple
      // steps being implemented with a return value
      // vs continuation style
      Console.WriteLine(Times3(5));
      Times3CPS(5, x => Console.WriteLine(x));

      var list = new FCSColl::List<int>(1, 2, 3, 4, 5);
      
      var sum1 = SumRecursive(list);
      Console.WriteLine(sum1);

      SumCPS1(list, sum2 => Console.WriteLine(sum2));

      var sum3 = SumCPS2(list, x => x);
      Console.WriteLine(sum3);

      var sum4 = SumCPS3(list);
      Console.WriteLine(sum4);
    }

    static int Times3(int x) {
      // this function has two steps:
      // - calculate something based on the parameter x
      // - return the value to the caller using "return"
      return x * 3;
    }

    static void Times3CPS(int x, Action<int> continuation) {
      // this function has two steps:
      // - calculate something based on the parameter x
      // - pass the result on to another function that's given 
      //   given as the continuation
      continuation(x * 3);
    }

    static int SumRecursive(FCSColl::List<int> list) {
      if (list.IsEmpty)
        return 0;
      return list.Head + SumRecursive(list.Tail);
    }

    static void SumCPS1(FCSColl::List<int> list, Action<int> cont) {
      if (list.IsEmpty)
        cont(0);
      else
        SumCPS1(list.Tail, x => cont(x + list.Head));
    }

    static int SumCPS2(FCSColl::List<int> list, Func<int, int> cont) {
      return list.IsEmpty ? 
        cont(0) : 
        SumCPS2(list.Tail, x => cont(x + list.Head));
    }

    static int SumCPS3(FCSColl::List<int> list) {
      Func<FCSColl::List<int>, Func<int, int>, int> recursor = null;
      recursor = (l, cont) =>
        l.IsEmpty ?
        cont(0) :
        recursor(l.Tail, x => cont(x + l.Head));
      return recursor(list, x => x);
    }
    #endregion

    #region OddEven
    private static void OddEven( ) {
      Console.WriteLine("----- Odd/Even");

      Console.WriteLine("IsOdd 5: {0}", IsOdd(5));
      Console.WriteLine("IsOdd 12: {0}", IsOdd(12));
      Console.WriteLine("IsEven 8: {0}", IsEven(8));
      Console.WriteLine("IsEven 3: {0}", IsEven(3));
    }

    static bool IsOdd(int number) {
      return number == 0 ? false : IsEven(Math.Abs(number) - 1);
    }

    static bool IsEven(int number) {
      return number == 0 ? true : IsOdd(Math.Abs(number) - 1);
    }
    #endregion

    #region ReadUI

    static void ReadUI( ) {
      Console.WriteLine("----- Read UI");

      XmlDocument doc = new XmlDocument( );
      doc.Load("ui.xml");
      var elementTree = BuildElementTree(doc.DocumentElement);
    }

    static Element BuildElementTree(XmlNode xmlNode) {
      switch (xmlNode.Name) {
        case "form":
          return BuildForm(xmlNode);
        case "group":
          return BuildGroup(xmlNode);
        case "label":
          return BuildLabel(xmlNode);
        case "checkbox":
          return BuildCheckbox(xmlNode);
        case "button":
          return BuildButton(xmlNode);
        case "edit":
          return BuildEdit(xmlNode);
        default:
          return null;
      }
    }

    private static Element BuildEdit(XmlNode xmlNode) {
      return new Edit { Content = xmlNode.InnerText };
    }

    private static Element BuildButton(XmlNode xmlNode) {
      return new Button { Content = xmlNode.InnerText };
    }

    private static Element BuildCheckbox(XmlNode xmlNode) {
      return new Checkbox { Content = xmlNode.InnerText };
    }

    private static Element BuildLabel(XmlNode xmlNode) {
      return new Label { Content = xmlNode.InnerText };
    }

    private static Element BuildGroup(XmlNode xmlNode) {
      return new Group { Elements = BuildList(xmlNode.ChildNodes) };
    }

    private static Element BuildForm(XmlNode xmlNode) {
      return new Form { Elements = BuildList(xmlNode.ChildNodes) };
    }

    private static List<Element> BuildList(XmlNodeList xmlNodeList) {
      var result = new List<Element>( );
      foreach (XmlNode node in xmlNodeList)
        result.Add(BuildElementTree(node));
      return result;
    }

    public class Element { }
    public class ContentElement : Element {
      public string Content { get; set; }
    }
    public class Group : Element {
      public List<Element> Elements { get; set; }
    }
    public class Form : Group { }
    public class Label : ContentElement { }
    public class Checkbox : ContentElement { }
    public class Button : ContentElement { }
    public class Edit : ContentElement { }
    #endregion

  }

}
