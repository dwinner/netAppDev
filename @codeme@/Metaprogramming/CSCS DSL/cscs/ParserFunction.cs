using System;
using System.Collections.Generic;
using CsCsLang.FunctionFlow;

namespace CsCsLang
{
   public class ParserFunction
   {
      // Global functions and variables:
      private static readonly Dictionary<string, ParserFunction> s_functions = new Dictionary<string, ParserFunction>();

      // Global variables:
      //private static Dictionary<string, ParserFunction> s_globals = new Dictionary<string, ParserFunction> ();

      // Global actions - function:
      private static readonly Dictionary<string, ActionFunction> s_actions = new Dictionary<string, ActionFunction>();

      // Local variables:
      // Stack of the functions being executed:

      private static readonly StringOrNumberFunction s_strOrNumFunction =
         new StringOrNumberFunction();

      private static readonly IdentityFunction s_idFunction =
         new IdentityFunction();

      private readonly ParserFunction m_impl;

      protected bool m_isGlobal = true;

      protected bool m_isNative = true;

      protected string m_name;

      public ParserFunction() => m_impl = this;

      // A "virtual" Constructor
      internal ParserFunction(ParsingScript script, string item, char ch, ref string action)
      {
         if (item.Length == 0 && (ch == Constants.StartArg || !script.StillValid()))
         {
            // There is no function, just an expression in parentheses
            m_impl = s_idFunction;
            return;
         }

         m_impl = GetRegisteredAction(item, ref action);
         if (m_impl != null) return;

         m_impl = GetArrayFunction(item, script, action);
         if (m_impl != null) return;

         m_impl = GetFunction(item);
         if (m_impl != null) return;

         if (m_impl == s_strOrNumFunction && string.IsNullOrWhiteSpace(item))
         {
            var problem = !string.IsNullOrWhiteSpace(action) ? action : ch.ToString();
            var restData = ch + script.Rest;
            throw new ArgumentException("Couldn't parse [" + problem + "] in " + restData + "...");
         }

         // Function not found, will try to parse this as a string in quotes or a number.
         s_strOrNumFunction.Item = item;
         m_impl = s_strOrNumFunction;
      }

      public string Name
      {
         get => m_name;
         set => m_name = value;
      }

      public bool isGlobal
      {
         get => m_isGlobal;
         set => m_isGlobal = value;
      }

      public bool isNative
      {
         get => m_isNative;
         set => m_isNative = value;
      }

      public static Stack<StackLevel> ExecutionStack { get; } = new Stack<StackLevel>();

      public static ParserFunction GetArrayFunction(string name, ParsingScript script, string action)
      {
         var arrayStart = name.IndexOf(Constants.StartArray);
         if (arrayStart < 0) return null;

         var arrayName = name;

         var delta = 0;
         var arrayIndices = Utils.GetArrayIndices(ref arrayName, ref delta);

         if (arrayIndices.Count == 0) return null;

         var pf = GetFunction(arrayName);
         var varFunc = pf as GetVarFunction;
         if (varFunc == null) return null;

         // we temporarily backtrack for the processing
         script.Backward(name.Length - arrayStart - 1);
         script.Backward(action != null ? action.Length : 0);
         // delta shows us how manxy chars we need to advance forward in GetVarFunction()
         delta -= arrayName.Length;
         delta += action != null ? action.Length : 0;

         varFunc.Indices = arrayIndices;
         varFunc.Delta = delta;
         return varFunc;
      }

      public static ParserFunction GetRegisteredAction(string name, ref string action)
      {
         var actionFunction = GetAction(action);

         // If passed action exists and is registered we are done.
         if (actionFunction == null) return null;

         var theAction = actionFunction.NewInstance() as ActionFunction;
         theAction.Name = name;
         theAction.Action = action;

         action = null;
         return theAction;
      }

      public static ParserFunction GetFunction(string item)
      {
         ParserFunction impl;
         // First search among local variables.

         if (ExecutionStack.Count > 0)
         {
            var local = ExecutionStack.Peek().Variables;
            if (local.TryGetValue(item, out impl)) return impl;
         }
         if (s_functions.TryGetValue(item, out impl)) return impl.NewInstance();

         return null;
      }

      public static ActionFunction GetAction(string action)
      {
         if (string.IsNullOrWhiteSpace(action)) return null;

         ActionFunction impl;
         if (s_actions.TryGetValue(action, out impl)) return impl;

         return null;
      }

      public static bool FunctionExists(string item) => LocalNameExists(item) || GlobalNameExists(item);

      public static void AddGlobalOrLocalVariable(string name, ParserFunction function)
      {
         function.Name = name;
         if (ExecutionStack.Count > 0 && (LocalNameExists(name) || !GlobalNameExists(name))) AddLocalVariable(function);
         else AddGlobal(name, function, false /* not native */);
      }

      private static bool LocalNameExists(string name)
      {
         if (ExecutionStack.Count == 0) return false;
         var vars = ExecutionStack.Peek().Variables;
         return vars.ContainsKey(name);
      }

      private static bool GlobalNameExists(string name) => s_functions.ContainsKey(name);

      public static void RegisterFunction(string name, ParserFunction function,
         bool isNative = true)
      {
         AddGlobal(name, function, isNative);
      }

      public static void AddGlobal(string name, ParserFunction function,
         bool isNative = true)
      {
         function.isNative = isNative;
         s_functions[name] = function;

         if (string.IsNullOrWhiteSpace(function.Name)) function.Name = name;
         if (!isNative) Translation.AddTempKeyword(name);
         //Console.WriteLine("Registered function " + name);
      }

      public static void AddAction(string name, ActionFunction action)
      {
         s_actions[name] = action;
      }

      public static void AddLocalVariables(StackLevel locals)
      {
         ExecutionStack.Push(locals);
      }

      public static void AddStackLevel(string name)
      {
         ExecutionStack.Push(new StackLevel(name));
      }

      public static void AddLocalVariable(ParserFunction local)
      {
         local.m_isGlobal = false;
         StackLevel locals = null;
         if (ExecutionStack.Count == 0)
         {
            locals = new StackLevel();
            ExecutionStack.Push(locals);
         }
         else locals = ExecutionStack.Peek();

         locals.Variables[local.Name] = local;
         Translation.AddTempKeyword(local.Name);
      }

      public static void PopLocalVariables()
      {
         ExecutionStack.Pop();
      }

      public static int GetCurrentStackLevel() => ExecutionStack.Count;

      public static void InvalidateStacksAfterLevel(int level)
      {
         while (ExecutionStack.Count > level) ExecutionStack.Pop();
      }

      public static void PopLocalVariable(string name)
      {
         if (ExecutionStack.Count == 0) return;
         var locals = ExecutionStack.Peek().Variables;
         locals.Remove(name);
      }

      public Variable GetValue(ParsingScript script) => m_impl.Evaluate(script);

      protected virtual Variable Evaluate(ParsingScript script) => new Variable();

      // Derived classes may want to return a new instance in order to
      // not to use same object in calculations.
      public virtual ParserFunction NewInstance() => this;

      public class StackLevel
      {
         public StackLevel(string name = null)
         {
            Name = name;
            Variables = new Dictionary<string, ParserFunction>();
         }

         public string Name { get; set; }
         public Dictionary<string, ParserFunction> Variables { get; set; }
      }
   }
}