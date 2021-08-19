using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmstrongDsl
{
   internal static class Program
   {
      private static void Main()
      {
         do
         {
            var inputs = Enumerable.Range(1, 10000);
            Console.WriteLine("Armstrong >>");
            var readLine = Console.ReadLine();
            if (readLine != null)
            {
               var line = readLine.Replace("(", "( ").Replace(")", " )");
               var statement = GenerateArmStrongStatement(GetTokens(line));
               var lc = new LinqCompiler(statement);
               lc.ExternalAssemblies.Add(typeof(IntExt).Assembly);
               lc.AddSource("input", inputs);

               Console.WriteLine("Armstrong Query Expressed in Plain English: {0}", line);
               Console.WriteLine("Generated LINQ Query: {0}", statement);
               var answers = lc.Evaluate();
               Console.WriteLine(answers);
            }
         } while (true);
      }

      private static string SanitizeBraces(string generatedStatement)
      {
         var gap = generatedStatement.ToCharArray().Count(c => c == '(') -
                   generatedStatement.ToCharArray().Count(c => c == ')');
         return gap == 0 ? generatedStatement : generatedStatement + new string(')', gap);
      }

      private static List<string> GetTokens(string phrase)
      {
         string[] specialOnes = { "are-same", "digits-at" };
         if (specialOnes.Any(phrase.EndsWith))
         {
            return phrase.Split(new[] { "of", "the", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
         }

         var tokens = new Stack<string>();
         phrase.Split(new[] { "of", "the", " " }, StringSplitOptions.RemoveEmptyEntries)
            .ToList()
            .ForEach(t => tokens.Push(t));

         foreach (var token in tokens)
         {
            Console.WriteLine("Token: {0}", token);
         }

         return tokens.ToList();
      }

      private static string GenerateArmStrongStatement(IEnumerable<string> tokens)
      {
         var mapping = new Dictionary<string, string>
         {
            {"*", "*"},
            {"times", "*"},
            {"(", ")"},
            {")", "("},
            {"are-same", ".IsSame()"},
            {"==", "=="},
            {"proper-divisors", ".ProperDivisors()"},
            {"even-digits", ".EvenDigits()"},
            {"odd-digits", ".OddDigits()"},
            {"number", "n"},
            {"square", ".Square()"},
            {"product", ".Product()"},
            {"is", "=="},
            {"!=", "!="},
            {"+", "+"},
            {"-", "-"},
            {"and", "&&"},
            {"or", "||"},
            {"/", "/"},
            {">", "<"},
            {"<", ">"},
            {"<=", ">="},
            {">=", "<="},
            {"divided-by", "/"},
            {"are", ".Are("},
            {"digits", ".Digits()"},
            {"reverse-digits", ".ReverseDigits()"},
            {"cube", ".Cube()"},
            {"factorial", ".Factorial()"},
            {"sum", ".Sum()"},
            {"average", ".Average()"},
            {"maximum", ".Max()"},
            {"minimum", ".Min()"},
            {"digits-at", ".DigitsAt("}
         };

         var armstrongBuilder = new StringBuilder();
         foreach (var to in tokens)
         {
            if (mapping.ContainsKey(to))
               armstrongBuilder.Append(mapping[to]);
            if (to.ToCharArray().All(t => char.IsNumber(t) || t == '.'))
               armstrongBuilder.Append(to);
         }

         return SanitizeBraces($"input.Where ( n => {armstrongBuilder})");
      }
   }
}