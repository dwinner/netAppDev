using System;

namespace Interpreter
{
   public class ContainsExpression : ComparisonExpression
   {
      public ContainsExpression(IExpression expressionA, IExpression expressionB)
         : base(expressionA, expressionB)
      {
      }

      public override void Interpret(IContext context)
      {
         ExpressionA.Interpret(context);
         ExpressionB.Interpret(context);
         var exprAresult = context.Get(ExpressionA);
         var exprBResult = context.Get(ExpressionB);
         var resultA = exprAresult as string;
         var resultB = exprBResult as string;
         if (resultA != null && resultB != null
         	 && resultA.IndexOf(resultB, StringComparison.Ordinal) != -1)
         {
            context.AddVariable(this, true);
            return;
         }

         context.AddVariable(this, false);
      }
   }
}