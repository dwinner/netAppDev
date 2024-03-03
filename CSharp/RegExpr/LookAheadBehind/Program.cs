using System.Text.RegularExpressions;

namespace LookAheadBehind;

internal static class Program
{
   private static void Main()
   {
      WordBoundaries();
      EmptyLines();
      HandlingEol();
      Anchors();
      LookBehind();
      LookAhead();
   }

   private static void WordBoundaries()
   {
      foreach (Match m in Regex.Matches("Wedding in Sarajevo", @"\b\w+\b"))
      {
         Console.WriteLine(m.Value);
      }

      Console.WriteLine(Regex.Matches("Wedding in Sarajevo", @"\bin\b").Count);
      Console.WriteLine(Regex.Matches("Wedding in Sarajevo", @"in").Count);

      var text = "Don't loose (sic) your cool";
      Console.WriteLine(Regex.Match(text, @"\b\w+\b\s(?=\(sic\))").Value);

      Console.WriteLine();
   }

   private static void EmptyLines()
   {
      const string s = """
                       The
                       second to last line

                       has some

                       spaces
                       	 
                       in it!
                       """;

      var emptyLines = Regex.Matches(s, "^(?=\r?$)", RegexOptions.Multiline);
      Console.WriteLine(emptyLines.Count);

      var blankLines = Regex.Matches(s, "^[ \t]*(?=\r?$)", RegexOptions.Multiline);
      Console.WriteLine(blankLines.Count);

      Console.WriteLine();
   }

   private static void HandlingEol()
   {
      const string fileNames = "a.txt" + "\r\n" + "b.doc" + "\r\n" + "c.txt";
      const string r = @".+\.txt(?=\r?$)";
      foreach (Match m in Regex.Matches(fileNames, r, RegexOptions.Multiline))
      {
         Console.Write($"{m} ");
      }

      Console.WriteLine();
   }

   private static void Anchors()
   {
      Console.WriteLine(Regex.Match("Not now", "^[Nn]o").Value);
      Console.WriteLine(Regex.Match("f = 0.2F", "[Ff]$").Value);
      Console.WriteLine();
   }

   private static void LookBehind()
   {
      const string regex = "(?i)(?<!however.*)good";

      Console.WriteLine(Regex.IsMatch("However good, we...", regex));
      Console.WriteLine(Regex.IsMatch("Very good, thanks!", regex));

      Console.WriteLine();
   }

   private static void LookAhead()
   {
      Console.WriteLine(Regex.Match("say 25 miles more", @"\d+\s(?=miles)").Value);
      Console.WriteLine(Regex.Match("say 25 miles more", @"\d+\s(?=miles).*").Value);

      var password = "blahblah3";
      Console.WriteLine(Regex.IsMatch(password, @"(?=.*\d).{6,}"));

      password = "blahblaha";
      Console.WriteLine(Regex.IsMatch(password, @"(?=.*\d).{6,}"));

      var regex = "(?i)good(?!.*(however|but))";
      Console.WriteLine(Regex.IsMatch("Good work! But...", regex));
      Console.WriteLine(Regex.IsMatch("Good work! Thanks!", regex));

      var fileNames = "a.txt" + "\r\n" + "b.docx" + "\r\n" + "c.txt";
      var r = @".+\.txt(?=\r?$)";
      foreach (Match m in Regex.Matches(fileNames, r, RegexOptions.Multiline))
      {
         Console.Write(m + " ");
      }

      Console.WriteLine();
   }
}