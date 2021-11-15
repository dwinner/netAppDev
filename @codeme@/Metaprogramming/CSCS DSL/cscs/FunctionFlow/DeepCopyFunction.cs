namespace CsCsLang.FunctionFlow
{
   internal class DeepCopyFunction : ActionFunction
   {
      protected override Variable Evaluate(ParsingScript script)
      {
         var varValue = Utils.GetItem(script);
         return varValue.DeepClone();
      }
   }
}