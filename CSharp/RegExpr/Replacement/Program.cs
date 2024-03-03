using System.Text.RegularExpressions;

namespace Replacement;

internal static class Program
{
   private static void Main()
   {
      Splitting();
      UsingMatchEvaluator();
      UpdatingXmlTag();
      ReferenceToOrigMatch();
      SimpleReplacement();
   }

   private static void Splitting()
   {
      foreach (var s in Regex.Split("a5b7c", @"\d"))
      {
         Console.Write(s + " ");
      }

      Console.WriteLine();

      foreach (var s in Regex.Split("oneTwoThree", @"(?=[A-Z])"))
      {
         Console.Write(s + " ");
      }

      Console.WriteLine();
   }

   private static void UsingMatchEvaluator()
   {
      var replaced = Regex.Replace(
         "5 is less than 10",
         @"\d+",
         m => (int.Parse(m.Value) * 10).ToString()
      );
      Console.WriteLine(replaced);
      Console.WriteLine();
   }

   private static void UpdatingXmlTag()
   {
      var regFind =
         @"<(?'tag'\w+?).*>" + // match first tag, and name it 'tag'
         @"(?'text'.*?)" + // match text content, name it 'text'
         @"</\k'tag'>"; // match last tag, denoted by 'tag'

      var regReplace =
         @"<${tag}" + // <tag
         @" value=""" + // value="
         @"${text}" + // text
         @"""/>"; // "/>

      var replaced = Regex.Replace("<msg>hello</msg>", regFind, regReplace);
      Console.WriteLine(replaced);
      Console.WriteLine();
   }

   private static void ReferenceToOrigMatch()
   {
      var text = "10 plus 20 makes 30";
      var replaced = Regex.Replace(text, @"\d+", @"<$0>");
      Console.WriteLine(replaced);
      Console.WriteLine();
   }

   private static void SimpleReplacement()
   {
      var find = @"\bcat\b";
      var replace = "dog";
      var replaced = Regex.Replace("catapult the cat", find, replace);
      Console.WriteLine(replaced);
      Console.WriteLine();
   }
}