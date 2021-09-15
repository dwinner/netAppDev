using System.Collections.Generic;
using System.Linq;

namespace CsCsLang
{
   public static class Constants
   {
      public const char StartArg = '(';
      public const char EndArg = ')';
      public const char StartArray = '[';
      public const char EndArray = ']';
      public const char EndLine = '\n';
      public const char NextArg = ',';
      public const char Quote = '"';
      public const char Space = ' ';
      public const char StartGroup = '{';
      public const char EndGroup = '}';
      public const char VarStart = '$';
      public const char EndStatement = ';';
      public const char ForEach = ':';
      public const char ContinueLine = '\\';
      public const char Empty = '\0';

      public const string Assignment = "=";
      public const string And = "&&";
      public const string Or = "||";
      public const string Not = "!";
      public const string Increment = "++";
      public const string Decrement = "--";
      public const string Equal = "==";
      public const string NotEqual = "!=";
      public const string Less = "<";
      public const string LessEq = "<=";
      public const string Greater = ">";
      public const string GreaterEq = ">=";
      public const string AddAssign = "+=";
      public const string SubtAssign = "-=";
      public const string MultAssign = "*=";
      public const string DivAssign = "/=";

      public const string Break = "break";
      public const string Catch = "catch";
      public const string Comment = "//";
      public const string Continue = "continue";
      public const string Else = "else";
      public const string ElseIf = "elif";
      public const string For = "for";
      public const string Function = "function";
      public const string If = "if";
      public const string Include = "include";
      public const string Return = "return";
      public const string Throw = "throw";
      public const string Try = "try";
      public const string Type = "type";
      public const string While = "while";

      public const string True = "true";
      public const string False = "false";

      public const string Abs = "abs";
      public const string Acos = "acos";
      public const string Add = "add";
      public const string AddToHash = "AddToHash";
      public const string AddAllToHash = "AddAllToHash";
      public const string Append = "append";
      public const string Appendline = "appendline";
      public const string Appendlines = "appendlines";
      public const string Asin = "asin";
      public const string Cd = "cd";
      public const string CdDotDot = "cd..";
      public const string Copy = "copy";
      public const string Ceil = "ceil";
      public const string Connectsrv = "connectsrv";
      public const string Contains = "contains";
      public const string ConsoleClr = "clr";
      public const string Cos = "cos";
      public const string DeepCopy = "DeepCopy";
      public const string Dir = "dir";
      public const string Delete = "del";
      public const string Env = "env";
      public const string Exists = "exists";
      public const string Exit = "exit";
      public const string Exp = "exp";
      public const string Findfiles = "findfiles";
      public const string Findstr = "findstr";
      public const string Floor = "floor";
      public const string GetColumn = "getcolumn";
      public const string GetKeys = "getkeys";
      public const string IndexOf = "indexof";
      public const string Kill = "kill";
      public const string Lock = "lock";
      public const string Log = "log";
      public const string Mkdir = "mkdir";
      public const string More = "more";
      public const string Move = "move";
      public const string Now = "Now";
      public const string Pi = "pi";
      public const string Pow = "pow";
      public const string Print = "print";
      public const string PrintBlack = "printblack";
      public const string PrintGray = "printgray";
      public const string PrintGreen = "printgreen";
      public const string PrintRed = "printred";
      public const string Psinfo = "psinfo";
      public const string Pstime = "pstime";
      public const string Pwd = "pwd";
      public const string Random = "GetRandom";
      public const string Read = "read";
      public const string Readfile = "readfile";
      public const string Readnumber = "readnum";
      public const string Remove = "RemoveItem";
      public const string RemoveAt = "RemoveAt";
      public const string Round = "round";
      public const string Run = "run";
      public const string Signal = "signal";
      public const string Setenv = "setenv";
      public const string Set = "set";
      public const string Show = "show";
      public const string Sin = "sin";
      public const string Size = "size";
      public const string Sleep = "sleep";
      public const string Sqrt = "sqrt";
      public const string Startsrv = "startsrv";
      public const string StopwatchElapsed = "StopWatchElapsed";
      public const string StopwatchStart = "StartStopWatch";
      public const string StopwatchStop = "StopStopWatch";
      public const string StrContains = "StrContains";
      public const string StrEndsWith = "StrEndsWith";
      public const string StrEquals = "StrEqual";
      public const string StrIndexOf = "StrIndexOf";
      public const string StrLower = "StrLower";
      public const string StrReplace = "StrReplace";
      public const string StrStartsWith = "StrStartsWith";
      public const string StrSubstr = "Substring";
      public const string StrTrim = "StrTrim";
      public const string StrUpper = "StrUpper";
      public const string Substr = "substr";
      public const string Tail = "tail";
      public const string Thread = "thread";
      public const string ThreadId = "threadid";
      public const string Tokenize = "tokenize";
      public const string TokenizeLines = "TokenizeLines";
      public const string TokenCounter = "CountTokens";
      public const string Tolower = "tolower";
      public const string Toupper = "toupper";
      public const string Translate = "translate";
      public const string Wait = "wait";
      public const string Write = "write";
      public const string Writeline = "writeline";
      public const string Writelines = "writelines";
      private const string Writenl = "writenl";
      public const string WriteConsole = "WriteConsole";

