/** 
 * CAPL grammar implementation.
 */

parser grammar CaplParser;

options {
    tokenVocab = CaplLexer;
}

primaryExpression
    :   Identifier
    |   Constant
    |   StringLiteral+
    |   '(' expression ')'
    |   '(' compoundStatement ')'
    |   (   variableBlock
        |   eventBlock
        |   timerBlock
        |   errorFrame
        |   envBlock
        |   functionDefinition
        |   startBlock
        |   messageBlock
        |   stopMeasurement)*
    ;

startBlock
    : 'on' 'start' '{' blockItemList? '}'
    ;

variableBlock
    :   'variables' '{' blockItemList? '}'
    ;

eventBlock
    :   'on' 'key' (KeyHit|'*') '{' blockItemList? '}'
    ;

timerBlock
    :   'on' 'timer' Identifier ('.' (Identifier|'*'))? '{' blockItemList '}'
    ;

errorFrame  // TODO: this should be case insensitive
    :   'on' ('errorFrame') '{' blockItemList? '}'
    ;

messageBlock
    :   'on' 'message' messageIdentifier '{' blockItemList '}'
    ;

stopMeasurement
    :   'on' 'stopMeasurement' '{' blockItemList '}'
    ;

envBlock
    :   'on' 'envVar' Identifier '{' blockItemList '}'
    ;

postfixExpression
    :   (   primaryExpression
    |   '(' typeName ')' '{' initializerList ','? '}')
    ('[' expression ']'
    | '(' argumentExpressionList? ')'
    | ('++' | '--'))*
    ;

argumentExpressionList
    :   assignmentExpression (',' assignmentExpression)*
    ;

unaryExpression
    :   ('++' |  '--')* (postfixExpression | unaryOperator castExpression)
    ;

unaryOperator
    :   '+' | '-' | '~' | '!'
    ;

castExpression
    :   '(' typeName ')' castExpression
    |   unaryExpression
    |   DigitSequence // for
    ;

multiplicativeExpression
    :   castExpression (('*'|'/'|'%') castExpression)*
    ;

additiveExpression
    :   multiplicativeExpression (('+'|'-') multiplicativeExpression)*
    ;

shiftExpression
    :   additiveExpression (('<<'|'>>') additiveExpression)*
    ;

relationalExpression
    :   shiftExpression (('<'|'>'|'<='|'>=') shiftExpression)*
    ;

equalityExpression
    :   relationalExpression (('=='| '!=') relationalExpression)*
    ;

andExpression
    :   equalityExpression ( '&' equalityExpression)*
    ;

exclusiveOrExpression
    :   andExpression ('^' andExpression)*
    ;

inclusiveOrExpression
    :   exclusiveOrExpression ('|' exclusiveOrExpression)*
    ;

logicalAndExpression
    :   inclusiveOrExpression ('&&' inclusiveOrExpression)*
    ;

logicalOrExpression
    :   logicalAndExpression ( '||' logicalAndExpression)*
    ;

conditionalExpression
    :   logicalOrExpression ('?' expression ':' conditionalExpression)?
    ;

assignmentExpression
    :   conditionalExpression
    |   unaryExpression assignmentOperator assignmentExpression
    |   DigitSequence // for
    ;

assignmentOperator
    :   '=' | '*=' | '/=' | '%=' | '+=' | '-=' | '<<=' | '>>=' | '&=' | '^=' | '|='
    ;

expression
    :   assignmentExpression (',' assignmentExpression)*
    ;

constantExpression
    :   conditionalExpression
    ;

declaration
    :   declarationSpecifiers initDeclaratorList? ';'    
    ;

declarationSpecifiers
    :   declarationSpecifier+
    ;

declarationSpecifiers2
    :   declarationSpecifier+
    ;

declarationSpecifier
    :   typeSpecifier
    ;

initDeclaratorList
    :   initDeclarator (',' initDeclarator)*
    ;

initDeclarator
    :   declarator ('=' initializer)?
    ;

typeSpecifier
    :   ('void'
    |   'char'
    |   'byte'
    |   'int'
    |   'long'
    |   'int64'
    |   'float'
    |   'double'
    |   'word'
    |   'dword'
    |   'timer'
    |   'msTimer'
    |   messageType)
    ;

messageType
    : 'message' messageIdentifier
    ;

messageIdentifier
    :   Identifier ('.' (Identifier|'*'))?
    |   '*'
    ;

specifierQualifierList
    :   typeSpecifier specifierQualifierList?
    ;

declarator
    :   directDeclarator
    ;

directDeclarator
    :   Identifier
    |   '(' declarator ')'
    |   directDeclarator '[' assignmentExpression? ']'
    |   directDeclarator '(' parameterTypeList ')'
    |   directDeclarator '(' identifierList? ')'
    ;

nestedParenthesesBlock
    :   (   ~('(' | ')')
        |   '(' nestedParenthesesBlock ')'
        )*
    ;

parameterTypeList
    :   parameterList (',' '...')?
    ;

parameterList
    :   parameterDeclaration (',' parameterDeclaration)*
    ;

parameterDeclaration
    :   declarationSpecifiers declarator
    |   declarationSpecifiers2 abstractDeclarator?
    ;

identifierList
    :   Identifier (',' Identifier)*
    ;

typeName
    :   specifierQualifierList abstractDeclarator?
    ;

abstractDeclarator
	:    directAbstractDeclarator
    ;

directAbstractDeclarator
    :   '(' abstractDeclarator ')'
    |   '[' assignmentExpression? ']'    
    |   '[' '*' ']'
    |   '(' parameterTypeList? ')'
    |   directAbstractDeclarator '[' assignmentExpression? ']'    
    |   directAbstractDeclarator '[' '*' ']'
    |   directAbstractDeclarator '(' parameterTypeList? ')'
    ;

initializer
    :   assignmentExpression
    |   '{' initializerList ','? '}'
    ;

initializerList
    :   designation? initializer (',' designation? initializer)*
    ;

designation
    :   designatorList '='
    ;

designatorList
    :   designator+
    ;

designator
    :   '[' constantExpression ']'
    ;

statement
    :   labeledStatement
    |   compoundStatement
    |   expressionStatement
    |   selectionStatement
    |   iterationStatement
    |   jumpStatement    
    ;

labeledStatement
    :   Identifier ':' statement
    |   'case' (constantExpression) ':' statement
    |   'default' ':' statement
    ;

compoundStatement
    :   '{' blockItemList? '}'
    ;

blockItemList
    :   blockItem+
    ;

blockItem
    :   statement
    |   declaration
    ;

expressionStatement
    :   expression? ';'
    ;

selectionStatement
    :   'if' '(' expression ')' statement ('else' statement)?
    |   'switch' '(' expression ')' statement
    ;

iterationStatement
    :   While '(' expression ')' statement
    |   Do statement While '(' expression ')' ';'
    |   For '(' forCondition ')' statement
    ;

forCondition
	:   (forDeclaration | expression?) ';' forExpression? ';' forExpression?
	;

forDeclaration
    :   declarationSpecifiers initDeclaratorList?
    ;

forExpression
    :   assignmentExpression (',' assignmentExpression)*
    ;

jumpStatement
    :   (('continue'| 'break')
    |   'return' expression?    
    )
    ';'
    ;

compilationUnit
    :   translationUnit? EOF
    ;

translationUnit
    :   externalDeclaration+
    ;

externalDeclaration
    :   functionDefinition
    |   declaration
    |   ';' // stray ;
    ;

functionDefinition
    :   declarationSpecifiers? declarator declarationList? compoundStatement
    ;

declarationList
    :   declaration+
    ;