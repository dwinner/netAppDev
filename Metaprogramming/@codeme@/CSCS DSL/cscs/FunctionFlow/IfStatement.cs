namespace CsCsLang.FunctionFlow
{
   internal class IfStatement : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var result = Interpreter.Instance.ProcessIf(script);
         return result;
      }
   }
}