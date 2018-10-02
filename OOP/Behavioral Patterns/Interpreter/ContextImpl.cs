using System.Collections.Generic;

namespace Interpreter
{
   public class ContextImpl : IContext
   {
      private readonly IDictionary<object, object> _varMap = new Dictionary<object, object>();

      public object Get(object key) => _varMap[key];

      public void AddVariable(object key, object value) => _varMap[key] = value;
   }
}