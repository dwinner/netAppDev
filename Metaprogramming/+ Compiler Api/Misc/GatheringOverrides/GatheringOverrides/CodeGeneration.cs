using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.Accessibility;

namespace GatheringOverrides
{
   public static class CodeGeneration
   {
      private const string DefaultIndentation = "    ";

      public static PropertyDeclarationSyntax BuildOverridableProperty(IPropertySymbol propertySymbol,
         string indentation = DefaultIndentation)
      {
         var propertyName = propertySymbol.Name;
         var propertyType = propertySymbol.Type.NormalizeTypeSymbol();
         var propertyModifiers = propertySymbol.GetAccessModifiersWithOverride(indentation);
         var accessorList = propertySymbol.GetAccessorList(indentation);

         var propertyDecl = PropertyDeclaration(
               IdentifierName(
                  Identifier(
                     TriviaList(),
                     propertyType,
                     TriviaList(Space)
                  )
               ),
               Identifier(
                  TriviaList(),
                  propertyName,
                  TriviaList(Space)
               )
            )
            .WithModifiers(propertyModifiers)
            .WithAccessorList(accessorList);

         return propertyDecl;
      }

      private static AccessorListSyntax GetAccessorList(this IPropertySymbol propertySymbol, string indentation)
      {
         var propertyName = propertySymbol.Name;
         AccessorListSyntax accessorList;
         var propertyAccessors = propertySymbol.DeclaredAccessibility;

         if (propertySymbol.IsReadOnly)
         {
            var getAccessModifiers = propertySymbol.GetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var getAccessorDecl = GetWithBaseCallAccessorDeclaration(getAccessModifiers, propertyName);
            accessorList = AccessorList(
               List(
                  new[]
                  {
                     getAccessorDecl
                  }
               )
            );
         }
         else if (propertySymbol.IsWriteOnly)
         {
            var setAccessModifiers = propertySymbol.SetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var setAccessorDecl = GetWithBaseCallAccessorDeclaration(indentation, setAccessModifiers, propertyName);
            accessorList = AccessorList(
               List(
                  new[]
                  {
                     setAccessorDecl
                  }
               )
            );
         }
         else
         {
            var getAccessModifiers = propertySymbol.GetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var getAccessorDecl = GetWithBaseCallAccessorDeclaration(getAccessModifiers, propertyName);
            var setAccessModifiers = propertySymbol.SetMethod.GenerateAccessModifiers(propertyAccessors, indentation);
            var setAccessorDecl = GetWithBaseCallAccessorDeclaration(string.Empty, setAccessModifiers, propertyName);
            accessorList = AccessorList(
               List(
                  new[]
                  {
                     getAccessorDecl,
                     setAccessorDecl
                  }
               )
            );
         }

         var reduceIndentation = " ".Repeat(indentation.Length / 2);
         return accessorList.WithOpenBraceToken(
               Token(
                  TriviaList(LineFeed, Whitespace(reduceIndentation)),
                  SyntaxKind.OpenBraceToken,
                  TriviaList(LineFeed)
               )
            )
            .WithCloseBraceToken(
               Token(
                  TriviaList(Whitespace(reduceIndentation)),
                  SyntaxKind.CloseBraceToken,
                  TriviaList(LineFeed)
               )
            );
      }

      private static AccessorDeclarationSyntax GetWithBaseCallAccessorDeclaration(string indentation,
         SyntaxTokenList setAccessModifiers, string propertyName)
         =>
            AccessorDeclaration(
                  SyntaxKind.SetAccessorDeclaration
               )
               .WithModifiers(
                  setAccessModifiers
               )
               .WithKeyword(
                  Token(
                     TriviaList(Whitespace($"{indentation}")),
                     SyntaxKind.SetKeyword,
                     TriviaList(Space)
                  )
               )
               .WithExpressionBody(
                  ArrowExpressionClause(
                        AssignmentExpression(
                              SyntaxKind.SimpleAssignmentExpression,
                              MemberAccessExpression(
                                 SyntaxKind.SimpleMemberAccessExpression,
                                 BaseExpression(),
                                 IdentifierName(
                                    Identifier(
                                       TriviaList(),
                                       propertyName,
                                       TriviaList(Space)
                                    )
                                 )
                              ),
                              IdentifierName("value")
                           )
                           .WithOperatorToken(
                              Token(
                                 TriviaList(),
                                 SyntaxKind.EqualsToken,
                                 TriviaList(Space)
                              )
                           )
                     )
                     .WithArrowToken(
                        Token(
                           TriviaList(),
                           SyntaxKind.EqualsGreaterThanToken,
                           TriviaList(Space)
                        )
                     )
               )
               .WithSemicolonToken(
                  Token(
                     TriviaList(),
                     SyntaxKind.SemicolonToken,
                     TriviaList(LineFeed)
                  )
               );

