namespace Interpreter
{
   public class EqualsExpression : ComparisonExpression
   {
      public EqualsExpression(IExpression expressionA, IExpression expressionB)
         : base(expressionA, expressionB)
      {
      }

      public override void Interpret(IContext context)
      {
         ExpressionA.Interpret(context);
         ExpressionB.Interpret(context);
         var result = context.Get(ExpressionA).Equals(context.Get(ExpressionB));
         context.AddVariable(this, result);
      }
   }
}