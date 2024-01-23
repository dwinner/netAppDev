using System;
using System.Linq.Expressions;
using DataLib;

Expression<Func<Racer, bool>> expression = r => r.Country == "Brazil" && r.Wins > 6;

DisplayTree(0, "Lambda", expression);
return;

void DisplayTree(int indent, string message, Expression expr)
{
   var output = $"{string.Empty.PadLeft(indent, '>')} {message}! NodeType: {expr.NodeType}; Expr: {expr}";

   indent++;
   switch (expr.NodeType)
   {
      case ExpressionType.Lambda:
         Console.WriteLine(output);
         var lambdaExpr = (LambdaExpression)expr;
         foreach (var parameter in lambdaExpr.Parameters)
         {
            DisplayTree(indent, "Parameter", parameter);
         }

         DisplayTree(indent, "Body", lambdaExpr.Body);
         break;
      case ExpressionType.Constant:
         var constExpr = (ConstantExpression)expr;
         Console.WriteLine($"{output} Const Value: {constExpr.Value}");
         break;
      case ExpressionType.Parameter:
         var paramExpr = (ParameterExpression)expr;
         Console.WriteLine($"{output} Param Type: {paramExpr.Type.Name}");
         break;
      case ExpressionType.Equal:
      case ExpressionType.AndAlso:
      case ExpressionType.GreaterThan:
         var binExpr = (BinaryExpression)expr;
         if (binExpr.Method != null)
         {
            Console.WriteLine($"{output} Method: {binExpr.Method.Name}");
         }
         else
         {
            Console.WriteLine(output);
         }

         DisplayTree(indent, "Left", binExpr.Left);
         DisplayTree(indent, "Right", binExpr.Right);
         break;
      case ExpressionType.MemberAccess:
         var memberExpr = (MemberExpression)expr;
         Console.WriteLine($"{output} Member Name: {memberExpr.Member.Name}, Type: {memberExpr.Type.Name}");
         if (memberExpr.Expression != null)
         {
            DisplayTree(indent, "Member Expr", memberExpr.Expression);
         }

         break;
      default:
         Console.WriteLine();
         Console.WriteLine($"{expr.NodeType} {expr.Type.Name}");
         break;
   }
}