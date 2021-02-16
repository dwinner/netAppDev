using System.Linq.Expressions;

namespace ExpressionSamples
{
   internal sealed class AddToSubtractExpressionVisitor : ExpressionVisitor
   {
      internal Expression Change(Expression expression) => Visit(expression);

      protected override Expression VisitBinary(BinaryExpression node)
         => node.NodeType == ExpressionType.Add ? Expression.Subtract(Visit(node.Left), Visit(node.Right)) : node;
   }
}