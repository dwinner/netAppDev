namespace Interpreter
{
   public class OrExpression : ComparisonExpression
   {
      public OrExpression(IExpression expressionA, IExpression expressionB)
         : base(expressionA, expressionB)
      {
      }

      public override void Interpret(IContext context)
      {
         ExpressionA.Interpret(context);
         ExpressionB.Interpret(context);
         var result = (bool) context.Get(ExpressionA) || (bool) context.Get(ExpressionB);
         context.AddVariable(this, result);
      }
   }
}