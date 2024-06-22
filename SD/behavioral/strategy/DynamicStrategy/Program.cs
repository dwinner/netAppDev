namespace DynamicStrategy;

using static Console;

internal static class Program
{
   private static readonly string[] Items = ["foo", "bar", "baz"];

   private static void Main()
   {
      var textProcessor = new TextProcessor();
      textProcessor.SetOutputFormat(OutputFormat.Markdown);
      textProcessor.AppendList(Items);
      WriteLine(textProcessor);

      textProcessor.Clear();
      textProcessor.SetOutputFormat(OutputFormat.Html);
      textProcessor.AppendList(Items);
      WriteLine(textProcessor);
   }
}