namespace CsCsLang.FunctionFlow
{
   internal class WhileStatement : ParserFunction
   {
      protected override Variable Evaluate(ParsingScript script) => Interpreter.Instance.ProcessWhile(script);
   }
}