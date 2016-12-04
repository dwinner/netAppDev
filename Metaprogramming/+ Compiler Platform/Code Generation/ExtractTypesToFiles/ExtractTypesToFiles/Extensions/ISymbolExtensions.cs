using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace ExtractTypesToFiles.Extensions
{
   public static class SymbolExtensions
   {
      public static string GetContainingNamespace(this ISymbol @this)
      {
         var namespaceParts = new List<string>();
         var containingNamespace = @this.ContainingNamespace;

         while (containingNamespace != null)
         {
            if (!string.IsNullOrWhiteSpace(containingNamespace.Name))
            {
               namespaceParts.Insert(0, containingNamespace.Name);
            }

            containingNamespace = containingNamespace.ContainingNamespace;
         }

         return string.Join(".", namespaceParts);
      }
   }
}