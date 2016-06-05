using Microsoft.CodeAnalysis;

namespace GuiSyntaxTree.ViewModels
{
   public class SyntaxTriviaViewModel
   {
      public TriviaKind Kind { get; }
      public SyntaxTrivia SyntaxTrivia { get; }

      public SyntaxTriviaViewModel(TriviaKind kind, SyntaxTrivia syntaxTrivia)
      {
         Kind = kind;
         SyntaxTrivia = syntaxTrivia;
      }

      public override string ToString()
         => $"{Kind}, Start : {SyntaxTrivia.Span.Start}, Lenght: {SyntaxTrivia.Span.Length} : {SyntaxTrivia}";
   }
}