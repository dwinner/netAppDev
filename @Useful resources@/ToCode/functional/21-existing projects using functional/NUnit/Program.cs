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
using NUnit.Framework.Constraints;

namespace NUnit {
  class Program {
    static void Main(string[] args) {
    }
  }

  public static class Calculator {
    public static int Add(int a, int b) {
      return a + b;
    }
  }

  public static class DataSource {
    public static string GetValue( ) {
      return "Some String";
    }

    public static IEnumerable<string> GetData( ) {
      yield return "First string";
      yield return "Second string";
      yield return "Another string";
      yield return "One final string";
    }
  }

  [TestFixture]
  public class Tests {
    [Test]
    internal void AddTest1( ) {
      Assert.AreEqual(20 + 10, Calculator.Add(20, 10));
    }

    [Test]
    public void AddTest1f( ) {
      Assert.That(Calculator.Add(20, 10),
        Is.EqualTo(10 + 20));
    }

    [Test]
    public void ValueTest1f( ) {
      Assert.That(DataSource.GetValue( ),
        Is.Not.Null.
        And.StartsWith("S").
        And.Not.EqualTo("Something else"));
    }

    [Test]
    public void DataTest1f( ) {
      Assert.That(DataSource.GetData( ).ToList( ),
        Has.Count.GreaterThan(0).
          And.Some.Matches<string>(v => v.StartsWith("O")));
    }

    [Test]
    public void DataTest1( ) {
      var step1 = new ConstraintExpression( );
      var step2 = step1.Append(new PropOperator("Count"));
      var step3 = step2.Append(new GreaterThanConstraint(0));

      var step4 = step3.And;
      var step5 = step4.Append(new SomeOperator( ));
      var step6 = step5.Append(new PredicateConstraint<string>(v => v.StartsWith("O")));

      Assert.That(DataSource.GetData( ).ToList( ), step6);
    }
  }
}
