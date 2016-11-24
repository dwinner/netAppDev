using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using Trivia = Microsoft.CodeAnalysis.SyntaxTrivia;

namespace CommentRemover.Extensions
{
   public static class SyntaxNodeExtensions
   {
      private readonly static Func<Trivia, bool> _HasComments =
         trivia => trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) || trivia.IsKind(SyntaxKind.MultiLineCommentTrivia);

      public static async Task<T> RemoveCommentsAsync<T>(this T @this)
         where T : SyntaxNode
         => await Task.FromResult(@this.RemoveComments()).ConfigureAwait(false);

      public static T RemoveComments<T>(this T @this)
         where T : SyntaxNode
      {
         var triviaToRemove = new List<Trivia>();

         var nodesWithComments =
            @this.DescendantNodesAndTokens(node => true)
               .Where(nodeOrToken => nodeOrToken.HasLeadingTrivia && nodeOrToken.GetLeadingTrivia().Any(_HasComments));

         var commentCount = 0;
         foreach (var leadingTrivia
            in nodesWithComments.Select(nodeWithComments => nodeWithComments.GetLeadingTrivia()))
         {
            for (var i = 0; i < leadingTrivia.Count; i++)
            {
               var trivia = leadingTrivia[i];
               if (_HasComments(trivia))
               {
                  triviaToRemove.Add(trivia);
                  commentCount++;

                  if (i > 0)
                  {
                     var precendingTrivia = leadingTrivia[i - 1];
                     if (precendingTrivia.IsKind(SyntaxKind.WhitespaceTrivia))
                     {
                        triviaToRemove.Add(precendingTrivia);
                     }
                  }
               }
            }

            triviaToRemove.AddRange(
               leadingTrivia.Where(trivia => trivia.IsKind(SyntaxKind.EndOfLineTrivia)).Take(commentCount));
         }

         return triviaToRemove.Count > 0
            ? @this.ReplaceTrivia(triviaToRemove, (@old, @new) => new Trivia())
               .WithAdditionalAnnotations(Formatter.Annotation)
            : @this;
      }
   }
}