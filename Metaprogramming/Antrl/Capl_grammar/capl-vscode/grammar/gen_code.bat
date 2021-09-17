antlr4 Capl.g4 -visitor -o CaplGrammar.Core -package CaplGrammar.Core -Xlog
pause
javac *.java
pause
grun Capl compilationUnit -token
@rem input there
pause
grun Capl compilationUnit -tree
@rem input there
pause
grun Capl compilationUnit -gui
@rem input there