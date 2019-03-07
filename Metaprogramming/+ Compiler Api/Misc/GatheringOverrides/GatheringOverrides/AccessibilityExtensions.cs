using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using static GatheringOverrides.TokenGeneration;

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
               tokens.Add(GetPrivateToken(indentation));
               break;

            case Accessibility.ProtectedAndInternal:
               tokens.AddRange(new[]
               {
                  GetPrivateToken(indentation),
                  GetProtectedToken()
               });
               break;

            case Accessibility.Protected:
               tokens.Add(GetProtectedToken(indentation));
               break;

            case Accessibility.Internal:
               tokens.Add(GetInternalToken(indentation));
               break;

            case Accessibility.ProtectedOrInternal:
               tokens.AddRange(new[]
               {
                  GetProtectedToken(indentation),
                  GetInternalToken()
               });
               break;

            case Accessibility.Public:
               tokens.Add(GetPublicToken(indentation));
               break;

            default:
               throw new ArgumentOutOfRangeException(nameof(accessibility), accessibility, null);
         }

         return tokens;
      }
   }
}