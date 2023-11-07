using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace GuiSyntaxTree.ViewModels
{
   public class SyntaxNodeViewModel
   {
      public SyntaxNode SyntaxNode { get; set; }

      public SyntaxNodeViewModel(SyntaxNode syntaxNode)
      {
         SyntaxNode = syntaxNode;
      }

      public IEnumerable<SyntaxNodeViewModel> Children
         => SyntaxNode.ChildNodes().Select(node => new SyntaxNodeViewModel(node));

      public IEnumerable<SyntaxTokenViewModel> Tokens
         => SyntaxNode.ChildTokens().Select(token => new SyntaxTokenViewModel(token));

      public string TypeName => SyntaxNode.GetType().Name;

      public IEnumerable<SyntaxTriviaViewModel> Trivia
      {
         get
         {
            var leadingTrivia = SyntaxNode.GetLeadingTrivia().Select(trivia => new SyntaxTriviaViewModel(TriviaKind.Leading, trivia));
            var trailingTrivia = SyntaxNode.GetTrailingTrivia().Select(trivia=>new SyntaxTriviaViewModel(TriviaKind.Trailing, trivia));
            return leadingTrivia.Union(trailingTrivia);
         }
      }
   }
}