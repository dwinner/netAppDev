using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   public static class BuildSyntaxNodesExtensions
   {
      public static ParameterSyntax DecorateWithReturnType(this ParameterSyntax parameterSyntax,
         ITypeSymbol parameterType, IParameterSymbol parameterSymbol)
      {
         var parameterReturnType = parameterType.SimplifyTypeName();
         parameterSyntax = parameterType is INamedTypeSymbol namedTypeSymbol
                           && namedTypeSymbol.IsGenericType
            ? parameterSyntax.DecorateWithGeneric(namedTypeSymbol, parameterReturnType)
            : parameterSyntax.DecorateWithQualifiers(parameterType, parameterSymbol, parameterReturnType);

         return parameterSyntax;
      }

      public static ParameterSyntax DecorateWithModifiers(this ParameterSyntax @this,
         IParameterSymbol parameterSymbol)
      {
         if (parameterSymbol.IsParams)
         {
            @this = @this.WithModifiers(
               TokenList(SyntaxKind.ParamsKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})));
         }
         else
         {
            var parameterRefKind = parameterSymbol.RefKind;
            switch (parameterRefKind)
            {
               case RefKind.None:
                  break;

               case RefKind.Ref:
                  @this = @this.WithModifiers(
                     TokenList(SyntaxKind.RefKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})));
                  break;

               case RefKind.Out:
                  @this = @this.WithModifiers(
                     TokenList(SyntaxKind.OutKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space})));
                  break;

               case RefKind.In:
                  break;

               default:
                  throw new ArgumentOutOfRangeException(nameof(parameterRefKind));
            }
         }

         return @this;
      }

      private static ParameterSyntax DecorateWithArray(this ParameterSyntax @this,
         string elementType)
      {
         @this = @this.WithType(
            ArrayType(
                  IdentifierName(elementType)
               )
               .WithRankSpecifiers(
                  SingletonList(
                     ArrayRankSpecifier(
                           SingletonSeparatedList<ExpressionSyntax>(
                              OmittedArraySizeExpression()
                           )
                        )
                        .WithCloseBracketToken(
                           SyntaxKind.CloseBracketToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}))
                  )
               )
         );

         return @this;
      }

      private static ParameterSyntax DecorateWithGeneric(this ParameterSyntax @this,
         INamedTypeSymbol namedTypeSymbol, string returnType)
      {
         // TOREFACTOR: There must exist the common way to handle nested generic parameters,
         // like Tuple<int, Tuple<int, int>> etc...

         var typeArguments = namedTypeSymbol.TypeArguments;
         var genericParameterNodes = new List<SyntaxNodeOrToken>(typeArguments.Length * 2);
         for (var genArgIdx = 0; genArgIdx < typeArguments.Length; genArgIdx++)
         {
            var typeArg = typeArguments[genArgIdx];
            var normalizedTypeArg = typeArg.SimplifyTypeName();
            switch (typeArg.TypeKind)
            {
               case TypeKind.Unknown:
               case TypeKind.Class:
               case TypeKind.Delegate:
               case TypeKind.Dynamic:
               case TypeKind.Enum:
               case TypeKind.Error:
               case TypeKind.Interface:
               case TypeKind.Module:
               case TypeKind.Struct:
               case TypeKind.TypeParameter:
               case TypeKind.Submission:
                  genericParameterNodes.Add(IdentifierName(normalizedTypeArg));
                  break;

               case TypeKind.Array:
                  var arrayTypeSymbol = (IArrayTypeSymbol) typeArg;
                  var arrayType = arrayTypeSymbol.ElementType.SimplifyTypeName();
                  var rank = arrayTypeSymbol.Rank;

                  var arrayTypeSyntax = ArrayType(IdentifierName(arrayType));
                  arrayTypeSyntax = rank >= 2
                     ? arrayTypeSyntax.WithRankSpecifiers(
                        SingletonList(
                           ArrayRankSpecifier(
                              SeparatedList<ExpressionSyntax>(GetRankTokens(rank))
                           )
                        )
                     )
                     : arrayTypeSyntax.WithRankSpecifiers(
                        SingletonList(
                           ArrayRankSpecifier(
                              SingletonSeparatedList<ExpressionSyntax>(
                                 OmittedArraySizeExpression()
                              )
                           )
                        )
                     );

                  genericParameterNodes.Add(arrayTypeSyntax);
                  break;

               case TypeKind.Pointer:
                  throw new InvalidOperationException("Pointer type cannot be used as type parameter");

               default:
                  throw new ArgumentOutOfRangeException(nameof(typeArg));
            }

            if (genArgIdx != typeArguments.Length - 1)
            {
               genericParameterNodes.Add(SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}));
            }
         }

         @this = @this.WithType(
            GenericName(returnType)
               .WithTypeArgumentList(
                  TypeArgumentList(
                        SeparatedList<TypeSyntax>(genericParameterNodes)
                     )
                     .WithGreaterThanToken(
                        SyntaxKind.GreaterThanToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}))
               )
         );

         return @this;
      }

      private static IEnumerable<SyntaxNodeOrToken> GetRankTokens(int rank)
      {
         if (rank < 2)
         {
            return Enumerable.Empty<SyntaxNodeOrToken>();
         }

         var rankTokens = new List<SyntaxNodeOrToken>();
         for (var rankIndex = 2; rankIndex <= rank; rankIndex++)
         {
            rankTokens.AddRange(new SyntaxNodeOrToken[]
            {
               OmittedArraySizeExpression(),
               Token(SyntaxKind.CommaToken)
            });
            if (rankIndex == rank)
            {
               rankTokens.Add(OmittedArraySizeExpression());
            }
         }

         return rankTokens;
      }

      private static ParameterSyntax DecorateWithQualifiers(this ParameterSyntax @this,
         ITypeSymbol parameterType, IParameterSymbol parameterSymbol, string returnType)
      {
         var parameterTypeKind = parameterType.TypeKind;
         switch (parameterTypeKind)
         {
            case TypeKind.Unknown:
            case TypeKind.Class:
            case TypeKind.Delegate:
            case TypeKind.Dynamic:
            case TypeKind.Enum:
            case TypeKind.Error:
            case TypeKind.Interface:
            case TypeKind.Module:
            case TypeKind.Struct:
            case TypeKind.TypeParameter:
            case TypeKind.Submission:
               @this = @this.WithType(
                  IdentifierName(
                     Identifier(TriviaList(), returnType, TriviaList(Space)))
               );
               break;

            case TypeKind.Array:
               var arrayTypeSymbol = (IArrayTypeSymbol) parameterType;
               var elementType = arrayTypeSymbol.ElementType.SimplifyTypeName();
               if (parameterSymbol.IsParams)
               {
                  @this = @this.DecorateWithArray(elementType);
               }
               else
               {
                  var rank = arrayTypeSymbol.Rank;
                  if (rank >= 2)
                  {
                     var rankTokens = GetRankTokens(rank);
                     @this = @this.WithType(
                        ArrayType(
                              IdentifierName(elementType)
                           )
                           .WithRankSpecifiers(
                              SingletonList(
                                 ArrayRankSpecifier(
                                       SeparatedList<ExpressionSyntax>(
                                          rankTokens
                                       )
                                    )
                                    .WithCloseBracketToken(
                                       SyntaxKind.CloseBracketToken.BuildToken(Array.Empty<SyntaxTrivia>(),
                                          new[] {Space}))
                              )
                           )
                     );
                  }
                  else
                  {
                     @this = @this.DecorateWithArray(elementType);
                  }
               }

               break;

            case TypeKind.Pointer:
               var pointerTypeSymbol = (IPointerTypeSymbol) parameterType;
               var pointedType = pointerTypeSymbol.PointedAtType.SimplifyTypeName();
               @this = @this.WithType(
                  PointerType(
                        IdentifierName(Identifier(pointedType))
                     )
                     .WithAsteriskToken(SyntaxKind.AsteriskToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}))
               );
               break;

            default:
               throw new ArgumentOutOfRangeException(nameof(parameterTypeKind));
         }

         return @this;
      }
   }
}