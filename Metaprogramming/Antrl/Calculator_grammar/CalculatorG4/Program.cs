/*
 * Sample using of ANTLR G4 grammar in .NET context
 */

using System;
using Antlr4.Runtime;

namespace CalculatorG4
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         var program = new Program();

         try
         {
            var input = program.GetInput();
            var result = program.EvaluateInput(input);

            program.DisplayResult(result);
         }
         catch (Exception ex)
         {
            program.DisplayError(ex);
         }

         Console.Write($"{Environment.NewLine}Press ENTER to exit: ");
         Console.ReadKey();
      }

      private string GetInput()
      {
         Console.Write("Enter a value to evaluate: ");
         return Console.ReadLine();
      }

      private int EvaluateInput(string input)
      {
         var lexer = new CalculatorLexer(new AntlrInputStream(input));

         lexer.RemoveErrorListeners();
         lexer.AddErrorListener(new ThrowingErrorListener<int>());

         var parser = new CalculatorParser(new CommonTokenStream(lexer));

         parser.RemoveErrorListeners();
         parser.AddErrorListener(new ThrowingErrorListener<IToken>());

         return new CalculatorVisitor().Visit(parser.expression());
      }

      private void DisplayResult(int result)
      {
         Console.WriteLine($"Result: {result}");
      }

      private void DisplayError(Exception ex)
      {
         Console.WriteLine("Something didn't go as expected:");
         Console.WriteLine(ex.Message);
      }
   }
}