/** 
 * Capl parser implementation.
 */

parser grammar CaplParser;

options {
   tokenVocab = CaplLexer;
}

primaryExpression
    :	Identifier
    |	AccessToSignalIdentifier
    |	SysvarIdentifier
	|	Constant
	|	StringLiteral+
	|	LeftParen expression RightParen
	|	LeftParen compoundStatement RightParen
	| 	( includeBlock
	    	| variableBlock
			| eventBlock
			| timerBlock
			| errorFrame
			| envBlock
			| functionDefinition
			| enumSpecifier
			| structSpecifier
			| startBlock
			| messageBlock
			| multiplexedMessageBlock
			| stopMeasurement
			| diagRequestBlock
			| diagResponseBlock
			| signalBlock
			| sysvarBlock
			| externalDeclaration
		)+
	;

variableBlock
    :	Variables LeftBrace blockItemList? RightBrace
    ;

includeBlock
    :	Includes LeftBrace IncludeDirective* RightBrace
    ;

startBlock
    :	On Start LeftBrace blockItemList? RightBrace
    ;

eventBlock
    :	On keyEventType LeftBrace blockItemList RightBrace
    ;

timerBlock
    :	On timerType LeftBrace blockItemList RightBrace
    ;

errorFrame
    : 	On ErrorFrame LeftBrace blockItemList? RightBrace
    ;

messageBlock
    :	On messageType LeftBrace blockItemList RightBrace
    ;

multiplexedMessageBlock
    :	On multiplexedMessageType LeftBrace blockItemList RightBrace
    ;

diagRequestBlock
    :	On diagRequestType LeftBrace blockItemList RightBrace
    ;

diagResponseBlock
    :	On diagResponseType LeftBrace blockItemList RightBrace
    ;

signalBlock
    :	On signalType LeftBrace blockItemList RightBrace
    ;

sysvarBlock
    :	On sysvarType LeftBrace blockItemList RightBrace
    ;

stopMeasurement
    :	On StopMeasurement LeftBrace blockItemList RightBrace
    ;

envBlock
    :	On EnvVar Identifier LeftBrace blockItemList RightBrace
    |   On EnvVar LeftParen Identifier RightParen LeftBrace blockItemList RightBrace
    ;

postfixExpression
    :	(primaryExpression
			| LeftParen typeName RightParen LeftBrace initializerList Comma? RightBrace
		)
		(LeftBracket expression RightBracket
			| LeftParen argumentExpressionList? RightParen
			| (PlusPlus | MinusMinus)
		)*
	;

argumentExpressionList
    :	assignmentExpression (Comma assignmentExpression)*
    ;

unaryExpression
    :	(PlusPlus | MinusMinus)* (postfixExpression | unaryOperator castExpression)
    ;

unaryOperator
    :	Plus | Minus | Tilde | Not
    ;

castExpression
    :	LeftParen typeName RightParen castExpression
	|	unaryExpression
	|	DigitSequence // for
	;

multiplicativeExpression
    :	castExpression ((Star | Div | Mod) castExpression)*
    ;

additiveExpression
    :	multiplicativeExpression ((Plus | Minus) multiplicativeExpression)*
	;

shiftExpression
    :	additiveExpression ((LeftShift | RightShift) additiveExpression)*
    ;

relationalExpression
    :	shiftExpression ((Less | Greater | LessEqual | GreaterEqual) shiftExpression)*
    ;

equalityExpression
    :	relationalExpression ((Equal | NotEqual) relationalExpression)*
    ;

andExpression
    :	equalityExpression ( And equalityExpression)*
    ;

exclusiveOrExpression
    :	andExpression (Caret andExpression)*
    ;

inclusiveOrExpression
    :	exclusiveOrExpression (Or exclusiveOrExpression)*
    ;

logicalAndExpression
    :	inclusiveOrExpression (AndAnd inclusiveOrExpression)*
    ;

logicalOrExpression
    :	logicalAndExpression (OrOr logicalAndExpression)*
    ;

