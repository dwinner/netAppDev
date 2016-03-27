using System.Reflection;

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
         object source = context.Get(_lookup);
         if (source != null)
         {
            PropertyInfo propertyInfo = source.GetType().GetProperty(_methodName);
            if (propertyInfo != null)
            {
               object result = propertyInfo.GetGetMethod().Invoke(source, null);
               context.AddVariable(this, result);
            }            
         }
      }
   }
}