using System.Text.RegularExpressions;

namespace REBasics;

internal static class Program
{
   private static void Main()
   {
      GreedyVsLazy();
      Quantifiers();
      CharacterSets();
      CharacterEscapes();
      RegExOptions();
      Compiled();
      Alternators();
      SimpleQualifier();
      NextMatch();
      Matches();
   }

   private static void Quantifiers()
   {
      Console.WriteLine(Regex.Match("cv15.docx", @"cv\d*\.docx").Success);
      Console.WriteLine(Regex.Match("cvjoint.docx", @"cv.*\.docx").Success);
      Console.WriteLine(Regex.Matches("slow! yeah slooow!", "slo+w").Count);

      var bp = new Regex(@"\d{2,3}/\d{2,3}");
      Console.WriteLine(bp.Match("It used to be 160/110").Value);
      Console.WriteLine(bp.Match("Now it's only 115/75").Value);

      Console.WriteLine();
   }

   private static void GreedyVsLazy()
   {
      var html = "<i>By default</i> quantifiers are <i>greedy</i> creatures";

      foreach (Match m in Regex.Matches(html, @"<i>.*</i>"))
      {
         Console.WriteLine(m.Value);
      }

      var ms = Regex.Matches(html, @"<i>.*?</i>");
      for (var index = 0; index < ms.Count; index++)
      {
         var m = ms[index];
         Console.WriteLine(m.Value);
      }

      Console.WriteLine();
   }

   private static void CharacterSets()
   {
      Console.WriteLine(Regex.Matches("That is that.", "[Tt]hat").Count);
      Console.WriteLine(Regex.Match("quiz qwerty", "q[^aeiou]").Index);
      Console.WriteLine(Regex.Match("b1-c4", @"[a-h]\d-[a-h]\d").Success);
      Console.WriteLine(Regex.IsMatch("Yes, please", @"\p{P}"));

      Console.WriteLine();
   }

   private static void CharacterEscapes()
   {
      //       The Regex metacharacters are as follows:
      //
      //       \  *  +  ?  |  {  [  (  )  ^  $  .  #

      Console.WriteLine(Regex.Match("what?", @"what\?").Value);
      Console.WriteLine(Regex.Match("what?", @"what?").Value);

      Console.WriteLine(Regex.Escape(@"?"));
      Console.WriteLine(Regex.Unescape(@"\?"));

      Console.WriteLine(Regex.IsMatch("hello world", @"hello world"));
      Console.WriteLine(Regex.IsMatch("hello world", @"(?x) hello world"));

      Console.WriteLine();
   }

   private static void RegExOptions()
   {
      Console.WriteLine(Regex.Match("a", "A", RegexOptions.IgnoreCase).Value);
      Console.WriteLine(Regex.Match("a", @"(?i)A").Value);
      Console.WriteLine(Regex.Match("AAAa", @"(?i)a(?-i)a").Value);
      Console.WriteLine();
   }

   private static void Compiled()
   {
      var r = new Regex(@"sausages?", RegexOptions.Compiled);

      Console.WriteLine(r.Match("sausage").Success);
      Console.WriteLine(r.Match("sausages").Success);
      Console.WriteLine();
   }

   private static void Alternators()
   {
      var r = "Jen(ny|nifer)?";
      Console.WriteLine(Regex.IsMatch("Jenny", r));
      Console.WriteLine(Regex.IsMatch("Jennifer", r));
      Console.WriteLine(Regex.IsMatch("Jen", r));
      Console.WriteLine(Regex.IsMatch("Ben", r));
      Console.WriteLine();
   }

   private static void SimpleQualifier()
   {
      Console.WriteLine(Regex.Match("color", @"colou?r").Success);
      Console.WriteLine(Regex.Match("colour", @"colou?r").Success);
      Console.WriteLine(Regex.Match("colouur", @"colou?r").Success);
      Console.WriteLine();
   }

   private static void NextMatch()
   {
      var m1 = Regex.Match("One color? There are two colours in my head!", @"colou?rs?");
      var m2 = m1.NextMatch();

      Console.WriteLine(m1);
      Console.WriteLine(m2);
      Console.WriteLine();
   }

   private static void Matches()
   {
      foreach (Match match in Regex.Matches("One color? There are two colours in my head!", @"colou?rs?"))
      {
         Console.WriteLine(match);
      }

      Console.WriteLine();
   }
}