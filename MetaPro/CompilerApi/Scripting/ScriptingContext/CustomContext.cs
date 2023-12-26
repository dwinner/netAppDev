using System.IO;

namespace ScriptingContext
{
   public class CustomContext
   {
      public CustomContext(Context context, TextWriter myOut)
      {
         Context = context;
         MyOut = myOut;
      }

      public Context Context { get; set; }

      public TextWriter MyOut { get; set; }
   }
}