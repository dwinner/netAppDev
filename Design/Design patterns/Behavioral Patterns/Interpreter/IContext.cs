namespace Interpreter
{
   public interface IContext
   {
      object Get(object key);

      void AddVariable(object key, object value);
   }
}