conditionalExpression
    :	logicalOrExpression (Question expression Colon conditionalExpression)?
	;

assignmentExpression
    :	conditionalExpression
	|	unaryExpression assignmentOperator assignmentExpression
	|	DigitSequence // for
	;

assignmentOperator
    :	Assign
	|	StarAssign
	|	DivAssign
	|	ModAssign
	|	PlusAssign
	|	MinusAssign
	|	LeftShiftAssign
	|	RightShiftAssign
	|	AndAssign
	|	XorAssign
	|	OrAssign
	;

expression
    :	assignmentExpression (Comma assignmentExpression)*
    ;

constantExpression
    :	conditionalExpression
    ;

declaration
    :	declarationSpecifiers initDeclaratorList? Semi
    ;

declarationSpecifiers
    :	declarationSpecifier+
    ;

declarationSpecifiers2
    :	declarationSpecifier+
    ;

typeQualifier
	:	Const
	;

functionSpecifier
    :	Testfunction
    |	(Export)? Testcase
    ;

declarationSpecifier
    :	typeSpecifier
    |	typeQualifier
    |	functionSpecifier
    ;

initDeclaratorList
    :	initDeclarator (Comma initDeclarator)*
    ;

initDeclarator
    :	declarator (Assign initializer)?
    ;

typeSpecifier
    : 	(Void
	|	Char
	|	Byte
	|	Int
	|	Long
	|	Int64
	|	Float
	|	Double
	|	Word
	|	Dword
	|	Qword
	|	Timer
	|	MsTimer
	|	structSpecifier
	|	enumSpecifier
	|	messageType
	|	multiplexedMessageType
	|	diagRequestType
	|	diagResponseType
	|	signalType
	|	sysvarType)
	;

structSpecifier
    :	structure Identifier? LeftBrace structDeclarationList RightBrace
    |	structure Identifier
    ;

structure
    :	Struct
    ;

structDeclarationList
    :	structDeclaration+
    ;

structDeclaration
    :	specifierQualifierList structDeclaratorList? Semi
    ;

specifierQualifierList
    :	(typeSpecifier | typeQualifier) specifierQualifierList?
    ;

structDeclaratorList
    :	structDeclarator (Comma structDeclarator)*
    ;

structDeclarator
    :	declarator
    |	declarator? Colon constantExpression
    ;

declarator
    :	directDeclarator
    ;

directDeclarator
    :	Identifier
	|	LeftParen declarator RightParen
	|	directDeclarator LeftBracket assignmentExpression? RightBracket
	|	directDeclarator LeftParen parameterTypeList RightParen
	|	directDeclarator LeftParen identifierList? RightParen
	;

nestedParenthesesBlock
    :	(~(LeftParen | RightParen) | LeftParen nestedParenthesesBlock RightParen)*
	;

parameterTypeList
    :	parameterList (Comma Ellipsis)?
    ;

parameterList
    :	parameterDeclaration (Comma parameterDeclaration)*
    ;

parameterDeclaration
    :	declarationSpecifiers declarator
	|	declarationSpecifiers2 abstractDeclarator?
	;

identifierList
    :	Identifier (Comma Identifier)*
    ;

typeName
    :	specifierQualifierList abstractDeclarator?
    ;

abstractDeclarator
    :	directAbstractDeclarator
    ;

directAbstractDeclarator
    :	LeftParen abstractDeclarator RightParen
	|	LeftBracket assignmentExpression? RightBracket
	|	LeftBracket Star RightBracket
	|	LeftParen parameterTypeList? RightParen
	|	directAbstractDeclarator LeftBracket assignmentExpression? RightBracket
	|	directAbstractDeclarator LeftBracket Star RightBracket
	|	directAbstractDeclarator LeftParen parameterTypeList? RightParen
	;

initializer
    :	assignmentExpression
	|	LeftBrace initializerList Comma? RightBrace
	;

initializerList
    :	designation? initializer (Comma designation? initializer)*
    ;