      private static AccessorDeclarationSyntax GetWithBaseCallAccessorDeclaration(SyntaxTokenList getAccessModifiers,
         string propertyName)
         =>
            AccessorDeclaration(
                  SyntaxKind.GetAccessorDeclaration
               )
               .WithModifiers(
                  getAccessModifiers
               )
               .WithKeyword(
                  Token(
                     TriviaList(),
                     SyntaxKind.GetKeyword,
                     TriviaList(Space)
                  )
               )
               .WithExpressionBody(
                  ArrowExpressionClause(
                        MemberAccessExpression(
                           SyntaxKind.SimpleMemberAccessExpression,
                           BaseExpression(),
                           IdentifierName(propertyName)
                        )
                     )
                     .WithArrowToken(
                        Token(
                           TriviaList(),
                           SyntaxKind.EqualsGreaterThanToken,
                           TriviaList(Space)
                        )
                     )
               )
               .WithSemicolonToken(
                  Token(
                     TriviaList(),
                     SyntaxKind.SemicolonToken,
                     TriviaList(LineFeed)
                  )
               );

      private static SyntaxTokenList GenerateAccessModifiers(this ISymbol methodSymbol,
         Accessibility propertyAccessibility, string indentation)
      {
         SyntaxTokenList accessModifiers;
         var leadingTrivia = Whitespace($"{indentation}");
         var trailingTrivia = Space;
         var emptyTokens = TokenList(
            Token(
               TriviaList(leadingTrivia),
               SyntaxKind.None,
               TriviaList(Whitespace(string.Empty))
            ));

         switch (methodSymbol.DeclaredAccessibility)
         {
            case NotApplicable:
               accessModifiers = emptyTokens;
               break;

            case Private
               when propertyAccessibility != Private:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;
            }

            case ProtectedAndInternal
               when propertyAccessibility != ProtectedAndInternal:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(trailingTrivia)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  ));
            }
               break;

            case Protected
               when propertyAccessibility != Protected:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
            }
               break;

            case Internal
               when propertyAccessibility != Internal:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.InternalKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
            }
               break;

            case ProtectedOrInternal
               when propertyAccessibility != ProtectedOrInternal:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.InternalKeyword,
                     TriviaList(trailingTrivia)
                  ));
            }
               break;

            case Public
               when propertyAccessibility != Public:
            {
               accessModifiers = TokenList(
                  Token(
                     TriviaList(leadingTrivia),
                     SyntaxKind.PublicKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
            }
               break;

            default:
               accessModifiers = emptyTokens;
               break;
         }

         return accessModifiers;
      }

      private static SyntaxTokenList GetAccessModifiersWithOverride(this ISymbol symbol, string indentation)
      {
         var accessibility = symbol.DeclaredAccessibility;
         var tokens = new List<SyntaxToken>();
         var indentationTrivia = Whitespace(" ".Repeat(indentation.Length / 2));
         var leadingTrivia = LineFeed;
         var trailingTrivia = Space;

         switch (accessibility)
         {
            case NotApplicable:
               break;

            case Private:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(trailingTrivia)
                  ));
               break;

            case ProtectedAndInternal:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               tokens.Add(
                  Token(
                     TriviaList(leadingTrivia, Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case Protected:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case Internal:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.InternalKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case ProtectedOrInternal:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               tokens.Add(
                  Token(
                     TriviaList(),
                     SyntaxKind.InternalKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            case Public:
               tokens.Add(
                  Token(
                     TriviaList(indentationTrivia),
                     SyntaxKind.PublicKeyword,
                     TriviaList(trailingTrivia)
                  )
               );
               break;

            default:
               throw new ArgumentOutOfRangeException();
         }

         tokens.Add(
            Token(
               TriviaList(),
               SyntaxKind.OverrideKeyword,
               TriviaList(trailingTrivia)
            )
         );

         var tokenCount = tokens.Count;
         var tokensWithIndentation = new SyntaxToken[tokenCount + 1];
         tokensWithIndentation[0] = Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.None,
            TriviaList(LineFeed));
         for (var i = 1; i < tokensWithIndentation.Length; i++)
         {
            tokensWithIndentation[i] = tokens[i - 1];
         }

         var accessTokens = TokenList(tokensWithIndentation);

         return accessTokens;
      }

      public static MethodDeclarationSyntax BuildOverridableMethod(IMethodSymbol methodSymbol,
         string indentation = DefaultIndentation)
      {
         var methodName = methodSymbol.Name;
         var returnType = methodSymbol.ReturnType.NormalizeTypeSymbol();

         var returnsByRef = methodSymbol.ReturnsByRef;
         var returnsByRefReadonly = methodSymbol.ReturnsByRefReadonly;

         var returnTypeIdentifier = IdentifierName(
            Identifier(
               TriviaList(),
               returnType,
               TriviaList(Space)
            )
         );
         var methodIdentifier = Identifier(methodName);
         var methodDecl = !returnsByRefReadonly
            ? returnsByRef
               ? MethodDeclaration(RefType(returnTypeIdentifier)
                  .WithRefKeyword(GetRefToken()), methodIdentifier)
               : MethodDeclaration(returnTypeIdentifier, methodIdentifier)
            : MethodDeclaration(
               RefType(returnTypeIdentifier)
                  .WithRefKeyword(GetRefToken())
                  .WithReadOnlyKeyword(GetReadonlyToken()),
               methodIdentifier);

         var methodModifiers = methodSymbol.GetOverridableMethodModifiers(indentation);
         methodDecl = methodDecl.WithModifiers(methodModifiers);

         if (methodSymbol.IsGenericMethod)
         {
            var typeParameters = methodSymbol.TypeParameters;
            if (typeParameters.Length > 0)
            {
               var typeParameterList = BuildGenericParameters(typeParameters);
               var genericParametersSyntax = TypeParameterList(SeparatedList<TypeParameterSyntax>(typeParameterList));
               methodDecl = methodDecl.WithTypeParameterList(genericParametersSyntax);
            }
         }

         var parameters = methodSymbol.Parameters;
         var parameterListSyntax = parameters.Length > 0
            ? ParameterList(SeparatedList<ParameterSyntax>(GetParameterNodes(parameters)))
            : ParameterList();

         parameterListSyntax = parameterListSyntax.WithCloseParenToken(GetCloseParenToken());
         methodDecl = methodDecl.WithParameterList(parameterListSyntax);

         return methodDecl;
      }

      private static IEnumerable<SyntaxNodeOrToken> GetParameterNodes(ImmutableArray<IParameterSymbol> parameters)
      {
         var parameterNodes = new List<SyntaxNodeOrToken>(parameters.Length * 2);
         for (var paramIdx = 0; paramIdx < parameters.Length; paramIdx++)
         {
            var parameterSymbol = parameters[paramIdx];
            var parameterName = parameterSymbol.Name;
            var parameterType = parameterSymbol.Type;
            var parameterSyntax = Parameter(Identifier(parameterName))
               .DecorateWithModifiers(parameterSymbol)
               .DecorateWithReturnType(parameterType, parameterSymbol);
            parameterNodes.Add(parameterSyntax);
            if (paramIdx != parameters.Length - 1)
            {
               parameterNodes.Add(GetCommaToken());
            }
         }

         return parameterNodes;
      }

      private static ParameterSyntax DecorateWithReturnType(this ParameterSyntax parameterSyntax,
         ITypeSymbol parameterType, IParameterSymbol parameterSymbol)
      {
         var parameterReturnType = parameterType.NormalizeTypeSymbol();
         parameterSyntax = parameterType is INamedTypeSymbol namedTypeSymbol
                           && namedTypeSymbol.IsGenericType
            ? parameterSyntax.HandleGenericParameter(namedTypeSymbol, parameterReturnType)
            : parameterSyntax.HandleParameter(parameterType, parameterSymbol, parameterReturnType);

         return parameterSyntax;
      }

      private static ParameterSyntax HandleParameter(this ParameterSyntax parameterSyntax,
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
               parameterSyntax = parameterSyntax.WithType(
                  IdentifierName(
                     Identifier(TriviaList(), returnType, TriviaList(Space)))
               );
               break;

            case TypeKind.Array:
               var arrayTypeSymbol = (IArrayTypeSymbol) parameterType;
               var elementType = arrayTypeSymbol.ElementType.NormalizeTypeSymbol();
               if (parameterSymbol.IsParams)
               {
                  parameterSyntax = parameterSyntax.DecorateWithArray(elementType);
               }
               else
               {
                  var rank = arrayTypeSymbol.Rank;
                  if (rank >= 2)
                  {
                     var rankTokens = GetRankTokens(rank);
                     parameterSyntax = parameterSyntax.WithType(
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
                                    .WithCloseBracketToken(GetCloseBracketToken())
                              )
                           )
                     );
                  }
                  else
                  {
                     parameterSyntax = parameterSyntax.DecorateWithArray(elementType);
                  }
               }

               break;

            case TypeKind.Pointer:
               var pointerTypeSymbol = (IPointerTypeSymbol) parameterType;
               var pointedType = pointerTypeSymbol.PointedAtType.NormalizeTypeSymbol();
               parameterSyntax = parameterSyntax.WithType(
                  PointerType(
                        IdentifierName(Identifier(pointedType))
                     )
                     .WithAsteriskToken(GetAsteriskToken())
               );
               break;

            default:
               throw new ArgumentOutOfRangeException(nameof(parameterTypeKind));
         }

         return parameterSyntax;
      }

      private static ParameterSyntax HandleGenericParameter(this ParameterSyntax parameterSyntax,
         INamedTypeSymbol namedTypeSymbol, string returnType)
      {
         // TODO: There must exist the common way to handle nested generic parameters,
         // like Tuple<int, Tuple<int, int>> etc...

         var typeArguments = namedTypeSymbol.TypeArguments;
         var genericParameterNodes = new List<SyntaxNodeOrToken>(typeArguments.Length * 2);
         for (var genArgIdx = 0; genArgIdx < typeArguments.Length; genArgIdx++)
         {
            var typeArg = typeArguments[genArgIdx];
            var normalizedTypeArg = typeArg.NormalizeTypeSymbol();
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
                  var arrayType = arrayTypeSymbol.ElementType.NormalizeTypeSymbol();
                  var rank = arrayTypeSymbol.Rank;

                  var arrayTypeSyntax = ArrayType(IdentifierName(arrayType));
                  arrayTypeSyntax = rank >= 2
                     ? arrayTypeSyntax.WithRankSpecifiers(
                        SingletonList(
                           ArrayRankSpecifier(
                              SeparatedList<ExpressionSyntax>(
                                 GetRankTokens(rank)
                              )
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
               genericParameterNodes.Add(GetCommaToken());
            }
         }

         parameterSyntax = parameterSyntax.WithType(
            GenericName(returnType)
               .WithTypeArgumentList(
                  TypeArgumentList(
                        SeparatedList<TypeSyntax>(genericParameterNodes)
                     )
                     .WithGreaterThanToken(GetGreaterThanToken())
               )
         );

         return parameterSyntax;
      }

      private static List<SyntaxNodeOrToken> GetRankTokens(int rank)
      {
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

      private static SyntaxToken GetGreaterThanToken() =>
         Token(
            TriviaList(),
            SyntaxKind.GreaterThanToken,
            TriviaList(
               Space
            )
         );

      private static SyntaxToken GetAsteriskToken() =>
         Token(
            TriviaList(),
            SyntaxKind.AsteriskToken,
            TriviaList(Space)
         );

      private static ParameterSyntax DecorateWithArray(this ParameterSyntax parameterSyntax, string elementType)
      {
         parameterSyntax = parameterSyntax.WithType(
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
                        .WithCloseBracketToken(GetCloseBracketToken())
                  )
               )
         );

         return parameterSyntax;
      }

      private static SyntaxToken GetCloseBracketToken() =>
         Token(
            TriviaList(),
            SyntaxKind.CloseBracketToken,
            TriviaList(Space)
         );

      private static SyntaxToken GetCommaToken() =>
         Token(
            TriviaList(),
            SyntaxKind.CommaToken,
            TriviaList(Space));

      private static ParameterSyntax DecorateWithModifiers(this ParameterSyntax parameterSyntax,
         IParameterSymbol parameterSymbol)
      {
         if (parameterSymbol.IsParams)
         {
            parameterSyntax = parameterSyntax.WithModifiers(TokenList(GetParamsToken()));
         }
         else
         {
            var parameterRefKind = parameterSymbol.RefKind;
            switch (parameterRefKind)
            {
               case RefKind.None:
                  break;

               case RefKind.Ref:
                  parameterSyntax = parameterSyntax.WithModifiers(TokenList(GetRefToken()));
                  break;

               case RefKind.Out:
                  parameterSyntax = parameterSyntax.WithModifiers(TokenList(GetOutToken()));
                  break;

               case RefKind.In:
                  break;

               default:
                  throw new ArgumentOutOfRangeException(nameof(parameterRefKind));
            }
         }

         return parameterSyntax;
      }

      private static SyntaxToken GetParamsToken() =>
         Token(
            TriviaList(),
            SyntaxKind.ParamsKeyword,
            TriviaList(Space));

      private static SyntaxToken GetRefToken() =>
         Token(
            TriviaList(),
            SyntaxKind.RefKeyword,
            TriviaList(Space)
         );

      private static SyntaxToken GetCloseParenToken() =>
         Token(
            TriviaList(),
            SyntaxKind.CloseParenToken,
            TriviaList(LineFeed)
         );

      private static IEnumerable<SyntaxNodeOrToken> BuildGenericParameters(
         ImmutableArray<ITypeParameterSymbol> typeParameters)
      {
         var typeParameterList = new List<SyntaxNodeOrToken>(typeParameters.Length * 2);
         for (var paramIdx = 0; paramIdx < typeParameters.Length; paramIdx++)
         {
            var typeParameterSymbol = typeParameters[paramIdx];
            typeParameterList.Add(TypeParameter(Identifier(typeParameterSymbol.Name)));
            if (paramIdx != typeParameters.Length - 1)
            {
               typeParameterList.Add(GetCommaToken());
            }
         }

         return typeParameterList;
      }

      private static SyntaxTokenList GetOverridableMethodModifiers(this IMethodSymbol methodSymbol, string indentation)
      {
         var accessibility = methodSymbol.DeclaredAccessibility;
         var tokens = new List<SyntaxToken>();

         switch (accessibility)
         {
            case NotApplicable:
               break;

            case Private:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case ProtectedAndInternal:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.PrivateKeyword,
                     TriviaList(Space)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case Protected:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case Internal:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.InternalKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case ProtectedOrInternal:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.ProtectedKeyword,
                     TriviaList(Space)
                  ),
                  Token(
                     TriviaList(),
                     SyntaxKind.InternalKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            case Public:
               tokens.AddRange(new[]
               {
                  Token(
                     TriviaList(Whitespace(indentation)),
                     SyntaxKind.PublicKeyword,
                     TriviaList(Space)
                  )
               });
               break;

            default:
               throw new ArgumentOutOfRangeException();
         }

         tokens.Add(Token(
            TriviaList(),
            SyntaxKind.OverrideKeyword,
            TriviaList(Space)
         ));

         var tokenList = TokenList(tokens);

         return tokenList;
      }

      public static SyntaxToken GenerateOpenBrace(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.OpenBraceToken,
            TriviaList(LineFeed)
         );

      public static SyntaxToken GenerateCloseBrace(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.CloseBraceToken,
            TriviaList(LineFeed)
         );

      public static SyntaxToken GenerateReturn(string indentation) =>
         Token(
            TriviaList(Whitespace(indentation)),
            SyntaxKind.ReturnKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GenerateSemicolon() =>
         Token(
            TriviaList(),
            SyntaxKind.SemicolonToken,
            TriviaList(LineFeed)
         );

      public static SyntaxToken GetOutToken() =>
         Token(
            TriviaList(),
            SyntaxKind.OutKeyword,
            TriviaList(Space)
         );

      public static SyntaxToken GetReadonlyToken() =>
         Token(
            TriviaList(),
            SyntaxKind.ReadOnlyKeyword,
            TriviaList(Space)
         );

      public static ArgumentListSyntax GenerateArgumentList(ImmutableArray<IParameterSymbol> parameters) =>
         throw new NotImplementedException();

      public static void GenerateParameterList(ImmutableArray<IParameterSymbol> parameters)
      {
         throw new NotImplementedException();
      }

      public static InvocationExpressionSyntax GenerateBaseInvocation(string identifier,
         ImmutableArray<IParameterSymbol> parameters)
      {
         var invocationExpr = InvocationExpression(
            MemberAccessExpression(
               SyntaxKind.SimpleMemberAccessExpression,
               BaseExpression(),
               IdentifierName(identifier)
            )
         );
         invocationExpr = invocationExpr.WithArgumentList(GenerateArgumentList(parameters));
         return invocationExpr;
      }

      public static RefExpressionSyntax WrapWithRef(ExpressionSyntax expr)
      {
         var refExpr = RefExpression(expr).WithRefKeyword(GetRefToken());
         return refExpr;
      }

      public static ReturnStatementSyntax WrapWithReturn(ExpressionSyntax expr, string indentation)
      {
         var returnStmt = ReturnStatement(expr)
            .WithReturnKeyword(GenerateReturn(indentation))
            .WithSemicolonToken(GenerateSemicolon());
         return returnStmt;
      }

      public static BlockSyntax GenerateMethodBody(ReturnStatementSyntax returnStmt, string indentation)
      {
         var block = Block(SingletonList<StatementSyntax>(returnStmt))
            .WithOpenBraceToken(GenerateOpenBrace(indentation))
            .WithCloseBraceToken(GenerateCloseBrace(indentation));
         return block;
      }
   }
}