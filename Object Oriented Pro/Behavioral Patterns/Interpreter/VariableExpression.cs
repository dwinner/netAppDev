namespace Interpreter
{
   public class VariableExpression : IExpression
   {
      private readonly object _lookup;
      private readonly string _methodName;

      public VariableExpression(object lookup, string methodName)
      {
         _lookup = lookup;
         _methodName = methodName;
      }

      public void Interpret(IContext context)
      {
         var source = context.Get(_lookup);
         if (source != null)
         {
            var propertyInfo = source.GetType().GetProperty(_methodName);
            if (propertyInfo != null)
            {
               var result = propertyInfo.GetGetMethod().Invoke(source, null);
               context.AddVariable(this, result);
            }
         }
      }
   }
}