      public const int Indent = 2;
      public const int DefaultFileLines = 20;
      public const int MaxCharsToShow = 45;

      public const string English = "en";
      private const string German = "de";
      public const string Russian = "ru";
      private const string Spanish = "es";
      private const string Synonyms = "sy";

      public static readonly string EndArgStr = EndArg.ToString();
      public static readonly string NullAction = EndArg.ToString();

      public static readonly string[] OperActions = {"+=", "-=", "*=", "/=", "%=", "&=", "|=", "^="};

      private static readonly string[] _MathActions =
      {
         "&&",
         "||",
         "==",
         "!=",
         "<=",
         ">=",
         "++",
         "--",
         "%",
         "*",
         "/",
         "+",
         "-",
         "^",
         "<",
         ">",
         "="
      };

      // Actions: always decreasing by the number of characters.
      public static string[] Actions = OperActions.Union(_MathActions).ToArray();

      public static readonly char[] NextArgArray = NextArg.ToString().ToCharArray();
      public static readonly char[] EndArgArray = EndArg.ToString().ToCharArray();
      public static char[] EndArrayArray = EndArray.ToString().ToCharArray();
      public static char[] EndLineArray = EndLine.ToString().ToCharArray();
      public static char[] ForArray = (EndArgStr + ForEach).ToCharArray();
      public static char[] QuoteArray = Quote.ToString().ToCharArray();

      public static char[] CompareArray = "<>=)".ToCharArray();
      public static char[] IfArgArray = "&|)".ToCharArray();
      public static readonly char[] EndParseArray = {Space, EndStatement, EndArg, EndGroup, '\n'};
      public static readonly char[] NextOrEndArray = {NextArg, EndArg, EndGroup, EndStatement, Space};

      public static readonly char[] TokenSeparation = "<>=+-*/%&|^,!()[]{}\t\n; ".ToCharArray();

      // Functions that allow a space separator after them, on top of parentheses. The
      // function arguments may have spaces as well, e.g. copy a.txt b.txt
      public static readonly List<string> FunctWithSpace = new List<string>
      {
         Appendline,
         Cd,
         Connectsrv,
         Copy,
         Delete,
         Dir,
         Exists,
         Findfiles,
         Findstr,
         Function,
         Mkdir,
         More,
         Move,
         Print,
         Readfile,
         Run,
         Show,
         Startsrv,
         Tail,
         Translate,
         Write,
         Writeline,
         Writenl
      };

      // Functions that allow a space separator after them, on top of parentheses but
      // only once, i.e. function arguments are not allowed to have spaces
      // between them e.g. return a*b;
      public static readonly List<string> FunctWithSpaceOnce = new List<string>
      {
         Return,
         Throw
      };

      // The Control Flow Functions. It doesn't make sense to merge them or
      // use in calculation of a result.
      public static readonly List<string> ControlFlow = new List<string>
      {
         Break,
         Continue,
         Function,
         If,
         Include,
         For,
         While,
         Return,
         Throw,
         Try
      };

      public static readonly List<string> ElseList = new List<string>();
      public static readonly List<string> ElseIfList = new List<string>();
      public static readonly List<string> CatchList = new List<string>();

      public const string AllFiles = "*.*";

      public static string Language(string lang)
      {
         switch (lang)
         {
            case "English": return English;
            case "German": return German;
            case "Russian": return Russian;
            case "Spanish": return Spanish;
            case "Synonyms": return Synonyms;
            default: return English;
         }
      }

      public static string TypeToString(Variable.VarType type)
      {
         switch (type)
         {
            case Variable.VarType.Number: return "NUMBER";
            case Variable.VarType.String: return "STRING";
            case Variable.VarType.Array: return "ARRAY";
            case Variable.VarType.Break: return "BREAK";
            case Variable.VarType.Continue: return "CONTINUE";
            default: return "NONE";
         }
      }
   }
}