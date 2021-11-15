namespace CsCsLang.FunctionFlow
{
   internal class BoolFunction : ParserFunction
   {
      private readonly bool m_value;

      public BoolFunction(bool init) => m_value = init;

      protected override Variable Evaluate(ParsingScript script) => new Variable(m_value);
   }
}