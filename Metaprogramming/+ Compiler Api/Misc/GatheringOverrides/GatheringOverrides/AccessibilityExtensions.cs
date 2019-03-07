using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   public static class AccessibilityExtensions
   {
      public static IList<SyntaxToken> GetModifierTokens(this Accessibility accessibility, string indentation)
      {
         var tokens = new List<SyntaxToken>();

         switch (accessibility)
         {
            case Accessibility.NotApplicable:
               break;

            case Accessibility.Private:
               tokens.Add(SyntaxKind.PrivateKeyword.BuildToken(new[] {Whitespace(indentation)}, new[] {Space}));
               break;

            case Accessibility.ProtectedAndInternal:
               tokens.AddRange(new[]
               {
                  SyntaxKind.PrivateKeyword.BuildToken(new[] {Whitespace(indentation)}, new[] {Space}),
                  SyntaxKind.ProtectedKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})
               });
               break;

            case Accessibility.Protected:
               tokens.Add(SyntaxKind.ProtectedKeyword.BuildToken(new[] {Whitespace(indentation)}, new[] {Space}));
               break;

            case Accessibility.Internal:
               tokens.Add(SyntaxKind.InternalKeyword.BuildToken(new[] {Whitespace(indentation)}, new[] {Space}));
               break;

            case Accessibility.ProtectedOrInternal:
               tokens.AddRange(new[]
               {
                  SyntaxKind.ProtectedKeyword.BuildToken(new[] {Whitespace(indentation)}, new[] {Space}),
                  SyntaxKind.InternalKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})
               });
               break;

            case Accessibility.Public:
               tokens.Add(SyntaxKind.PublicKeyword.BuildToken(new[] {Whitespace(indentation)}, new[] {Space}));
               break;

            default:
               throw new ArgumentOutOfRangeException(nameof(accessibility), accessibility, null);
         }

         return tokens;
      }
   }
}