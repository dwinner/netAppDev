﻿/*
 * Деревья выражений
 */

using System;
using System.Linq.Expressions;
using DataLib;

namespace ExpressionTreeSample
{
   static class Program
   {
      static void Main()
      {
         Expression<Func<Racer, bool>> expression = r => r.Country == "Brazil" && r.Wins > 6;
         DisplayTree(0, "Lambda", expression);
      }

      private static void DisplayTree(int indent, string message, Expression expression)
      {
         string output = String.Format("{0} {1} ! NodeType: {2}; Expr: {3} ", "".PadLeft(indent, '>'), message,
            expression.NodeType, expression);

         indent++;
         switch (expression.NodeType)
         {
            case ExpressionType.Lambda:
               Console.WriteLine(output);
               var lambdaExpr = (LambdaExpression)expression;
               foreach (var parameter in lambdaExpr.Parameters)
               {
                  DisplayTree(indent, "Parameter", parameter);
               }
               DisplayTree(indent, "Body", lambdaExpr.Body);
               break;

            case ExpressionType.Constant:
               var constExpr = (ConstantExpression)expression;
               Console.WriteLine("{0} Const Value: {1}", output, constExpr.Value);
               break;

            case ExpressionType.Parameter:
               var paramExpr = (ParameterExpression)expression;
               Console.WriteLine("{0} Param Type: {1}", output, paramExpr.Type.Name);
               break;

            case ExpressionType.Equal:
            case ExpressionType.AndAlso:
            case ExpressionType.GreaterThan:
               var binExpr = (BinaryExpression)expression;
               if (binExpr.Method != null)
               {
                  Console.WriteLine("{0} Method: {1}", output, binExpr.Method.Name);
               }
               else
               {
                  Console.WriteLine(output);
               }

               DisplayTree(indent, "Left", binExpr.Left);
               DisplayTree(indent, "Right", binExpr.Right);
               break;

            case ExpressionType.MemberAccess:
               var memberExpr = (MemberExpression)expression;
               Console.WriteLine("{0} Member Name: {1}, Type: {2}", output, memberExpr.Member.Name, memberExpr.Type.Name);
               DisplayTree(indent, "Member Expr", memberExpr.Expression);
               break;

            default:
               Console.WriteLine();
               Console.WriteLine("{0} {1}", expression.NodeType, expression.Type.Name);
               break;
         }
      }
   }
}
