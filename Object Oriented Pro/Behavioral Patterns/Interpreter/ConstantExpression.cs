namespace Interpreter
{
   public class ConstantExpression : IExpression
   {
      private readonly object _value;

      public ConstantExpression(object value)
      {
         _value = value;
      }

      public void Interpret(IContext context) => context.AddVariable(this, _value);
   }
}