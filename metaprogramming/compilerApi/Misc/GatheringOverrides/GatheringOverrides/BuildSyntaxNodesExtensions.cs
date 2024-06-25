using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static GatheringOverrides.CodeGeneration;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace GatheringOverrides
{
   internal static class BuildSyntaxNodesExtensions
   {
      internal static ParameterSyntax DecorateWithReturnType(this ParameterSyntax parameterSyntax,
         ITypeSymbol parameterType, IParameterSymbol parameterSymbol)
      {
         var parameterReturnType = parameterType.SimplifyTypeName();
         parameterSyntax = parameterType is INamedTypeSymbol namedTypeSymbol
                           && namedTypeSymbol.IsGenericType
            ? parameterSyntax.DecorateWithGeneric(namedTypeSymbol, parameterReturnType)
            : parameterSyntax.DecorateWithQualifiers(parameterType, parameterSymbol, parameterReturnType);

         return parameterSyntax;
      }

      internal static ParameterSyntax DecorateWithModifiers(this ParameterSyntax @this,
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
         /* TOREFACTOR: There must be developed the common recursice way to handle
          nested generic parameters, like Tuple<int, Tuple<int, int>> etc... */

         var typeArguments = namedTypeSymbol.TypeArguments;
         var genericParameterNodes = new List<SyntaxNodeOrToken>(typeArguments.Length * 2);
         for (var genArgIdx = 0; genArgIdx < typeArguments.Length; genArgIdx++)
         {
            var typeArg = typeArguments[genArgIdx];
            var qualifiedByTypeNode = typeArg.GetQualifiedByTypeNode();
            genericParameterNodes.Add(qualifiedByTypeNode);
            if (genArgIdx != typeArguments.Length - 1)
            {
               genericParameterNodes.Add(
                  SyntaxKind.CommaToken.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}));
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

      internal static IEnumerable<SyntaxNodeOrToken> GetRankTokens(int rank)
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
               @this = @this.WithType(IdentifierName(returnType.ToIdentifier()));
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

      internal static ArgumentSyntax DecorateWithModifiers(this ArgumentSyntax @this,
         IParameterSymbol parameterSymbol)
      {
         switch (parameterSymbol.RefKind)
         {
            case RefKind.None:
               break;

            case RefKind.Ref:
               @this =
                  @this.WithRefKindKeyword(
                     SyntaxKind.RefKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}));
               break;

            case RefKind.Out:
               @this =
                  @this.WithRefKindKeyword(
                     SyntaxKind.OutKeyword.BuildToken(Array.Empty<SyntaxTrivia>(), new[] {Space}));
               break;

            case RefKind.In:
               break;

            default:
               throw new ArgumentOutOfRangeException();
         }

         return @this;
      }

      internal static BaseExpressionSyntax BuildBaseExpr(this IMethodSymbol @this, string indentation)
      {
         var baseExpression = BaseExpression();
         if (@this.ReturnsVoid)
         {
            baseExpression = baseExpression
               .WithToken(SyntaxKind.BaseKeyword.BuildToken(new[] {Whitespace(indentation)},
                  Array.Empty<SyntaxTrivia>()));
         }

         return baseExpression;
      }

      internal static InvocationExpressionSyntax BuildBaseInvocationExpr(this IMethodSymbol @this,
         BaseExpressionSyntax baseExpr, string methodName)
      {
         InvocationExpressionSyntax invocationExpr;
         if (@this.IsGenericMethod)
         {
            var typeParameters = @this.TypeParameters;
            if (typeParameters.Length > 0)
            {
               var typeParameterList = BuildGeneric(typeParameters, IdentifierName);
               var typeArgumentList = TypeArgumentList(SeparatedList<TypeSyntax>(typeParameterList));
               invocationExpr = InvocationExpression(
                  MemberAccessExpression(
                     SyntaxKind.SimpleMemberAccessExpression,
                     baseExpr,
                     GenericName(Identifier(methodName))
                        .WithTypeArgumentList(typeArgumentList)));
            }
            else
            {
               invocationExpr = InvocationExpression(
                  MemberAccessExpression(
                     SyntaxKind.SimpleMemberAccessExpression,
                     baseExpr,
                     GenericName(Identifier(methodName))));
            }
         }
         else
         {
            invocationExpr = InvocationExpression(
               MemberAccessExpression(
                  SyntaxKind.SimpleMemberAccessExpression,
                  baseExpr,
                  IdentifierName(methodName)));
         }

         return invocationExpr;
      }
   }
}