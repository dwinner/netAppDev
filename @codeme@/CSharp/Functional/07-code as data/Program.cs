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
using System.Linq.Expressions;

namespace chapter7 {
  class Program {
    static void Main(string[] args) {
      AnalyzeExpressions( );

      GenerateExpressions( );

      DotNet4Only( );
    }

    private static void AnalyzeExpressions( ) {
      // a simple lambda expression:
      Func<int, int, int> add = (x, y) => x + y;

      // the local anonymous function can be called directly:
      int result = add(10, 20);
      // result is now 30

      // creating an expression tree instead:
      Expression<Func<int, int, int>> addExpr =
        (x, y) => x + y;
      
      // compile the expression tree
      Func<int, int, int> addCompiled = addExpr.Compile( );

      // executing it works the same as the add function above now
      int result2 = addCompiled(10, 20);
      // result2 is now 30

      // show the expression tree
      ExpressionDumper.Output(addExpr);

      // a few people to work with
      var people = new List<Person> {
		        new Person{Name = "Smith", FirstName = "Harry"},
		        new Person{Name = "Miller", FirstName = "Kate"},
		        new Person{Name = "Jones", FirstName = "Susan"}
		      };

      // select those people who have an "i" in their names
      var peopleWithIInTheirName =
        from p in people
        where p.Name.Contains("i")
        select p;

      // for further analysis, the predicate expression as an
      // expression tree
      Expression<Predicate<Person>> nameContainsI =
        p => p.Name.Contains("i");

      // in TSQL, the following criteria results:
      // CHARINDEX("i", Name) > 0
      var sqlContainsCriteria = ConvertExpressionToTSQL(nameContainsI);
      Console.WriteLine(sqlContainsCriteria);
    }

    static string ConstructWhereClause(Expression expression) {
      return String.Format("WHERE {0}", ConvertExpressionToTSQL(expression));
    }

    static string ConvertExpressionToTSQL(Expression expression) {
      switch (expression.NodeType) {
        case ExpressionType.Lambda:
          return ConvertLambdaExpressionToTSQL((LambdaExpression) expression);
        case ExpressionType.Call:
          return ConvertMethodCallExpressionToTSQL((MethodCallExpression) expression);
        case ExpressionType.Constant:
          return ConvertConstantExpressionToTSQL((ConstantExpression) expression);
        case ExpressionType.MemberAccess:
          return ConvertMemberExpressionToTSQL((MemberExpression) expression);
        case ExpressionType.Parameter:
          return ConvertParameterExpressionToTSQL((ParameterExpression) expression);
        case ExpressionType.GreaterThan:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.GreaterThanOrEqual:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.LessThan:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.LessThanOrEqual:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.Equal:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.AndAlso:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.OrElse:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.Add:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.Subtract:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.Multiply:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        case ExpressionType.Divide:
          return ConvertBinaryExpressionToTSQL((BinaryExpression) expression);
        default:
          return "Unsupported expression type";
      }
    }

    static string ConvertLambdaExpressionToTSQL(LambdaExpression lambdaExpression) {
      // skipping things here, only interested in the body
      return ConvertExpressionToTSQL(lambdaExpression.Body);
    }

    static string ConvertMethodCallExpressionToTSQL(MethodCallExpression methodCallExpression) {
      // the only method that's currently supported for an actual
      // conversion is String.Contains

      var stringContainsMethodInfo = typeof(String).GetMethod("Contains");
      if (methodCallExpression.Method == stringContainsMethodInfo) {
        return String.Format("(CHARINDEX(\"{0}\", {1}) > 0)",
          ConvertExpressionToTSQL(methodCallExpression.Arguments[0]),
          ConvertExpressionToTSQL(methodCallExpression.Object));
      }
      else
        return ConvertUnknownMethodCallExpressionToTSQL(
          methodCallExpression.Method.Name, methodCallExpression.Arguments);
    }

    static string ConvertMemberExpressionToTSQL(MemberExpression memberExpression) {
      return memberExpression.Member.Name;
    }

    static string ConvertConstantExpressionToTSQL(ConstantExpression constantExpression) {
      return constantExpression.Value.ToString( );
    }

    static string ConvertUnknownMethodCallExpressionToTSQL(string methodName, System.Collections.ObjectModel.ReadOnlyCollection<System.Linq.Expressions.Expression> methodArguments) {
      // try to build a somewhat helpful string for 
      // method calls that aren't supported
      StringBuilder builder = new StringBuilder( );
      builder.AppendFormat("UnsupportedMethod{0}(", methodName);
      bool firstArg = true;
      foreach (var argument in methodArguments) {
        if (!firstArg)
          builder.Append(", ");
        else
          firstArg = false;
        builder.Append(ConvertExpressionToTSQL(argument));
      }
      builder.Append(")");
      return builder.ToString( );
    }

    private static string ConvertParameterExpressionToTSQL(ParameterExpression expression) {
      return expression.Name;
    }

