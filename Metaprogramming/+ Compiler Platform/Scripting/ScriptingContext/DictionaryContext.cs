using System.Collections.Generic;

namespace ScriptingContext
{
   public sealed class DictionaryContext
   {
      public Dictionary<string, object> Values { get; } = new Dictionary<string, object>();
   }
}