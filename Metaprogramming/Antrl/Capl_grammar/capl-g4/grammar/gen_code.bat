antlr4 -Dlanguage=CSharp CaplLexer.g4 -o CaplGrammar.Core -package CaplGrammar.Core -Xlog
antlr4 -Dlanguage=CSharp CaplParser.g4 -visitor -o CaplGrammar.Core -package CaplGrammar.Core -Xlog
pause

javac *.java
pause

grun Capl primaryExpression -token
@rem input there
pause

grun Capl primaryExpression -tree
@rem input there
pause

grun Capl primaryExpression -gui
@rem input there