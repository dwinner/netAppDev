using System.Text;

namespace ReflectiveVisitor;

public static class ExpressionPrinter
{
   private static readonly Dictionary<Type, Action<Expression, StringBuilder>> ActionsMap = new()
   {
      [typeof(DoubleExpression)] = (e, sb) =>
      {
         var de = (DoubleExpression)e;
         sb.Append(de.Value);
      },
      [typeof(AdditionExpression)] = (e, sb) =>
      {
         var ae = (AdditionExpression)e;
         sb.Append('(');
         Print(ae.Left, sb);
         sb.Append('+');
         Print(ae.Right, sb);
         sb.Append(')');
      }
   };

   public static void Print2(this Expression e, StringBuilder sb)
   {
      ActionsMap[e.GetType()](e, sb);
   }

   public static void Print(Expression e, StringBuilder sb)
   {
      switch (e)
      {
         case DoubleExpression de:
            sb.Append(de.Value);
            break;

         case AdditionExpression ae:
            sb.Append('(');
            Print(ae.Left, sb);
            sb.Append('+');
            Print(ae.Right, sb);
            sb.Append(')');
            break;
      }
      // breaks open-closed principle
      // will work incorrectly on missing case
   }
}