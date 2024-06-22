namespace StaticStrategy;

using static Console;

internal static class Program
{
   private static readonly string[] Items = ["foo", "bar", "baz"];

   private static void Main()
   {
      var tp = new TextProcessor<MarkdownListStrategy>();
      tp.AppendList(Items);
      WriteLine(tp);

      var tp2 = new TextProcessor<HtmlListStrategy>();
      tp2.AppendList(Items);
      WriteLine(tp2);
   }
}