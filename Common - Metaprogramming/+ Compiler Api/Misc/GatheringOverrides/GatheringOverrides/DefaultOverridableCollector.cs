using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static GatheringOverrides.CodeGeneration;
using static GatheringOverrides.SymbolExtensions;

namespace GatheringOverrides
{
   /// <summary>
   ///    Default implementation for <see cref="IOverridableCollector" />
   /// </summary>
   internal sealed class DefaultOverridableCollector : IOverridableCollector
   {
      private const int DefaultCapacity = 0x400;
      private readonly ITypeSymbol _declaredTypeSymbol;

      public DefaultOverridableCollector(BaseTypeDeclarationSyntax targetType, SemanticModel model)
      {
         _declaredTypeSymbol = model.GetDeclaredSymbol(targetType) as ITypeSymbol;
         if (_declaredTypeSymbol == null)
         {
            throw new ArgumentException("Desired type is not a declaration node", nameof(targetType));
         }
      }

      /// <inheritdoc />
      public IEnumerable<MemberEntry> GatherProperties(TypeHierarchyFilter filter, string indentation)
      {
         var baseTypes = _declaredTypeSymbol.GetBaseTypes(filter);
         var accessibleToOverride = GetOverridableSymbols(baseTypes);
         var overridableProperties = GetOverridableProperties(accessibleToOverride);
         var properties = new List<MemberEntry>(DefaultCapacity);
         properties.AddRange(
            from propertySymbol in overridableProperties
            let signature = propertySymbol.ToSignature()
            let summary = propertySymbol.GetSummary()
            let deferredCode =
               new Lazy<MemberDeclarationSyntax>(() => BuildOverridableProperty(propertySymbol, indentation))
            select new MemberEntry
            {
               Signature = signature,
               Summary = summary,
               Code = deferredCode
            });

         return properties;
      }

      /// <inheritdoc />
      public IEnumerable<MemberEntry> GatherMethods(TypeHierarchyFilter filter, string indentation)
      {
         var baseTypes = _declaredTypeSymbol.GetBaseTypes(filter);
         var accessibleToOverride = GetOverridableSymbols(baseTypes);
         var overridableMethods = GetOverridableMethods(accessibleToOverride);
         var methods = new List<MemberEntry>(DefaultCapacity);
         methods.AddRange(
            from methodSymbol in overridableMethods
            let signature = methodSymbol.ToSignature()
            let summary = methodSymbol.GetSummary()
            let deferredCode =
               new Lazy<MemberDeclarationSyntax>(() => BuildOverridableMethod(methodSymbol, indentation))
            select new MemberEntry
            {
               Signature = signature,
               Summary = summary,
               Code = deferredCode
            });

         return methods;
      }
   }
}