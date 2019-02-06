/*
 Property:
    protected override bool HandlesScrolling => base.HandlesScrolling;
*/

PropertyDeclaration(
    IdentifierName(
        Identifier(
            TriviaList(),
            "ReturnType",
            TriviaList(Space)
        )
    ),
    Identifier(
        TriviaList(),
        "PropertyName",
        TriviaList(Space)
    )
)
.WithModifiers(
    TokenList(
        new []
        {
            Token(
                TriviaList(new [] { LineFeed, Whitespace("    ") }),
                SyntaxKind.ProtectedKeyword,
                TriviaList(Space)
            ),
            Token(
                TriviaList(),
                SyntaxKind.OverrideKeyword,
                TriviaList(Space)
            )
        }
    )
)
.WithExpressionBody(
    ArrowExpressionClause(
        MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            BaseExpression(),
            IdentifierName("PropertyName")
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
)

/*
Method:
    protected override ReturnType MethodName(ParameterType1 parameter1, ParameterType2 parameter2, ParameterType3 parameter3)
    {
        return base.MethodName(parameter1, parameter2, parameter3);
    }
*/

MethodDeclaration(
    IdentifierName(Identifier(TriviaList(), "ReturnType", TriviaList(Space))),
    Identifier("MethodName")
)
.WithModifiers(
    TokenList(
        new []
        {
            Token(
                TriviaList(new []{LineFeed, Whitespace("    ")}),
                SyntaxKind.ProtectedKeyword,
                TriviaList(Space)
            ),
            Token(
                TriviaList(),
                SyntaxKind.OverrideKeyword,
                TriviaList(Space)
            )
        }
    )
)
.WithParameterList(
    ParameterList(
        SeparatedList<ParameterSyntax>(
            new SyntaxNodeOrToken[]
            {
                Parameter(Identifier("parameter1"))
                    .WithType(
                        IdentifierName(
                            Identifier(TriviaList(), "ParameterType1", TriviaList(Space))
                        )
                    ),
                Token(TriviaList(), SyntaxKind.CommaToken, TriviaList(Space)),
                Parameter(Identifier("parameter2"))
                    .WithType(
                        IdentifierName(
                            Identifier(TriviaList(), "ParameterType2", TriviaList(Space))
                        )
                    ),
                Token(TriviaList(), SyntaxKind.CommaToken, TriviaList(Space)),
                Parameter(Identifier("parameter3"))
                    .WithType(
                        IdentifierName(
                            Identifier(TriviaList(), "ParameterType3", TriviaList(Space))
                        )
                    )
            }
        )
    )
    .WithCloseParenToken(
        Token(
            TriviaList(),
            SyntaxKind.CloseParenToken,
            TriviaList(LineFeed)
        )
    )
)
.WithBody(
    Block(
        SingletonList<StatementSyntax>(
            ReturnStatement(
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        BaseExpression(),
                        IdentifierName("MethodName")
                    )
                )
                .WithArgumentList(
                    ArgumentList(
                        SeparatedList<ArgumentSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                Argument(
                                    IdentifierName("parameter1")
                                ),
                                Token(TriviaList(), SyntaxKind.CommaToken, TriviaList(Space)),
                                Argument(
                                    IdentifierName("parameter2")
                                ),
                                Token(TriviaList(), SyntaxKind.CommaToken, TriviaList(Space)),
                                Argument(
                                    IdentifierName("parameter3")
                                )
                            }
                        )
                    )
                )
            )
            .WithReturnKeyword(
                Token(
                    TriviaList(Whitespace("        ")),
                    SyntaxKind.ReturnKeyword,
                    TriviaList(Space)
                )
            )
            .WithSemicolonToken(
                Token(
                    TriviaList(),
                    SyntaxKind.SemicolonToken,
                    TriviaList(LineFeed)
                )
            )
        )
    )
    .WithOpenBraceToken(
        Token(
            TriviaList(Whitespace("    ")),
            SyntaxKind.OpenBraceToken,
            TriviaList(LineFeed)
        )
    )
    .WithCloseBraceToken(
        Token(
            TriviaList(Whitespace("    ")),
            SyntaxKind.CloseBraceToken,
            TriviaList(LineFeed)
        )
    )
)