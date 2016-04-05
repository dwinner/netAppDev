using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using static Microsoft.CodeAnalysis.Accessibility;

namespace SimpleRoslynAnalysis
{
   public static class SymbolExtensions
   {
      public static string GetFullNamespace(this ISymbol aSymbol)
      {
         if (string.IsNullOrEmpty(aSymbol?.ContainingNamespace?.Name))
            return null;

         var restResult = aSymbol.ContainingNamespace.GetFullNamespace();
         var result = aSymbol.ContainingNamespace.Name;
         if (restResult != null)
            result = $"{restResult}{'.'}{result}";

         return result;
      }

      private static string GetFullTypeString(this INamedTypeSymbol aNamedTypeSymbol)
      {
         var result = aNamedTypeSymbol.Name;
         if (aNamedTypeSymbol.TypeArguments.Length <= 0)
            return result;

         result += "<";
         var isFirstIteration = true;
         foreach (var typeArgument in aNamedTypeSymbol.TypeArguments.Cast<INamedTypeSymbol>())
         {
            if (isFirstIteration)
               isFirstIteration = false;
            else
               result += ", ";

            result += typeArgument.GetFullTypeString();
         }

         result += ">";

         return result;
      }

      public static string GetMethodSignature(this IMethodSymbol aMethodSymbol)
      {
         var result = aMethodSymbol.DeclaredAccessibility.AccessorToString();

         // TOREFACTOR: Неудачный вариант сбора информации
         if (aMethodSymbol.IsAsync) result += " async";
         if (aMethodSymbol.IsAbstract) result += " abstract";
         if (aMethodSymbol.IsVirtual) result += " async";
         if (aMethodSymbol.IsStatic) result += " static";
         if (aMethodSymbol.IsOverride) result += " override";
         result += aMethodSymbol.ReturnsVoid
             ? " void"
             : $" {(aMethodSymbol.ReturnType as INamedTypeSymbol).GetFullTypeString()}";
         result += $" {aMethodSymbol.Name}(";

         // TOREFACTOR: Неудачный вариант сбора информации в строку
         var isFirstParameter = true;
         foreach (var parameter in aMethodSymbol.Parameters)
         {
            if (isFirstParameter)
               isFirstParameter = false;
            else
               result += ", ";

            var parameterTypeString = (parameter.Type as INamedTypeSymbol).GetFullTypeString();
            result += parameterTypeString;
            result += $" {parameter.Name}";

            if (parameter.HasExplicitDefaultValue)
               result += $" = {parameter.ExplicitDefaultValue}";
         }

         result += ")";

         return result;
      }

      private static string AccessorToString(this Accessibility anAccessibility)
      {
         // TODO: Снабдить константы в перечислении Accessibility описательными атрибутами
         switch (anAccessibility)
         {
            case NotApplicable:
               return nameof(NotApplicable).ToLower();

            case Private:
               return nameof(Private).ToLower();

            case ProtectedAndInternal:
               return nameof(ProtectedAndInternal).ToLower();

            case Protected:
               return nameof(Protected).ToLower();

            case Internal:
               return nameof(Internal).ToLower();

            case ProtectedOrInternal:
               return nameof(ProtectedOrInternal).ToLower();

            case Public:
               return nameof(Public).ToLower();

            default:
               throw new ArgumentOutOfRangeException(nameof(anAccessibility), anAccessibility, null);
         }
      }

      public static object GetAttributeConstructorValueByParameterName(this AttributeData anAttributeData,
          string anArgumentName)
      {
         var attrCtorParams = anAttributeData.AttributeConstructor.Parameters;
         var parameterSymbol = attrCtorParams.FirstOrDefault(ctorParam => ctorParam.Name == anArgumentName);
         var parameterIndex = attrCtorParams.IndexOf(parameterSymbol);
         var ctorArg = anAttributeData.ConstructorArguments[parameterIndex];

         return ctorArg.Value;
      }
   }
}