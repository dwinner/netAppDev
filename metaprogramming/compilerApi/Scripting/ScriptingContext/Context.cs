namespace ScriptingContext
{
   public sealed class Context
   {
      public Context(int value)
      {
         Value = value;
      }

      public int Value { get; }
   }
}