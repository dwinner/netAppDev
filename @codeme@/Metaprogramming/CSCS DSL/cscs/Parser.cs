using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsCsLang
{
   public class Parser
   {
      public static bool Verbose { get; set; }

      public static Variable SplitAndMerge(ParsingScript script) => SplitAndMerge(script, Constants.EndParseArray);

      public static Variable SplitAndMerge(ParsingScript script, char[] to)
      {
         // First step: process passed expression by splitting it into a list of cells.
         var listToMerge = Split(script, to);

         if (listToMerge.Count == 0)
            throw new ArgumentException("Couldn't parse [" +
                                        script.Rest + "]");

         // Second step: merge list of cells to get the result of an expression.
         var result = MergeList(listToMerge);
         return result;
      }

      private static List<Variable> Split(ParsingScript script, char[] to)
      {
         var listToMerge = new List<Variable>(16);

         if (!script.StillValid() || to.Contains(script.Current))
         {
            listToMerge.Add(Variable.EmptyInstance);
            script.Forward();
            return listToMerge;
         }

         var item = new StringBuilder();
         var arrayIndexDepth = 0;
         var inQuotes = false;
         var negated = 0;

         var rest = script.Rest;
         if (rest == "b[a[0]];")
         {
            var stop = 1;
         }

         do
         { // Main processing cycle of the first part.
            var negateSymbol = Utils.IsNotSign(script.Rest);
            if (negateSymbol != null && !inQuotes)
            {
               negated++;
               script.Forward(negateSymbol.Length);
               continue;
            }

            var ch = script.CurrentAndForward();
            CheckQuotesIndices(script, ch, ref inQuotes, ref arrayIndexDepth);
            string action = null;

            var keepCollecting = inQuotes || arrayIndexDepth > 0 ||
                                 StillCollecting(item.ToString(), to, script, ref action);
            if (keepCollecting)
            {
               // The char still belongs to the previous operand.
               item.Append(ch);

               var goForMore = script.StillValid() &&
                               (inQuotes || arrayIndexDepth > 0 || !to.Contains(script.Current));
               if (goForMore) continue;
            }

            if (SkipOrAppendIfNecessary(item, ch, to)) continue;
            var token = item.ToString();
            CheckConsistency(token, listToMerge, script);

            script.MoveForwardIf(Constants.Space);

            if (action != null && action.Length > 1) script.Forward(action.Length - 1);

            // We are done getting the next token. The getValue() call below may
            // recursively call loadAndCalculate(). This will happen if extracted
            // item is a function or if the next item is starting with a START_ARG '('.
            var func = new ParserFunction(script, token, ch, ref action);
            var current = func.GetValue(script);

            if (negated > 0 && current.Type == Variable.VarType.Number)
            {
               // If there has been a NOT sign, this is a boolean.
               // Use XOR (true if exactly one of the arguments is true).
               var neg = !((negated % 2 == 0) ^ Convert.ToBoolean(current.Value));
               current = new Variable(Convert.ToDouble(neg));
               negated = 0;
            }

            if (action == null) action = UpdateAction(script, to);
            else script.MoveForwardIf(action[0]);

            var next = script.TryCurrent(); // we've already moved forward
            var done = listToMerge.Count == 0 &&
                       (next == Constants.EndStatement ||
                        action == Constants.NullAction && current.Type != Variable.VarType.Number ||
                        current.IsReturn);
            if (done)
            {
               if (action != null && action != Constants.EndArgStr)
                  throw new ArgumentException("Action [" +
                                              action + "] without an argument.");
               // If there is no numerical result, we are not in a math expression.
               listToMerge.Add(current);
               return listToMerge;
            }

            var cell = current.Clone();
            cell.Action = action;

            var addIt = UpdateIfBool(script, ref cell, ref listToMerge);
            if (addIt) listToMerge.Add(cell);
            item.Clear();
         } while (script.StillValid() &&
                  (inQuotes || arrayIndexDepth > 0 || !to.Contains(script.Current)));

         // This happens when called recursively inside of the math expression:
         script.MoveForwardIf(Constants.EndArg);

         return listToMerge;
      }

      private static void CheckConsistency(string item, List<Variable> listToMerge,
         ParsingScript script)
      {
         if (Constants.ControlFlow.Contains(item) && listToMerge.Count > 0) listToMerge.Clear();
      }

      private static void CheckQuotesIndices(ParsingScript script,
         char ch, ref bool inQuotes, ref int arrayIndexDepth)
      {
         switch (ch)
         {
            case Constants.Quote:
            {
               var prev = script.TryPrevPrev();
               inQuotes = prev != '\\' ? !inQuotes : inQuotes;
               return;
            }
            case Constants.StartArray:
            {
               if (!inQuotes) arrayIndexDepth++;
               return;
            }
            case Constants.EndArray:
            {
               if (!inQuotes) arrayIndexDepth--;
               return;
            }
         }
      }

      private static void AppendIfNecessary(StringBuilder item, char ch, char[] to)
      {
         if (ch == Constants.EndArray && to.Length == 1 && to[0] == Constants.EndArray &&
             item.Length > 0 && item[item.Length - 1] != Constants.EndArray) item.Append(ch);
      }

      private static bool SkipOrAppendIfNecessary(StringBuilder item, char ch, char[] to)
      {
         if (to.Length == 1 && to[0] == Constants.EndArray && item.Length > 0)
            if (ch == Constants.EndArray && item[item.Length - 1] != Constants.EndArray) item.Append(ch);
            else if (item.Length == 1 && item[0] == Constants.EndArray) return true;
         return false;
      }

      private static bool StillCollecting(string item, char[] to, ParsingScript script,
         ref string action)
      {
         var prev = script.TryPrevPrev();
         var ch = script.TryPrev();
         var next = script.TryCurrent();

         if (to.Contains(ch) || ch == Constants.StartArg ||
             ch == Constants.StartGroup ||
             next == Constants.Empty) return false;

         // Case of a negative number, or starting with the closing bracket:
         if (item.Length == 0 &&
             (ch == '-' && next != '-' || ch == Constants.EndArray
              || ch == Constants.EndArg)) return true;

         // Case of a scientific notation 1.2e+5 or 1.2e-5 or 1e5:
         if (char.ToUpper(prev) == 'E' &&
             (ch == '-' || ch == '+' || char.IsDigit(ch)) &&
             item.Length > 1 && char.IsDigit(item[item.Length - 2])) return true;

         // Otherwise if it's an action (+, -, *, etc.) or a space
         // we're done collecting current token.
         if ((action = Utils.ValidAction(script.FromPrev())) != null ||
             item.Length > 0 && ch == Constants.Space) return false;

         return true;
      }

      private static bool UpdateIfBool(ParsingScript script, ref Variable current, ref List<Variable> listToMerge)
      {
         // Short-circuit evaluation: check if don't need to evaluate more.
         var needToAdd = true;
         if ((current.Action == "&&" || current.Action == "||") &&
             listToMerge.Count > 0)
            if (CanMergeCells(listToMerge.Last(), current))
            {
               listToMerge.Add(current);
               current = MergeList(listToMerge);
               listToMerge.Clear();
               needToAdd = false;
            }
         if (current.Action == "&&" && current.Value == 0.0 ||
             current.Action == "||" && current.Value != 0.0)
         {
            Utils.SkipRestExpr(script);
            current.Action = Constants.NullAction;
            needToAdd = true;
         }
         return needToAdd;
      }

      private static string UpdateAction(ParsingScript script, char[] to)
      {
         // We search a valid action till we get to the End of Argument ')'
         // or pass the end of string.
         if (!script.StillValid() || script.Current == Constants.EndArg ||
             to.Contains(script.Current)) return Constants.NullAction;

         var action = Utils.ValidAction(script.Rest);

         // We need to advance forward not only the action length but also all
         // the characters we skipped before getting the action.
         var advance = action == null ? 0 : action.Length;
         script.Forward(advance);
         return action == null ? Constants.NullAction : action;
      }

      private static Variable MergeList(List<Variable> listToMerge)
      {
         if (listToMerge.Count == 0) return Variable.EmptyInstance;
         // If there is just one resulting cell there is no need
         // to perform the second step to merge tokens.
         if (listToMerge.Count == 1) return listToMerge[0];

         var baseCell = listToMerge[0];
         var index = 1;

         // Second step: merge list of cells to get the result of an expression.
         var result = Merge(baseCell, ref index, listToMerge);
         return result;
      }

      // From outside this function is called with mergeOneOnly = false.
      // It also calls itself recursively with mergeOneOnly = true, meaning
      // that it will return after only one merge.
      private static Variable Merge(Variable current, ref int index, List<Variable> listToMerge,
         bool mergeOneOnly = false)
      {
         if (Verbose) Utils.PrintList(listToMerge, index - 1);

         while (index < listToMerge.Count)
         {
            var next = listToMerge[index++];

            while (!CanMergeCells(current, next)) // If we cannot merge cells yet, go to the next cell and merge
               // next cells first. E.g. if we have 1+2*3, we first merge next
               // cells, i.e. 2*3, getting 6, and then we can merge 1+6.
               Merge(next, ref index, listToMerge, true /* mergeOneOnly */);

            MergeCells(current, next);
            if (mergeOneOnly) break;
         }

         if (Verbose)
            Console.WriteLine("Calculated: {0} {1}",
               current.Value, current.String);
         return current;
      }

      private static void MergeCells(Variable leftCell, Variable rightCell)
      {
         if (leftCell.IsReturn ||
             leftCell.Type == Variable.VarType.Break ||
             leftCell.Type == Variable.VarType.Continue) return;
         if (leftCell.Type == Variable.VarType.Number) MergeNumbers(leftCell, rightCell);
         else MergeStrings(leftCell, rightCell);

         leftCell.Action = rightCell.Action;
      }

      private static void MergeNumbers(Variable leftCell, Variable rightCell)
      {
         if (rightCell.Type != Variable.VarType.Number) rightCell.Value = rightCell.AsDouble();
         switch (leftCell.Action)
         {
            case "^":
               leftCell.Value = Math.Pow(leftCell.Value, rightCell.Value);
               break;
            case "%":
               leftCell.Value %= rightCell.Value;
               break;
            case "*":
               leftCell.Value *= rightCell.Value;
               break;
            case "/":
               if (rightCell.Value == 0.0) throw new ArgumentException("Division by zero");
               leftCell.Value /= rightCell.Value;
               break;
            case "+":
               if (rightCell.Type != Variable.VarType.Number) leftCell.String = leftCell.AsString() + rightCell.String;
               else leftCell.Value += rightCell.Value;
               break;
            case "-":
               leftCell.Value -= rightCell.Value;
               break;
            case "<":
               leftCell.Value = Convert.ToDouble(leftCell.Value < rightCell.Value);
               break;
            case ">":
               leftCell.Value = Convert.ToDouble(leftCell.Value > rightCell.Value);
               break;
            case "<=":
               leftCell.Value = Convert.ToDouble(leftCell.Value <= rightCell.Value);
               break;
            case ">=":
               leftCell.Value = Convert.ToDouble(leftCell.Value >= rightCell.Value);
               break;
            case "==":
               leftCell.Value = Convert.ToDouble(leftCell.Value == rightCell.Value);
               break;
            case "!=":
               leftCell.Value = Convert.ToDouble(leftCell.Value != rightCell.Value);
               break;
            case "&&":
               leftCell.Value = Convert.ToDouble(
                  Convert.ToBoolean(leftCell.Value) && Convert.ToBoolean(rightCell.Value));
               break;
            case "||":
               leftCell.Value = Convert.ToDouble(
                  Convert.ToBoolean(leftCell.Value) || Convert.ToBoolean(rightCell.Value));
               break;
         }
      }

      private static void MergeStrings(Variable leftCell, Variable rightCell)
      {
         switch (leftCell.Action)
         {
            case "+":
               leftCell.String = leftCell.AsString() + rightCell.AsString();
               break;
            case "<":
               var arg1 = leftCell.AsString();
               var arg2 = rightCell.AsString();
               leftCell.Value = Convert.ToDouble(string.Compare(arg1, arg2) < 0);
               break;
            case ">":
               leftCell.Value = Convert.ToDouble(
                  string.Compare(leftCell.AsString(), rightCell.AsString()) > 0);
               break;
            case "<=":
               leftCell.Value = Convert.ToDouble(
                  string.Compare(leftCell.AsString(), rightCell.AsString()) <= 0);
               break;
            case ">=":
               leftCell.Value = Convert.ToDouble(
                  string.Compare(leftCell.AsString(), rightCell.AsString()) >= 0);
               break;
            case "==":
               leftCell.Value = Convert.ToDouble(
                  string.Compare(leftCell.AsString(), rightCell.AsString()) == 0);
               break;
            case "!=":
               leftCell.Value = Convert.ToDouble(
                  string.Compare(leftCell.AsString(), rightCell.AsString()) != 0);
               break;
            case ")":
               break;
            default:
               throw new ArgumentException("Can't perform action [" +
                                           leftCell.Action + "] on strings");
         }
      }

      private static bool CanMergeCells(Variable leftCell, Variable rightCell) =>
         GetPriority(leftCell.Action) >= GetPriority(rightCell.Action);

      private static int GetPriority(string action)
      {
         switch (action)
         {
            case "++":
            case "--": return 10;
            case "^": return 9;
            case "%":
            case "*":
            case "/": return 8;
            case "+":
            case "-": return 7;
            case "<":
            case ">":
            case ">=":
            case "<=": return 6;
            case "==":
            case "!=": return 5;
            case "&&": return 4;
            case "||": return 3;
            case "+=":
            case "-=":
            case "*=":
            case "/=":
            case "%=":
            case "=": return 2;
         }
         return 0;
      }
   }
}