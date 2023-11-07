using System.Diagnostics;
using Graph.Adt;
using Graph.Algs;

namespace StringHandling;

/// <summary>
///    The <see cref="RegExNfa" />  class provides a data type for creating a
///    <em>nondeterministic finite state automaton</em> (NFA) from a regular
///    expression and testing whether a given string is matched by that regular
///    expression.
///    It supports the following operations: <em>concatenation</em>,
///    <em>closure</em>, <em>binary or</em>, and <em>parentheses</em>.
///    It does not support <em>mutiway or</em>, <em>character classes</em>,
///    <em>metacharacters</em> (either in the text or pattern),
///    <em>capturing capabilities</em>, <em>greedy</em> or <em>reluctant</em>
///    modifiers, and other features in industrial-strength implementations
///    such as <see cref="System.Text.RegularExpressions.Regex" />.
///    <p>
///       This implementation builds the NFA using a digraph and a stack
///       and simulates the NFA using digraph search.
///       The constructor takes time proportional to <em>m</em>, where <em>m</em>
///       is the number of characters in the regular expression.
///       The <em>recognizes</em> method takes time proportional to <em>m n</em>,
///       where <em>n</em> is the number of characters in the text.
///    </p>
/// </summary>
public sealed class RegExNfa
{
   private const char OpenParen = '(';
   private const char VerticalLine = '|';
   private const char CloseParen = ')';
   private const char Star = '*';
   private const char Dot = '.';
   private readonly DiGraph _graph; // digraph of epsilon transitions
   private readonly string _regExpr; // regular expression
   private readonly int _regExprLen; // number of characters in regular expression

   /// <summary>
   ///    Initializes the NFA from the specified regular expression.
   /// </summary>
   /// <param name="regExpr">The regular expression</param>
   public RegExNfa(string regExpr)
   {
      _regExpr = regExpr;
      _regExprLen = regExpr.Length;
      var operandStack = new Stack<int>();
      _graph = new DiGraph(_regExprLen + 1);
      for (var reIdx = 0; reIdx < _regExprLen; reIdx++)
      {
         var loopBackIdx = reIdx;
         var curReChar = _regExpr[reIdx];
         switch (curReChar)
         {
            case OpenParen or VerticalLine:
               operandStack.Push(reIdx);
               break;

            case CloseParen:
            {
               var orIdx = operandStack.Pop();
               switch (_regExpr[orIdx])
               {
                  // 2-way or operator
                  case VerticalLine:
                     loopBackIdx = operandStack.Pop();
                     _graph.AddEdge(loopBackIdx, orIdx + 1);
                     _graph.AddEdge(orIdx, reIdx);
                     break;

                  case OpenParen:
                     loopBackIdx = orIdx;
                     break;

                  default:
#if DEBUG
                     Debug.Assert(false, $"Invalid regular expression '{regExpr}'");
#endif
                     break;
               }

               break;
            }
         }

         // closure operator (uses 1-character lookahead)
         if (reIdx < _regExprLen - 1 && _regExpr[reIdx + 1] == Star)
         {
            _graph.AddEdge(loopBackIdx, reIdx + 1);
            _graph.AddEdge(reIdx + 1, loopBackIdx);
         }

         if (curReChar is OpenParen or Star or CloseParen)
         {
            _graph.AddEdge(reIdx, reIdx + 1);
         }
      }

      if (operandStack.Count != 0)
      {
         throw new ArgumentException("Invalid regular expression", nameof(regExpr));
      }
   }

   /// <summary>
   ///    Finds out if <paramref name="text" /> can be recognized by the regular expression
   /// </summary>
   /// <param name="text">The text</param>
   /// <returns>true if the text is matched, false otherwise</returns>
   /// <exception cref="InvalidOperationException">If <paramref name="text" /> contains the metacharacter</exception>
   public bool Recognized(string text)
   {
      const int srcVertex = 0;
      var directedDfs = new DirectedDepth1StSearch(_graph);
      directedDfs.Search(srcVertex);
      var markedVertices = GetMarkedVertices(directedDfs);

      // Compute possible NFA states for txt[i+1]
      foreach (var curChar in text)
      {
         if (curChar is Star or VerticalLine or OpenParen or CloseParen)
         {
            throw new InvalidOperationException($"text contains the metacharacter '{curChar}'");
         }

         var matchedVertices = GetMatchedVertices(markedVertices, curChar);
         if (matchedVertices.Count == 0)
         {
            continue;
         }

         directedDfs = new DirectedDepth1StSearch(_graph);
         directedDfs.Search(matchedVertices);
         markedVertices = GetMarkedVertices(directedDfs);

         // optimization if no states reachable
         if (markedVertices.Count == 0)
         {
            return false;
         }
      }

      return CheckAccessState(markedVertices);
   }

   // check for accept state
   private bool CheckAccessState(in List<int> markedVertices) =>
      markedVertices.Any(vtx => vtx == _regExprLen);

   private List<int> GetMatchedVertices(in List<int> markedVertices, char currentChar) =>
      (from vtx in markedVertices
         where vtx != _regExprLen
         where _regExpr[vtx] == currentChar || _regExpr[vtx] == Dot
         select vtx + 1)
      .ToList();

   private List<int> GetMarkedVertices(in DirectedDepth1StSearch directedDfs)
   {
      var markedVertices = new List<int>();
      for (var vtx = 0; vtx < _graph.VerticeCount; vtx++)
      {
         if (directedDfs.IsMarked(vtx))
         {
            markedVertices.Add(vtx);
         }
      }

      return markedVertices;
   }
}