namespace Interpreter
{
   public class AndExpression : CompoundExpression
   {
      public AndExpression(ComparisonExpression comparisonExpressionA, ComparisonExpression comparisonExpressionB)
         : base(comparisonExpressionA, comparisonExpressionB)
      {
      }

      public override void Interpret(IContext context)
      {
         ComparisonExpressionA.Interpret(context);
         ComparisonExpressionB.Interpret(context);
         var result = (bool) context.Get(ComparisonExpressionA) && (bool) context.Get(ComparisonExpressionB);
         context.AddVariable(this, result);
      }
   }
}