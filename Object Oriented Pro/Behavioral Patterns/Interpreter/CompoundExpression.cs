namespace Interpreter
{
   public abstract class CompoundExpression : IExpression
   {
      protected readonly ComparisonExpression ComparisonExpressionA;
      protected readonly ComparisonExpression ComparisonExpressionB;

      protected CompoundExpression(ComparisonExpression comparisonExpressionA,
         ComparisonExpression comparisonExpressionB)
      {
         ComparisonExpressionA = comparisonExpressionA;
         ComparisonExpressionB = comparisonExpressionB;
      }

      public abstract void Interpret(IContext context);
   }
}