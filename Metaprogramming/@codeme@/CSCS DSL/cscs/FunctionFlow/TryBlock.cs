namespace CsCsLang.FunctionFlow
{
   internal class TryBlock : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script) => Interpreter.Instance.ProcessTry(script);
   }
}