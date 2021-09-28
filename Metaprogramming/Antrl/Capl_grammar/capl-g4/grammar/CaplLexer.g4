lexer grammar CaplLexer;

KeyHit :    KeyboardSymbol ;

KeyboardSymbol
    :   '\'' [a-zA-Z1-9] '\''
    |   'F1'    // TODO: enumerate all printable symbols there
    ;

StopMeasurement : 'stopMeasurement';
Start : 'start';
ErrorFrame : 'errorFrame';
Key : 'key';
On : 'on';
Variables : 'variables';
Break : 'break';
Case : 'case';
Char : 'char';
Byte : 'byte';
Continue : 'continue';
Default : 'default';
Do : 'do';
Double : 'double';
Else : 'else';
Float : 'float';
For : 'for';
If : 'if';
Int : 'int';
Word : 'word';
Dword : 'dword';
Message : 'message';
EnvVar : 'envVar';
Timer : 'timer';
MsTimer : 'msTimer';
Long : 'long';
Int64 : 'int64';
Return : 'return';
Switch : 'switch';
Void : 'void';
While : 'while';

LeftParen : '(';
RightParen : ')';
LeftBracket : '[';
RightBracket : ']';
LeftBrace : '{';
RightBrace : '}';

Less : '<';
LessEqual : '<=';
Greater : '>';
GreaterEqual : '>=';
LeftShift : '<<';
RightShift : '>>';

Plus : '+';
PlusPlus : '++';
Minus : '-';
MinusMinus : '--';
Star : '*';
Div : '/';
Mod : '%';

And : '&';
Or : '|';
AndAnd : '&&';
OrOr : '||';
Caret : '^';
Not : '!';
Tilde : '~';

Question : '?';
Colon : ':';
Semi : ';';
Comma : ',';

Assign : '=';
// '*=' | '/=' | '%=' | '+=' | '-=' | '<<=' | '>>=' | '&=' | '^=' | '|='
StarAssign : '*=';
DivAssign : '/=';
ModAssign : '%=';
PlusAssign : '+=';
MinusAssign : '-=';
LeftShiftAssign : '<<=';
RightShiftAssign : '>>=';
AndAssign : '&=';
XorAssign : '^=';
OrAssign : '|=';

Equal : '==';
NotEqual : '!=';
Ellipsis : '...';

Identifier
    :   IdentifierNondigit (IdentifierNondigit | Digit )*
    |   ('this'|IdentifierNondigit (IdentifierNondigit | Digit )*) '.' Identifier ('.' (Identifier))*
    ;

This : 'this';
Dot : '.';

fragment IdentifierNondigit
    :   Nondigit
    |   UniversalCharacterName
    ;

fragment Nondigit
    :   [a-zA-Z_]
    ;

fragment Digit
    :   [0-9]
    ;

fragment UniversalCharacterName
    :   '\\u' HexQuad
    |   '\\U' HexQuad HexQuad
    ;

fragment HexQuad
    :   HexadecimalDigit HexadecimalDigit HexadecimalDigit HexadecimalDigit
    ;

Constant
    :   IntegerConstant
    |   FloatingConstant
    |   CharacterConstant
    ;

fragment IntegerConstant
    :   DecimalConstant IntegerSuffix?
    |   OctalConstant IntegerSuffix?
    |   HexadecimalConstant IntegerSuffix?
    |	BinaryConstant
    ;

fragment BinaryConstant
    :	'0' [bB] [0-1]+
    ;

fragment DecimalConstant
    :   NonzeroDigit Digit*
    ;

fragment OctalConstant
    :   '0' OctalDigit*
    ;

fragment HexadecimalConstant
    :   HexadecimalPrefix HexadecimalDigit+
    ;

fragment HexadecimalPrefix
    :   '0' [xX]
    ;

fragment NonzeroDigit
    :   [1-9]
    ;

fragment OctalDigit
    :   [0-7]
    ;

fragment HexadecimalDigit
    :   [0-9a-fA-F]
    ;

fragment IntegerSuffix
    :   LongSuffix
    |   LongLongSuffix
    ;

fragment LongSuffix
    :   [lL]
    ;

fragment LongLongSuffix
    :   'll' | 'LL'
    ;

fragment FloatingConstant
    :   DecimalFloatingConstant
    |   HexadecimalFloatingConstant
    ;

fragment DecimalFloatingConstant
    :   FractionalConstant ExponentPart? FloatingSuffix?
    |   DigitSequence ExponentPart FloatingSuffix?
    ;

fragment HexadecimalFloatingConstant
    :   HexadecimalPrefix (HexadecimalFractionalConstant | HexadecimalDigitSequence) BinaryExponentPart FloatingSuffix?
    ;

fragment FractionalConstant
    :   DigitSequence? '.' DigitSequence
    |   DigitSequence '.'
    ;

fragment ExponentPart
    :   [eE] Sign? DigitSequence
    ;

fragment Sign
    :   [+-]
    ;

DigitSequence
    :   Digit+
    ;

fragment HexadecimalFractionalConstant
    :   HexadecimalDigitSequence? '.' HexadecimalDigitSequence
    |   HexadecimalDigitSequence '.'
    ;

fragment BinaryExponentPart
    :   [pP] Sign? DigitSequence
    ;

fragment HexadecimalDigitSequence
    :   HexadecimalDigit+
    ;

fragment FloatingSuffix
    :   [flFL]
    ;

fragment CharacterConstant
    :   '\'' CCharSequence '\''
    |   'L\'' CCharSequence '\''
    |   'u\'' CCharSequence '\''
    |   'U\'' CCharSequence '\''
    ;

fragment CCharSequence
    :   CChar+
    ;

fragment CChar
    :   ~['\\\r\n]
    |   EscapeSequence
    ;

fragment EscapeSequence
    :   SimpleEscapeSequence
    |   OctalEscapeSequence
    |   HexadecimalEscapeSequence
    |   UniversalCharacterName
    ;

fragment SimpleEscapeSequence
    :   '\\' ['"?abfnrtv\\]
    ;

fragment OctalEscapeSequence
    :   '\\' OctalDigit OctalDigit? OctalDigit?
    ;

fragment HexadecimalEscapeSequence
    :   '\\x' HexadecimalDigit+
    ;

StringLiteral
    :   EncodingPrefix? '"' SCharSequence? '"'
    ;

fragment EncodingPrefix
    :   'u8'
    |   'u'
    |   'U'
    |   'L'
    ;

fragment SCharSequence
    :   SChar+
    ;

fragment SChar
    :   ~["\\\r\n]
    |   EscapeSequence
    |   '\\\n'   // Added line
    |   '\\\r\n' // Added line
    ;

Whitespace
    :   [ \t]+
        -> skip
    ;

Newline
    :   (   '\r' '\n'?
        |   '\n'
        )
        -> skip
    ;

BlockComment
    :   '/*' .*? '*/'
        -> skip
    ;

LineComment
    :   '//' ~[\r\n]*
        -> skip
    ;