using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GatheringOverrides
{
   /// <summary>
   ///    Type symbol extensions
   /// </summary>
   public static class TypeSymbolExtensions
   {
      private static readonly Dictionary<ITypeSymbol, List<ITypeSymbol>> BaseTypeCache =
         new Dictionary<ITypeSymbol, List<ITypeSymbol>>();

      /// <summary>
      ///    Возвращает список базовых типов для типа. В список включен и изначальный тип
      /// </summary>
      /// <param name="typeSymbol">Type Symbol</param>
      /// <returns>Base types</returns>
      public static IEnumerable<ITypeSymbol> GetBaseTypesAndSelf(this ITypeSymbol typeSymbol)
      {
         if (BaseTypeCache.ContainsKey(typeSymbol))
            return BaseTypeCache[typeSymbol];

         var typeSymbols = new List<ITypeSymbol>();
         CollectAllTypeSymbols(typeSymbols, typeSymbol);
         typeSymbols = typeSymbols.Distinct(EqualityComparer<ITypeSymbol>.Default).ToList();
         BaseTypeCache[typeSymbol] = typeSymbols;

         return typeSymbols;
      }

      /// <summary>
      ///    Рекурсивно обходит базовые типы и записывает их в <paramref name="typeSymbols" />
      /// </summary>
      /// <param name="typeSymbols">Type symbols</param>
      /// <param name="typeSymbol">Type symbol</param>
      private static void CollectAllTypeSymbols(List<ITypeSymbol> typeSymbols, ITypeSymbol typeSymbol)
      {
         while (true)
         {
            if (typeSymbol == null)
               return;

            typeSymbols.Add(typeSymbol);
            if (typeSymbol.Interfaces != null)
            {
               typeSymbols.AddRange(typeSymbol.Interfaces);
               foreach (var currentInterface in typeSymbol.Interfaces)
                  CollectAllTypeSymbols(typeSymbols, currentInterface);
            }

            typeSymbol = typeSymbol.BaseType;
         }
      }
   }
}