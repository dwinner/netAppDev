namespace Interpreter
{
   public interface IExpression
   {
      void Interpret(IContext context);
   }
}