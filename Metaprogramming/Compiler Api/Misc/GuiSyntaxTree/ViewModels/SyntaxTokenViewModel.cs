using Microsoft.CodeAnalysis;

namespace GuiSyntaxTree.ViewModels
{
   public class SyntaxTokenViewModel
   {
      public SyntaxToken SyntaxToken { get; }

      public SyntaxTokenViewModel(SyntaxToken syntaxToken)
      {
         SyntaxToken = syntaxToken;
      }

      public string TypeName => SyntaxToken.GetType().Name;

      public override string ToString() => SyntaxToken.ToString();
   }
}