designation
    :	designatorList Assign
    ;

designatorList
    :	designator+
    ;

designator
    :	LeftBracket constantExpression RightBracket
    ;

statement
    :	labeledStatement
	|	compoundStatement
	|	expressionStatement
	|	selectionStatement
	|	iterationStatement
	|	jumpStatement
	;

labeledStatement
    :	Identifier Colon statement
	|	Case (constantExpression) Colon statement
	|	Default Colon statement
	;

compoundStatement
    :	LeftBrace blockItemList? RightBrace
    ;

blockItemList
    :	blockItem+
    ;

blockItem
    :	statement | declaration
    ;

expressionStatement
    :	expression? Semi
    ;

selectionStatement
    :	If LeftParen expression RightParen statement (Else statement)?
	|	Switch LeftParen expression RightParen statement
	;

iterationStatement
    :	While LeftParen expression RightParen statement
	|	Do statement While LeftParen expression RightParen Semi
	|	For LeftParen forCondition RightParen statement
	;

forCondition
    :	(forDeclaration | expression?) Semi forExpression? Semi forExpression?
    ;

forDeclaration
    :	declarationSpecifiers initDeclaratorList?
    ;

forExpression
    :	assignmentExpression (Comma assignmentExpression)*
    ;

jumpStatement
    :	((Continue | Break) | Return expression?) Semi
    ;

compilationUnit
	:	translationUnit? EOF
	;

translationUnit
	:	externalDeclaration+
	;

externalDeclaration
    :	functionDefinition
	|	declaration
	|	Semi
	;

functionDefinition
    :	declarationSpecifiers? declarator declarationList? compoundStatement
    ;

declarationList
	:	declaration+
	;

enumSpecifier
    :   Enum Identifier? LeftBrace enumeratorList Comma? RightBrace Semi?
    |   Enum Identifier
    ;

enumeratorList
    :   enumerator (Comma enumerator)*
    ;

enumerator
    :   enumerationConstant (Assign constantExpression)?
    ;

enumerationConstant
    :   Identifier
    ;

timerType
    :	Timer Identifier (Dot (Identifier | Star))?
    ;

messageType
    :	Message Identifier (Dot (Identifier | Star))?
	|	Message Star
	|	Message Constant
	|	Message Identifier Minus Identifier
	;

multiplexedMessageType
    :	MultiplexedMessage Identifier (Dot (Identifier | Star))?
	|	MultiplexedMessage Star
	|	MultiplexedMessage Constant
	|	MultiplexedMessage Identifier Minus Identifier
	;

diagRequestType
    :	DiagRequest Identifier ((Dot|DoubleColon) (Identifier | Star))?
	|	DiagRequest Star
	|	DiagRequest Constant
	|	DiagRequest Identifier Minus Identifier
	;

diagResponseType
    :	DiagResponse Identifier ((Dot|DoubleColon) (Identifier | Star))?
	|	DiagResponse Star
	|	DiagResponse Constant
	|	DiagResponse Identifier Minus Identifier
	;

signalType
    :	Signal Identifier ((Dot|DoubleColon) (Identifier | Star))?
	|	Signal Star
	|	Signal Constant
	|	Signal Identifier Minus Identifier
	;

sysvarType
    :	DoubleSysvar DoubleColon Identifier (DoubleColon Identifier)*
	;

keyEventType
    :	Key Constant
	|	Key (
			F1
		|	F2
		|	F3
		|	F4
		|	F5
		|	F6
		|	F7
		|	F8
		|	F9
		|	F10
		|	F11
		|	F12
		|	CtrlF1
		|	CtrlF2
		|	CtrlF3
		|	CtrlF4
		|	CtrlF5
		|	CtrlF6
		|	CtrlF7
		|	CtrlF8
		|	CtrlF9
		|	CtrlF10
		|	CtrlF11
		|	CtrlF12
		|	PageUp
		|	PageDown
		|	Home)
	|	Key Star
	;