    private static string ConvertBinaryExpressionToTSQL(BinaryExpression expression) {
      string op = "UNKNOWN_BINOP";
      switch (expression.NodeType) {
        case ExpressionType.GreaterThan:
          op = ">";
          break;
        case ExpressionType.GreaterThanOrEqual:
          op = ">=";
          break;
        case ExpressionType.LessThan:
          op = "<";
          break;
        case ExpressionType.LessThanOrEqual:
          op = "<=";
          break;
        case ExpressionType.Equal:
          op = "==";
          break;
        case ExpressionType.AndAlso:
          op = "&&";
          break;
        case ExpressionType.OrElse:
          op = "||";
          break;
        case ExpressionType.Add:
          op = "+";
          break;
        case ExpressionType.Subtract:
          op = "-";
          break;
        case ExpressionType.Multiply:
          op = "*";
          break;
        case ExpressionType.Divide:
          op = "/";
          break;
      }
      return String.Format("(({0}) {1} ({2}))",
        ConvertExpressionToTSQL(expression.Left),
        op, ConvertExpressionToTSQL(expression.Right));
    }

    private static void GenerateExpressions( ) {
      // this was the expression that was used previously:
      // Expression<Func<int, int, int>> addExpr = (x, y) => x + y;

      // regenerating the same thing at runtime:
      ParameterExpression xParam = Expression.Parameter(typeof(int), "x");
      ParameterExpression yParam = Expression.Parameter(typeof(int), "y");

      var addExpr = Expression.Lambda<Func<int, int, int>>(
        Expression.Add(xParam, yParam), xParam, yParam);

      // output the expression tree for comparison:
      ExpressionDumper.Output(addExpr);

      // and check that it works
      var add = addExpr.Compile( );
      Console.WriteLine(add(10, 20));

      // create something more complicated - this predicate:
      // (x, y) => (x + 11) > 30 && x < y
      var complexPredicate = Expression.Lambda<Func<int, int, bool>>(
        Expression.AndAlso(
          Expression.GreaterThan(
            Expression.Add(xParam, Expression.Constant(11)),
            Expression.Constant(30)),
          Expression.LessThan(xParam, yParam)),
        xParam, yParam);

      // benefit of the expression system: parse this into a different
      // format, like a string that vaguely resembles SQL
      var predicateSql = ConstructWhereClause(complexPredicate);
      Console.WriteLine(predicateSql);

      // output is this: WHERE ((((((x) + (11))) > (30))) && (((x) < (y))))
      // there's no paren reduction algorithm in place, but otherwise
      // that's obviously an exact representation of the lambda 
      // in the comment above
    }


    internal class Person {
      public string Name { get; set; }
      public string FirstName { get; set; }
    }

    static void DotNet4Only( ) {
      // The function Fact is a simple function to calculate a factorial
      Console.WriteLine(Fact(0)); // 1
      Console.WriteLine(Fact(1)); // 1
      Console.WriteLine(Fact(5)); // 120

      // it's also possible to do the same thing in a lambda
      Func<int, int> fact = x => {
        int result = 1;
        for (int m = 2; m <= x; m++)
          result *= m;
        return result;
      };
      // same results
      Console.WriteLine(fact(0)); // 1
      Console.WriteLine(fact(1)); // 1
      Console.WriteLine(fact(5)); // 120

      // this doesn't compile - no support for statement bodies in
      // expression trees, as far as C# is concerned
      //Expression<Func<int, int>> factExpression = x => {
      //  int result = 1;
      //  for (int m = 2; m <= x; m++)
      //    result *= m;
      //  return result;
      //};


      // now get hold of a version of fact constructed as an 
      // expression tree
      var factRuntime = ConstructRuntimeFact( );
      // same results again
      Console.WriteLine(factRuntime(0)); // 1
      Console.WriteLine(factRuntime(1)); // 1
      Console.WriteLine(factRuntime(5)); // 120
    }

    static int Fact(int x) {
      int result = 1;
      for (int m = 2; m <= x; m++)
        result *= m;
      return result;
    }

    static Func<int, int> ConstructRuntimeFact( ) {
      var param = Expression.Parameter(typeof(int), "x");
      var resultVar = Expression.Variable(typeof(int), "result");
      var mVar = Expression.Variable(typeof(int), "m");
      var loopEnd = Expression.Label( );

      return Expression.Lambda<Func<int, int>>(
        Expression.Block(
          new[] { resultVar },
          Expression.Assign(resultVar, Expression.Constant(1)),
          Expression.Block(
            new[] { mVar },
            Expression.Assign(mVar, Expression.Constant(2)),
            Expression.Loop(
              Expression.Block(
                Expression.IfThen(
                  Expression.Not(
                    Expression.LessThanOrEqual(mVar, param)),
                  Expression.Break(loopEnd)),
                Expression.MultiplyAssign(resultVar, mVar),
                Expression.PostIncrementAssign(mVar)),
              loopEnd)),
          resultVar),
        param).Compile( );
    }
  }
}
