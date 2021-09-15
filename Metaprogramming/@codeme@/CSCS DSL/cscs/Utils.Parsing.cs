using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsCsLang
{
   public partial class Utils
   {
      public static Variable GetItem(ParsingScript script)
      {
         script.MoveForwardIf(Constants.NextArg, Constants.Space);
         CheckNotEnd(script);

         var value = new Variable();
         var inQuotes = script.Current == Constants.Quote;

         if (script.Current == Constants.StartGroup)
         {
            // We are extracting a list between curly braces.
            script.Forward(); // Skip the first brace.
            var isList = true;
            value.Tuple = GetArgs(script,
               Constants.StartGroup, Constants.EndGroup, out isList);
            return value;
         }
         // A variable, a function, or a number.
         var var = script.Execute(Constants.NextOrEndArray);
         value = var.Clone();

         if (inQuotes) script.MoveForwardIf(Constants.Quote);
         script.MoveForwardIf(Constants.EndArg, Constants.Space);
         return value;
      }

      public static string GetToken(ParsingScript script, char[] to)

      {
         var curr = script.TryCurrent();
         var prev = script.TryPrev();

         if (!to.Contains(Constants.Space))
            while (curr == Constants.Space && prev != Constants.Quote)
            {
               script.Forward();
               curr = script.TryCurrent();
               prev = script.TryPrev();
            }

         // String in quotes
         var inQuotes = curr == Constants.Quote;
         if (inQuotes)
         {
            var qend = script.Find(Constants.Quote, script.Pointer + 1);
            if (qend == -1)
               throw new ArgumentException("Unmatched quotes in [" +
                                           script.FromPrev() + "]");
            var result = script.Substr(script.Pointer + 1, qend - script.Pointer - 1);
            script.Pointer = qend + 1;
            return result;
         }

         script.MoveForwardIf(Constants.Quote);

         var end = script.FindFirstOf(to);
         end = end < 0 ? script.Size() : end;

         // Skip found characters that have a backslash before.
         while (end > 0 && end + 1 < script.Size() &&
                script.String[end - 1] == '\\') end = script.FindFirstOf(to, end + 1);

         end = end < 0 ? script.Size() : end;

         if (script.At(end - 1) == Constants.Quote) end--;

         var var = script.Substr(script.Pointer, end - script.Pointer);
         // \"yes\" --> "yes"
         var = var.Replace("\\\"", "\"");
         script.Pointer = end;

         script.MoveForwardIf(Constants.Quote, Constants.Space);

         return var;
      }

      public static string GetNextToken(ParsingScript script)
      {
         if (!script.StillValid()) return "";
         var end = script.FindFirstOf(Constants.TokenSeparation);

         if (end < 0) return "";

         var var = script.Substr(script.Pointer, end - script.Pointer);
         script.Pointer = end;
         return var;
      }

      public static void SkipRestExpr(ParsingScript script)
      {
         var argRead = 0;
         var inQuotes = false;
         var previous = Constants.Empty;

         while (script.StillValid())
         {
            var currentChar = script.Current;
            if (inQuotes && currentChar != Constants.Quote)
            {
               script.Forward();
               continue;
            }

            switch (currentChar)
            {
               case Constants.Quote:
                  if (previous != '\\') inQuotes = !inQuotes;
                  break;
               case Constants.StartArg:
                  argRead++;
                  break;
               case Constants.EndArg:
                  argRead--;
                  if (argRead < 0) return;
                  break;
               case Constants.EndStatement:
                  return;
               case Constants.NextArg:
                  if (argRead <= 0) return;
                  break;
               default:
                  break;
            }

            script.Forward();
            previous = currentChar;
         }
      }

      public static string GetStringOrVarValue(ParsingScript script)
      {
         script.MoveForwardIf(Constants.Space);

         // If this token starts with a quote then it is a string constant.
         // Otherwide we treat it as a variable, but if the variable doesn't exist then it
         // will be still treated as a string constant.
         var stringConstant = script.Rest.StartsWith(Constants.Quote.ToString());

         var token = GetToken(script, Constants.NextOrEndArray);
         // Check if this is a variable definition:
         stringConstant = stringConstant || !ParserFunction.FunctionExists(token);
         if (!stringConstant)
         {
            var sourceValue = ParserFunction.GetFunction(token).GetValue(script);
            token = sourceValue.String;
         }

         return token;
      }

      public static bool IsCompareSign(char ch) => ch == '<' || ch == '>' || ch == '=';

      public static bool IsAndOrSign(char ch) => ch == '&' || ch == '|';

      // Checks whether there is an argument separator (e.g.  ',') before the end of the
      // function call. E.g. returns true for "a,b)" and "a(b,c),d)" and false for "b),c".
      public static bool SeparatorExists(ParsingScript script)
      {
         if (!script.StillValid()) return false;

         var argumentList = 0;
         for (var i = script.Pointer; i < script.Size(); i++)
         {
            var ch = script.At(i);
            switch (ch)
            {
               case Constants.NextArg:
                  return true;
               case Constants.StartArg:
                  argumentList++;
                  break;
               case Constants.EndStatement:
               case Constants.EndGroup:
               case Constants.EndArg:
                  if (--argumentList < 0) return false;
                  break;
            }
         }

         return false;
      }

      public static List<string> GetFunctionArgs(ParsingScript script)
      {
         bool isList;
         var args = GetArgs(script,
            Constants.StartArg, Constants.EndArg, out isList);

         var result = new List<string>();
         for (var i = 0; i < args.Count; i++) result.Add(args[i].AsString());
         return result;
      }

      public static List<Variable> GetArgs(ParsingScript script,
         char start, char end, out bool isList)
      {
         var args = new List<Variable>();
         isList = script.StillValid() && script.Current == Constants.StartGroup;

         if (!script.StillValid() || script.Current == Constants.EndStatement) return args;

         var tempScript = new ParsingScript(script.String, script.Pointer);
         var body = GetBodyBetween(tempScript, start, end);
         var rest = script.Rest;
         // After the statement above tempScript.Parent will point to the last
         // character belonging to the body between start and end characters. 

         while (script.Pointer < tempScript.Pointer)
         {
            var item = GetItem(script);
            args.Add(item);
            rest = script.Rest;
            if (script.Pointer == tempScript.Pointer) rest = script.Rest;
            script.MoveForwardIf(Constants.NextArg);
         }

         if (script.Pointer <= tempScript.Pointer) script.MoveForwardIf(Constants.EndArg);

         script.MoveForwardIf(Constants.Space);
         //script.MoveForwardIf(Constants.SPACE, Constants.END_STATEMENT);
         return args;
      }

      public static string[] GetFunctionSignature(ParsingScript script)
      {
         script.MoveForwardIf(Constants.StartArg, Constants.Space);

         var endArgs = script.FindFirstOf(Constants.EndArg.ToString());
         if (endArgs < 0) throw new ArgumentException("Couldn't extract function signature");

         var argStr = script.Substr(script.Pointer, endArgs - script.Pointer);
         var args = argStr.Split(Constants.NextArgArray);

         script.Pointer = endArgs + 1;

         return args;
      }

      public static bool EndsWithFunction(string buffer, List<string> functions)
      {
         foreach (var key in functions)
            if (buffer.EndsWith(key, StringComparison.OrdinalIgnoreCase))
            {
               var prev = key.Length >= buffer.Length
                  ? Constants.EndStatement
                  : buffer[buffer.Length - key.Length - 1];
               if (Constants.TokenSeparation.Contains(prev)) return true;
            }

         return false;
      }

      public static bool SpaceNotNeeded(char next) => next == Constants.Space || next == Constants.StartArg ||
                                                      next == Constants.StartGroup || next == Constants.StartArray ||
                                                      next == Constants.Empty;

      public static bool KeepSpace(StringBuilder sb, char next)
      {
         if (SpaceNotNeeded(next)) return false;

         return EndsWithFunction(sb.ToString(), Constants.FunctWithSpace);
      }

      public static bool KeepSpaceOnce(StringBuilder sb, char next)
      {
         if (SpaceNotNeeded(next)) return false;

         return EndsWithFunction(sb.ToString(), Constants.FunctWithSpaceOnce);
      }

      public static string ConvertToScript(string source, out Dictionary<int, int> char2Line)
      {
         var sb = new StringBuilder(source.Length);
         char2Line = new Dictionary<int, int>();

         var inQuotes = false;
         var spaceOK = false;
         var inComments = false;
         var simpleComments = false;
         var previous = Constants.Empty;

         var parentheses = 0;
         var groups = 0;
         var lineNumber = 0;
         var lastScriptLength = 0;

         for (var i = 0; i < source.Length; i++)
         {
            var ch = source[i];
            var next = i + 1 < source.Length ? source[i + 1] : Constants.Empty;

            if (ch == '\n')
            {
               if (sb.Length > lastScriptLength)
               {
                  char2Line[sb.Length - 1] = lineNumber;
                  lastScriptLength = sb.Length;
               }
               lineNumber++;
            }

            if (inComments && (simpleComments && ch != '\n' ||
                               !simpleComments && ch != '*')) continue;

            switch (ch)
            {
               case '/':
                  if (!inQuotes && (inComments || next == '/' || next == '*'))
                  {
                     inComments = true;
                     simpleComments = simpleComments || next == '/';
                     continue;
                  }
                  break;
               case '*':
                  if (!inQuotes && inComments && next == '/')
                  {
                     i++; // skip next character
                     inComments = false;
                     continue;
                  }
                  break;
               case '“':
               case '”':
               case '"':
                  ch = '"';
                  if (!inComments) if (previous != '\\') inQuotes = !inQuotes;
                  break;
               case ' ':
                  if (inQuotes) sb.Append(ch);
                  else
                  {
                     var keepSpace = KeepSpace(sb, next);
                     spaceOK = keepSpace ||
                               previous != Constants.Empty && previous != Constants.NextArg && spaceOK;
                     var spaceOKonce = KeepSpaceOnce(sb, next);
                     if (spaceOK || spaceOKonce) sb.Append(ch);
                  }
                  continue;
               case '\t':
               case '\r':
                  if (inQuotes) sb.Append(ch);
                  continue;
               case '\n':
                  if (simpleComments) inComments = simpleComments = false;
                  spaceOK = false;
                  continue;
               case Constants.EndArg:
                  if (!inQuotes)
                  {
                     parentheses--;
                     spaceOK = false;
                  }
                  break;
               case Constants.StartArg:
                  if (!inQuotes) parentheses++;
                  break;
               case Constants.EndGroup:
                  if (!inQuotes)
                  {
                     groups--;
                     spaceOK = false;
                  }
                  break;
               case Constants.StartGroup:
                  if (!inQuotes) groups++;
                  break;
               case Constants.EndStatement:
                  if (!inQuotes) spaceOK = false;
                  break;
               default: break;
            }
            if (!inComments) sb.Append(ch);
            previous = ch;
         }

         return sb.ToString();
      }

      public static string BeautifyScript(string script, string header)
      {
         var result = new StringBuilder();
         var extraSpace = "<>=&|+-*/%".ToCharArray();

         var indent = Constants.Indent;
         result.AppendLine(header);

         var inQuotes = false;
         var lineStart = true;

         for (var i = 0; i < script.Length; i++)
         {
            var ch = script[i];
            inQuotes = ch == Constants.Quote ? !inQuotes : inQuotes;

            if (inQuotes)
            {
               result.Append(ch);
               continue;
            }

            var needExtra = extraSpace.Contains(ch) && i > 0 && i < script.Length - 1;
            if (needExtra && !extraSpace.Contains(script[i - 1])) result.Append(" ");

            switch (ch)
            {
               case Constants.StartGroup:
                  result.AppendLine(" " + Constants.StartGroup);
                  indent += Constants.Indent;
                  lineStart = true;
                  break;
               case Constants.EndGroup:
                  indent -= Constants.Indent;
                  result.AppendLine(new string(' ', indent) + Constants.EndGroup);
                  lineStart = true;
                  break;
               case Constants.EndStatement:
                  result.AppendLine(ch.ToString());
                  lineStart = true;
                  break;
               default:
                  if (lineStart)
                  {
                     result.Append(new string(' ', indent));
                     lineStart = false;
                  }
                  result.Append(ch.ToString());
                  break;
            }
            if (needExtra && !extraSpace.Contains(script[i + 1])) result.Append(" ");
         }

         result.AppendLine(Constants.EndGroup.ToString());
         return result.ToString();
      }

      public static string GetBodyBetween(ParsingScript script, char open, char close)
      {
         // We are supposed to be one char after the beginning of the string, i.e.
         // we must not have the opening char as the first one.
         var sb = new StringBuilder(script.Size());
         var braces = 0;
         var inQuotes = false;
         var checkBraces = true;

         for (; script.StillValid(); script.Forward())
         {
            var ch = script.Current;

            if (close != Constants.Quote)
            {
               checkBraces = !inQuotes;
               if (ch == Constants.Quote) inQuotes = !inQuotes;
            }

            if (string.IsNullOrWhiteSpace(ch.ToString()) && sb.Length == 0) continue;
            if (checkBraces && ch == open) braces++;
            else if (checkBraces && ch == close) braces--;

            sb.Append(ch);
            if (braces == -1)
            {
               if (ch == close) sb.Remove(sb.Length - 1, 1);
               break;
            }
         }

         return sb.ToString();
      }

      public static string IsNotSign(string data) =>
         data.StartsWith(Constants.Not) && !data.StartsWith(Constants.NotEqual) ? Constants.Not : null;

      public static string ValidAction(string rest)
      {
         var action = StartsWith(rest, Constants.Actions);
         return action;
      }

      public static string StartsWith(string data, string[] items)
      {
         foreach (var item in items) if (data.StartsWith(item)) return item;
         return null;
      }

      public static List<Variable> GetArrayIndices(ref string varName)
      {
         var end = 0;
         return GetArrayIndices(ref varName, ref end);
      }

      public static List<Variable> GetArrayIndices(ref string varName, ref int end)
      {
         var indices = new List<Variable>();

         var argStart = varName.IndexOf(Constants.StartArray);
         if (argStart < 0) return indices;
         var firstIndexStart = argStart;

         while (argStart < varName.Length &&
                varName[argStart] == Constants.StartArray)
         {
            var argEnd = varName.IndexOf(Constants.EndArray, argStart + 1);
            if (argEnd == -1 || argEnd <= argStart + 1) break;

            var tempScript = new ParsingScript(varName, argStart);
            tempScript.MoveForwardIf(Constants.StartArg, Constants.StartArray);

            var index = tempScript.ExecuteTo(Constants.EndArray);

            indices.Add(index);
            argStart = argEnd + 1;
         }

         if (indices.Count > 0)
         {
            varName = varName.Substring(0, firstIndexStart);
            end = argStart - 1;
         }

         return indices;
      }

      public static Variable ExtractArrayElement(Variable array,
         List<Variable> indices)
      {
         var currLevel = array;

         for (var i = 0; i < indices.Count; i++)
         {
            var index = indices[i];
            var arrayIndex = currLevel.GetArrayIndex(index);

            var tupleSize = currLevel.Tuple != null ? currLevel.Tuple.Count : 0;
            if (arrayIndex < 0 || arrayIndex >= tupleSize)
               throw new ArgumentException("Unknown index [" + index.AsString() +
                                           "] for tuple of size " + tupleSize);
            currLevel = currLevel.Tuple[arrayIndex];
         }
         return currLevel;
      }

      public static string GetLinesFromList(ParsingScript script)
      {
         var lines = GetItem(script);
         if (lines.Tuple == null) throw new ArgumentException("Expected a list argument");

         var sb = new StringBuilder(80 * lines.Tuple.Count);
         foreach (var line in lines.Tuple) sb.AppendLine(line.String);

         return sb.ToString();
      }
   }
}