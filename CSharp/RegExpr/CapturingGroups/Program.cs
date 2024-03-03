using System.Text.RegularExpressions;

namespace CapturingGroups;

internal static class Program
{
   private static void Main()
   {
      NamedGroupsXml();
      NamedGroups();
      Groups();
   }

   private static void NamedGroupsXml()
   {
      const string regFind = @"<(?'tag'\w+?).*>" + // match first tag, and name it 'tag'
                             @"(?'text'.*?)" + // match text content, name it 'text'
                             @"</\k'tag'>"; // match last tag, denoted by 'tag'

      var m = Regex.Match("<h1>hello</h1>", regFind);
      Console.WriteLine(m.Groups["tag"].Value);
      Console.WriteLine(m.Groups["text"].Value);

      Console.WriteLine();
   }

   private static void NamedGroups()
   {
      var regEx =
         @"\b" + // word boundary
         @"(?'letter'\w)" + // match first letter, and name it 'letter'
         @"\w+" + // match middle letters
         @"\k'letter'" + // match last letter, denoted by 'letter'
         @"\b"; // word boundary

      foreach (Match m in Regex.Matches("bob pope peep", regEx))
      {
         Console.Write(m + " ");
      }

      Console.WriteLine();
   }

   private static void Groups()
   {
      var m = Regex.Match("206-465-1918", @"(\d{3})-(\d{3}-\d{4})");

      Console.WriteLine(m.Groups[0].Value);
      Console.WriteLine(m.Groups[1].Value);
      Console.WriteLine(m.Groups[2].Value);
      foreach (Match ma in Regex.Matches("pop pope peep", @"\b(\w)\w+\1\b"))
      {
         Console.Write(ma + " ");
      }

      Console.WriteLine();
   }
}