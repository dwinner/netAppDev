using System;
using System.Collections.Generic;
using System.Linq;

namespace CsCsLang
{
   public class ParsingScript
   {
      private int m_scriptOffset; // used in functiond defined in bigger scripts

      public ParsingScript(string data, int from = 0,
         Dictionary<int, int> char2Line = null)
      {
         String = data;
         Pointer = from;
         Char2Line = char2Line;
      }

      public ParsingScript(ParsingScript other)
      {
         String = other.String;
         Pointer = other.Pointer;
         Char2Line = other.Char2Line;
         Filename = other.Filename;
         OriginalScript = other.OriginalScript;
      }

      public int Pointer { get; set; }

      public string String { get; private set; }

      public string Rest => Substr(Pointer, Constants.MaxCharsToShow);

      public char Current => String[Pointer];

      public Dictionary<int, int> Char2Line { get; set; }

      public int ScriptOffset
      {
         set => m_scriptOffset = value;
      }

      public string Filename { get; set; }

      public string OriginalScript { get; set; }

      public int Size() => String.Length;
      public bool StillValid() => Pointer < String.Length;

      public void SetDone()
      {
         Pointer = String.Length;
      }

      public int Find(char ch, int from = -1) => String.IndexOf(ch, from < 0 ? Pointer : from);

      public int FindFirstOf(string str, int from = -1) => FindFirstOf(str.ToCharArray(), from);

      public int FindFirstOf(char[] arr, int from = -1) => String.IndexOfAny(arr, from < 0 ? Pointer : from);

      public string Substr(int fr = -2, int len = -1)
      {
         var from = Math.Min(Pointer, String.Length - 1);
         fr = fr == -2 ? from : fr == -1 ? 0 : fr;
         return len < 0 || len >= String.Length - fr ? String.Substring(fr) : String.Substring(fr, len);
      }

      public string GetOriginalLine(out int lineNumber)
      {
         lineNumber = GetOriginalLineNumber();
         if (lineNumber < 0) return "";

         var lines = OriginalScript.Split(Constants.EndLine);
         if (lineNumber < lines.Length) return lines[lineNumber];

         return "";
      }

      public int GetOriginalLineNumber()
      {
         if (Char2Line == null || Char2Line.Count == 0) return -1;

         var pos = m_scriptOffset + Pointer;
         var lineStart = Char2Line.Keys.ToList();
         var lower = 0;
         var index = lower;

         if (pos <= lineStart[lower]) return Char2Line[lineStart[lower]];
         var upper = lineStart.Count - 1;
         if (pos >= lineStart[upper]) return Char2Line[lineStart[upper]];

         while (lower <= upper)
         {
            index = (lower + upper) / 2;
            var guessPos = lineStart[index];
            if (pos == guessPos) break;
            if (pos < guessPos)
            {
               if (index == 0 || pos > lineStart[index - 1]) break;
               upper = index - 1;
            }
            else lower = index + 1;
         }

         return Char2Line[lineStart[index]];
      }

      public char At(int i) => String[i];
      public char CurrentAndForward() => String[Pointer++];

      public char TryCurrent() => Pointer < String.Length ? String[Pointer] : Constants.Empty;

      public char TryNext() => Pointer + 1 < String.Length ? String[Pointer + 1] : Constants.Empty;

      public char TryPrev() => Pointer >= 1 ? String[Pointer - 1] : Constants.Empty;

      public char TryPrevPrev() => Pointer >= 2 ? String[Pointer - 2] : Constants.Empty;

      public string FromPrev(int backChars = 1, int maxChars = Constants.MaxCharsToShow) =>
         Substr(Pointer - backChars, maxChars);

      public void Forward(int delta = 1)
      {
         Pointer += delta;
      }

      public void Backward(int delta = 1)
      {
         if (Pointer >= delta) Pointer -= delta;
      }

      public void MoveForwardIf(char[] arr)
      {
         foreach (var ch in arr) if (MoveForwardIf(ch)) return;
      }

      public bool MoveForwardIf(char expected, char expected2 = Constants.Empty)
      {
         if (StillValid() && (Current == expected || Current == expected2))
         {
            Forward();
            return true;
         }
         return false;
      }

      public void MoveBackIf(char notExpected)
      {
         if (StillValid() && Pointer > 0 && Current == notExpected) Backward();
      }

      public void SkipAllIfNotIn(char toSkip, char[] to)
      {
         if (to.Contains(toSkip)) return;
         while (StillValid() && Current == toSkip) Forward();
      }

      public int GoToNextStatement()
      {
         var endGroupRead = 0;
         while (StillValid())
         {
            var currentChar = Current;
            switch (currentChar)
            {
               case Constants.EndGroup:
                  endGroupRead++;
                  Forward(); // '}'
                  return endGroupRead;
               case Constants.StartGroup: // '{'
               case Constants.Quote: // '"'
               case Constants.Space: // ' '
               case Constants.EndStatement: // ';'
               case Constants.EndArg: // ')'
                  Forward();
                  break;
               default: return endGroupRead;
            }
         }
         return endGroupRead;
      }

      public Variable Execute() => ExecuteFrom(Pointer);

      public Variable ExecuteTo(char to = '\0') => ExecuteFrom(Pointer, to);

      public Variable ExecuteFrom(int from, char to = '\0')
      {
         Pointer = from;
         var toArray = to == '\0' ? Constants.EndParseArray : to.ToString().ToCharArray();
         return Execute(toArray);
      }

      public Variable Execute(char[] toArray)
      {
         if (!String.EndsWith(Constants.EndStatement.ToString())) String += Constants.EndStatement;
         return Parser.SplitAndMerge(this, toArray);
      }

      public void ExecuteAll()
      {
         while (StillValid())
         {
            ExecuteTo(Constants.EndLine);
            GoToNextStatement();
         }
      }
   }
}