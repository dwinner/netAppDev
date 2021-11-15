using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using CsCsLang.FunctionFlow;

namespace CsCsLang
{
   public class Interpreter
   {
      private static Interpreter instance;

      private readonly StringBuilder m_output = new StringBuilder();

      private int MAX_LOOPS;

      private Interpreter()
      {
         Init();
      }

      public static Interpreter Instance
      {
         get
         {
            if (instance == null) instance = new Interpreter();
            return instance;
         }
      }

      public string Output
      {
         get
         {
            var output = m_output.ToString().Trim();
            m_output.Clear();
            return output;
         }
      }

      public event EventHandler<OutputAvailableEventArgs> GetOutput;

      public void AppendOutput(string text, bool newLine = false)
      {
         var handler = GetOutput;
         if (handler != null)
         {
            var args = new OutputAvailableEventArgs(text +
                                                    (newLine ? Environment.NewLine : string.Empty));
            handler(this, args);
         }
      }

      public void Init()
      {
         ParserFunction.RegisterFunction(Constants.If, new IfStatement());
         ParserFunction.RegisterFunction(Constants.While, new WhileStatement());
         ParserFunction.RegisterFunction(Constants.For, new ForStatement());
         ParserFunction.RegisterFunction(Constants.Break, new BreakStatement());
         ParserFunction.RegisterFunction(Constants.Continue, new ContinueStatement());
         ParserFunction.RegisterFunction(Constants.Return, new ReturnStatement());
         ParserFunction.RegisterFunction(Constants.Function, new FunctionCreator());
         ParserFunction.RegisterFunction(Constants.Include, new IncludeFile());
         ParserFunction.RegisterFunction(Constants.Try, new TryBlock());
         ParserFunction.RegisterFunction(Constants.Throw, new ThrowFunction());
         ParserFunction.RegisterFunction(Constants.Type, new TypeFunction());
         ParserFunction.RegisterFunction(Constants.True, new BoolFunction(true));
         ParserFunction.RegisterFunction(Constants.False, new BoolFunction(false));

         ParserFunction.RegisterFunction(Constants.Abs, new AbsFunction());
         ParserFunction.RegisterFunction(Constants.Acos, new AcosFunction());
         ParserFunction.RegisterFunction(Constants.Add, new AddFunction());
         ParserFunction.RegisterFunction(Constants.AddToHash, new AddVariableToHashFunction());
         ParserFunction.RegisterFunction(Constants.AddAllToHash, new AddVariablesToHashFunction());
         ParserFunction.RegisterFunction(Constants.Append, new AppendFunction());
         ParserFunction.RegisterFunction(Constants.Appendline, new AppendLineFunction());
         ParserFunction.RegisterFunction(Constants.Appendlines, new AppendLinesFunction());
         ParserFunction.RegisterFunction(Constants.Asin, new AsinFunction());
         ParserFunction.RegisterFunction(Constants.Cd, new CdFunction());
         ParserFunction.RegisterFunction(Constants.CdDotDot, new Cd__Function());
         ParserFunction.RegisterFunction(Constants.Ceil, new CeilFunction());
         ParserFunction.RegisterFunction(Constants.Connectsrv, new ClientSocket());
         ParserFunction.RegisterFunction(Constants.ConsoleClr, new ClearConsole());
         ParserFunction.RegisterFunction(Constants.Contains, new ContainsFunction());
         ParserFunction.RegisterFunction(Constants.Copy, new CopyFunction());
         ParserFunction.RegisterFunction(Constants.Cos, new CosFunction());
         ParserFunction.RegisterFunction(Constants.DeepCopy, new DeepCopyFunction());
         ParserFunction.RegisterFunction(Constants.Delete, new DeleteFunction());
         ParserFunction.RegisterFunction(Constants.Dir, new DirFunction());
         ParserFunction.RegisterFunction(Constants.Env, new GetEnvFunction());
         ParserFunction.RegisterFunction(Constants.Exists, new ExistsFunction());
         ParserFunction.RegisterFunction(Constants.Exit, new ExitFunction());
         ParserFunction.RegisterFunction(Constants.Exp, new ExpFunction());
         ParserFunction.RegisterFunction(Constants.Findfiles, new FindfilesFunction());
         ParserFunction.RegisterFunction(Constants.Findstr, new FindstrFunction());
         ParserFunction.RegisterFunction(Constants.Floor, new FloorFunction());
         ParserFunction.RegisterFunction(Constants.GetColumn, new GetColumnFunction());
         ParserFunction.RegisterFunction(Constants.GetKeys, new GetAllKeysFunction());
         ParserFunction.RegisterFunction(Constants.IndexOf, new IndexOfFunction());
         ParserFunction.RegisterFunction(Constants.Kill, new KillFunction());
         ParserFunction.RegisterFunction(Constants.Lock, new LockFunction());
         ParserFunction.RegisterFunction(Constants.Log, new LogFunction());
         ParserFunction.RegisterFunction(Constants.Mkdir, new MkdirFunction());
         ParserFunction.RegisterFunction(Constants.More, new MoreFunction());
         ParserFunction.RegisterFunction(Constants.Move, new MoveFunction());
         ParserFunction.RegisterFunction(Constants.Now, new DateTimeFunction());
         ParserFunction.RegisterFunction(Constants.Pi, new PiFunction());
         ParserFunction.RegisterFunction(Constants.Pow, new PowFunction());
         ParserFunction.RegisterFunction(Constants.Print, new PrintFunction());
         ParserFunction.RegisterFunction(Constants.PrintBlack, new PrintFunction(ConsoleColor.Black));
         ParserFunction.RegisterFunction(Constants.PrintGray, new PrintFunction(ConsoleColor.DarkGray));
         ParserFunction.RegisterFunction(Constants.PrintGreen, new PrintFunction(ConsoleColor.Green));
         ParserFunction.RegisterFunction(Constants.PrintRed, new PrintFunction(ConsoleColor.Red));
         ParserFunction.RegisterFunction(Constants.Psinfo, new PsInfoFunction());
         ParserFunction.RegisterFunction(Constants.Pstime, new ProcessorTimeFunction());
         ParserFunction.RegisterFunction(Constants.Pwd, new PwdFunction());
         ParserFunction.RegisterFunction(Constants.Random, new GetRandomFunction());
         ParserFunction.RegisterFunction(Constants.Read, new ReadConsole());
         ParserFunction.RegisterFunction(Constants.Readfile, new ReadCSCSFileFunction());
         ParserFunction.RegisterFunction(Constants.Readnumber, new ReadConsole(true));
         ParserFunction.RegisterFunction(Constants.Remove, new RemoveFunction());
         ParserFunction.RegisterFunction(Constants.RemoveAt, new RemoveAtFunction());
         ParserFunction.RegisterFunction(Constants.Round, new RoundFunction());
         ParserFunction.RegisterFunction(Constants.Run, new RunFunction());
         ParserFunction.RegisterFunction(Constants.Signal, new SignalWaitFunction(true));
         ParserFunction.RegisterFunction(Constants.Setenv, new SetEnvFunction());
         ParserFunction.RegisterFunction(Constants.Show, new ShowFunction());
         ParserFunction.RegisterFunction(Constants.Sin, new SinFunction());
         ParserFunction.RegisterFunction(Constants.Size, new SizeFunction());
         ParserFunction.RegisterFunction(Constants.Sleep, new SleepFunction());
         ParserFunction.RegisterFunction(Constants.Sqrt, new SqrtFunction());
         ParserFunction.RegisterFunction(Constants.Startsrv, new ServerSocket());
         ParserFunction.RegisterFunction(Constants.StopwatchElapsed,
            new StopWatchFunction(StopWatchFunction.Mode.ELAPSED));
         ParserFunction.RegisterFunction(Constants.StopwatchStart,
            new StopWatchFunction(StopWatchFunction.Mode.START));
         ParserFunction.RegisterFunction(Constants.StopwatchStop, new StopWatchFunction(StopWatchFunction.Mode.STOP));
         ParserFunction.RegisterFunction(Constants.StrContains,
            new StringManipulationFunction(StringManipulationFunction.Mode.CONTAINS));
         ParserFunction.RegisterFunction(Constants.StrLower,
            new StringManipulationFunction(StringManipulationFunction.Mode.LOWER));
         ParserFunction.RegisterFunction(Constants.StrEndsWith,
            new StringManipulationFunction(StringManipulationFunction.Mode.ENDS_WITH));
         ParserFunction.RegisterFunction(Constants.StrEquals,
            new StringManipulationFunction(StringManipulationFunction.Mode.EQUALS));
         ParserFunction.RegisterFunction(Constants.StrIndexOf,
            new StringManipulationFunction(StringManipulationFunction.Mode.INDEX_OF));
         ParserFunction.RegisterFunction(Constants.StrReplace,
            new StringManipulationFunction(StringManipulationFunction.Mode.REPLACE));
         ParserFunction.RegisterFunction(Constants.StrStartsWith,
            new StringManipulationFunction(StringManipulationFunction.Mode.STARTS_WITH));
         ParserFunction.RegisterFunction(Constants.StrSubstr,
            new StringManipulationFunction(StringManipulationFunction.Mode.SUBSTRING));
         ParserFunction.RegisterFunction(Constants.StrTrim,
            new StringManipulationFunction(StringManipulationFunction.Mode.TRIM));
         ParserFunction.RegisterFunction(Constants.StrUpper,
            new StringManipulationFunction(StringManipulationFunction.Mode.UPPER));
         ParserFunction.RegisterFunction(Constants.Substr, new SubstrFunction());
         ParserFunction.RegisterFunction(Constants.Tail, new TailFunction());
         ParserFunction.RegisterFunction(Constants.Thread, new ThreadFunction());
         ParserFunction.RegisterFunction(Constants.ThreadId, new ThreadIDFunction());
         ParserFunction.RegisterFunction(Constants.Tokenize, new TokenizeFunction());
         ParserFunction.RegisterFunction(Constants.TokenizeLines, new TokenizeLinesFunction());
         ParserFunction.RegisterFunction(Constants.TokenCounter, new TokenCounterFunction());
         ParserFunction.RegisterFunction(Constants.Tolower, new ToLowerFunction());
         ParserFunction.RegisterFunction(Constants.Toupper, new ToUpperFunction());
         ParserFunction.RegisterFunction(Constants.Translate, new TranslateFunction());
         ParserFunction.RegisterFunction(Constants.Wait, new SignalWaitFunction(false));
         ParserFunction.RegisterFunction(Constants.Write, new PrintFunction(false));
         ParserFunction.RegisterFunction(Constants.Writeline, new WriteLineFunction());
         ParserFunction.RegisterFunction(Constants.Writelines, new WriteLinesFunction());
         ParserFunction.RegisterFunction(Constants.WriteConsole, new WriteToConsole());

         ParserFunction.AddAction(Constants.Assignment, new AssignFunction());
         ParserFunction.AddAction(Constants.Increment, new IncrementDecrementFunction());
         ParserFunction.AddAction(Constants.Decrement, new IncrementDecrementFunction());

         for (var i = 0; i < Constants.OperActions.Length; i++)
            ParserFunction.AddAction(Constants.OperActions[i], new OperatorAssignFunction());

         Constants.ElseList.Add(Constants.Else);
         Constants.ElseIfList.Add(Constants.ElseIf);
         Constants.CatchList.Add(Constants.Catch);

         ReadConfig();
      }

      private void ReadConfig()
      {
         MAX_LOOPS = ReadConfig("maxLoops", 256000);
#if !__MOBILE__
         if (ConfigurationManager.GetSection("Languages") == null) return;
         var languagesSection = ConfigurationManager.GetSection("Languages") as NameValueCollection;
         if (languagesSection.Count == 0) return;

         var errorsPath = ConfigurationManager.AppSettings["errorsPath"];
         Translation.Language = ConfigurationManager.AppSettings["language"];
         Translation.LoadErrors(errorsPath);

         var dictPath = ConfigurationManager.AppSettings["dictionaryPath"];

         var baseLanguage = Constants.English;
         var languages = languagesSection["languages"];
         var supportedLanguages = languages.Split(",".ToCharArray());

         foreach (var lang in supportedLanguages)
         {
            var language = Constants.Language(lang);
            var tr1 = Translation.KeywordsDictionary(baseLanguage, language);
            var tr2 = Translation.KeywordsDictionary(language, baseLanguage);

            Translation.TryLoadDictionary(dictPath, baseLanguage, language);

            var languageSection = ConfigurationManager.GetSection(lang) as NameValueCollection;

            Translation.Add(languageSection, Constants.If, tr1, tr2);
            Translation.Add(languageSection, Constants.For, tr1, tr2);
            Translation.Add(languageSection, Constants.While, tr1, tr2);
            Translation.Add(languageSection, Constants.Break, tr1, tr2);
            Translation.Add(languageSection, Constants.Continue, tr1, tr2);
            Translation.Add(languageSection, Constants.Return, tr1, tr2);
            Translation.Add(languageSection, Constants.Function, tr1, tr2);
            Translation.Add(languageSection, Constants.Include, tr1, tr2);
            Translation.Add(languageSection, Constants.Throw, tr1, tr2);
            Translation.Add(languageSection, Constants.Try, tr1, tr2);
            Translation.Add(languageSection, Constants.Type, tr1, tr2);
            Translation.Add(languageSection, Constants.True, tr1, tr2);
            Translation.Add(languageSection, Constants.False, tr1, tr2);

            Translation.Add(languageSection, Constants.Add, tr1, tr2);
            Translation.Add(languageSection, Constants.AddToHash, tr1, tr2);
            Translation.Add(languageSection, Constants.AddAllToHash, tr1, tr2);
            Translation.Add(languageSection, Constants.Append, tr1, tr2);
            Translation.Add(languageSection, Constants.Appendline, tr1, tr2);
            Translation.Add(languageSection, Constants.Appendlines, tr1, tr2);
            Translation.Add(languageSection, Constants.Cd, tr1, tr2);
            Translation.Add(languageSection, Constants.CdDotDot, tr1, tr2);
            Translation.Add(languageSection, Constants.Ceil, tr1, tr2);
            Translation.Add(languageSection, Constants.ConsoleClr, tr1, tr2);
            Translation.Add(languageSection, Constants.Contains, tr1, tr2);
            Translation.Add(languageSection, Constants.Copy, tr1, tr2);
            Translation.Add(languageSection, Constants.DeepCopy, tr1, tr2);
            Translation.Add(languageSection, Constants.Delete, tr1, tr2);
            Translation.Add(languageSection, Constants.Dir, tr1, tr2);
            Translation.Add(languageSection, Constants.Env, tr1, tr2);
            Translation.Add(languageSection, Constants.Exit, tr1, tr2);
            Translation.Add(languageSection, Constants.Exists, tr1, tr2);
            Translation.Add(languageSection, Constants.Findfiles, tr1, tr2);
            Translation.Add(languageSection, Constants.Findstr, tr1, tr2);
            Translation.Add(languageSection, Constants.Floor, tr1, tr2);
            Translation.Add(languageSection, Constants.GetColumn, tr1, tr2);
            Translation.Add(languageSection, Constants.GetKeys, tr1, tr2);
            Translation.Add(languageSection, Constants.IndexOf, tr1, tr2);
            Translation.Add(languageSection, Constants.Kill, tr1, tr2);
            Translation.Add(languageSection, Constants.Lock, tr1, tr2);
            Translation.Add(languageSection, Constants.Mkdir, tr1, tr2);
            Translation.Add(languageSection, Constants.More, tr1, tr2);
            Translation.Add(languageSection, Constants.Move, tr1, tr2);
            Translation.Add(languageSection, Constants.Now, tr1, tr2);
            Translation.Add(languageSection, Constants.Print, tr1, tr2);
            Translation.Add(languageSection, Constants.Print, tr1, tr2);
            Translation.Add(languageSection, Constants.PrintBlack, tr1, tr2);
            Translation.Add(languageSection, Constants.PrintGray, tr1, tr2);
            Translation.Add(languageSection, Constants.PrintGreen, tr1, tr2);
            Translation.Add(languageSection, Constants.PrintRed, tr1, tr2);
            Translation.Add(languageSection, Constants.Psinfo, tr1, tr2);
            Translation.Add(languageSection, Constants.Pwd, tr1, tr2);
            Translation.Add(languageSection, Constants.Random, tr1, tr2);
            Translation.Add(languageSection, Constants.Read, tr1, tr2);
            Translation.Add(languageSection, Constants.Readfile, tr1, tr2);
            Translation.Add(languageSection, Constants.Readnumber, tr1, tr2);
            Translation.Add(languageSection, Constants.Remove, tr1, tr2);
            Translation.Add(languageSection, Constants.RemoveAt, tr1, tr2);
            Translation.Add(languageSection, Constants.Round, tr1, tr2);
            Translation.Add(languageSection, Constants.Run, tr1, tr2);
            Translation.Add(languageSection, Constants.Set, tr1, tr2);
            Translation.Add(languageSection, Constants.Setenv, tr1, tr2);
            Translation.Add(languageSection, Constants.Show, tr1, tr2);
            Translation.Add(languageSection, Constants.Signal, tr1, tr2);
            Translation.Add(languageSection, Constants.Size, tr1, tr2);
            Translation.Add(languageSection, Constants.Sleep, tr1, tr2);
            Translation.Add(languageSection, Constants.StopwatchElapsed, tr1, tr2);
            Translation.Add(languageSection, Constants.StopwatchStart, tr1, tr2);
            Translation.Add(languageSection, Constants.StopwatchStop, tr1, tr2);
            Translation.Add(languageSection, Constants.StrContains, tr1, tr2);
            Translation.Add(languageSection, Constants.StrEndsWith, tr1, tr2);
            Translation.Add(languageSection, Constants.StrEquals, tr1, tr2);
            Translation.Add(languageSection, Constants.StrIndexOf, tr1, tr2);
            Translation.Add(languageSection, Constants.StrLower, tr1, tr2);
            Translation.Add(languageSection, Constants.StrReplace, tr1, tr2);
            Translation.Add(languageSection, Constants.StrStartsWith, tr1, tr2);
            Translation.Add(languageSection, Constants.StrSubstr, tr1, tr2);
            Translation.Add(languageSection, Constants.StrTrim, tr1, tr2);
            Translation.Add(languageSection, Constants.StrUpper, tr1, tr2);
            Translation.Add(languageSection, Constants.Substr, tr1, tr2);
            Translation.Add(languageSection, Constants.Tail, tr1, tr2);
            Translation.Add(languageSection, Constants.Thread, tr1, tr2);
            Translation.Add(languageSection, Constants.ThreadId, tr1, tr2);
            Translation.Add(languageSection, Constants.Tokenize, tr1, tr2);
            Translation.Add(languageSection, Constants.TokenizeLines, tr1, tr2);
            Translation.Add(languageSection, Constants.TokenCounter, tr1, tr2);
            Translation.Add(languageSection, Constants.Tolower, tr1, tr2);
            Translation.Add(languageSection, Constants.Toupper, tr1, tr2);
            Translation.Add(languageSection, Constants.Translate, tr1, tr2);
            Translation.Add(languageSection, Constants.Wait, tr1, tr2);
            Translation.Add(languageSection, Constants.Write, tr1, tr2);
            Translation.Add(languageSection, Constants.Writeline, tr1, tr2);
            Translation.Add(languageSection, Constants.Writelines, tr1, tr2);
            Translation.Add(languageSection, Constants.WriteConsole, tr1, tr2);

            // Special dealing for else, elif since they are not separate
            // functions but are part of the if statement block.
            // Same for and, or, not.
            Translation.AddSubstatement(languageSection, Constants.Else, Constants.ElseList, tr1, tr2);
            Translation.AddSubstatement(languageSection, Constants.ElseIf, Constants.ElseIfList, tr1, tr2);
            Translation.AddSubstatement(languageSection, Constants.Catch, Constants.CatchList, tr1, tr2);
         }
#endif
      }

      public int ReadConfig(string configName, int defaultValue = 0)
      {
         var value = defaultValue;
#if !__MOBILE__
         var config = ConfigurationManager.AppSettings[configName];
         if (string.IsNullOrWhiteSpace(config) || !int.TryParse(config, out value))
            return defaultValue;
#endif
         return value;
      }

      public Variable Process(string script)
      {
         Dictionary<int, int> char2Line;
         var data = Utils.ConvertToScript(script, out char2Line);
         if (string.IsNullOrWhiteSpace(data)) return null;

         var toParse = new ParsingScript(data, 0, char2Line);
         toParse.OriginalScript = script;

         Variable result = null;

         while (toParse.Pointer < data.Length)
         {
            result = toParse.ExecuteTo();
            toParse.GoToNextStatement();
         }

         return result;
      }

      internal Variable ProcessFor(ParsingScript script)
      {
         var forString = Utils.GetBodyBetween(script, Constants.StartArg, Constants.EndArg);
         script.Forward();
         if (forString.Contains(Constants.EndStatement.ToString())) ProcessCanonicalFor(script, forString);
         else ProcessArrayFor(script, forString);

         return Variable.EmptyInstance;
      }

      private void ProcessArrayFor(ParsingScript script, string forString)
      {
         var index = forString.IndexOf(Constants.ForEach);
         if (index <= 0 || index == forString.Length - 1) throw new ArgumentException("Expecting: for(item : array)");

         var varName = forString.Substring(0, index);

         var forScript = new ParsingScript(forString);
         var arrayValue = forScript.ExecuteFrom(index + 1);

         var cycles = arrayValue.TotalElements();
         if (cycles == 0)
         {
            SkipBlock(script);
            return;
         }
         var startForCondition = script.Pointer;

         for (var i = 0; i < cycles; i++)
         {
            script.Pointer = startForCondition;
            var current = arrayValue.GetValue(i);
            ParserFunction.AddGlobalOrLocalVariable(varName,
               new GetVarFunction(current));
            var result = ProcessBlock(script);
            if (result.IsReturn || result.Type == Variable.VarType.Break) break;
         }
         script.Pointer = startForCondition;
         SkipBlock(script);
      }

      private void ProcessCanonicalFor(ParsingScript script, string forString)
      {
         var forTokens = forString.Split(Constants.EndStatement);
         if (forTokens.Length != 3) throw new ArgumentException("Expecting: for(init; condition; loopStatement)");

         var startForCondition = script.Pointer;

         var initScript = new ParsingScript(forTokens[0] + Constants.EndStatement);
         var condScript = new ParsingScript(forTokens[1] + Constants.EndStatement);
         var loopScript = new ParsingScript(forTokens[2] + Constants.EndStatement);

         initScript.ExecuteFrom(0);

         var cycles = 0;
         var stillValid = true;

         while (stillValid)
         {
            var condResult = condScript.ExecuteFrom(0);
            stillValid = Convert.ToBoolean(condResult.Value);
            if (!stillValid) return;

            if (MAX_LOOPS > 0 && ++cycles >= MAX_LOOPS)
               throw new ArgumentException("Looks like an infinite loop after " +
                                           cycles + " cycles.");

            script.Pointer = startForCondition;
            var result = ProcessBlock(script);
            if (result.IsReturn || result.Type == Variable.VarType.Break) break;
            loopScript.ExecuteFrom(0);
         }

         script.Pointer = startForCondition;
         SkipBlock(script);
      }

      internal Variable ProcessWhile(ParsingScript script)
      {
         var startWhileCondition = script.Pointer;

         // A check against an infinite loop.
         var cycles = 0;
         var stillValid = true;

         while (stillValid)
         {
            script.Pointer = startWhileCondition;

            //int startSkipOnBreakChar = from;
            var condResult = script.ExecuteTo(Constants.EndArg);
            stillValid = Convert.ToBoolean(condResult.Value);
            if (!stillValid) break;

            // Check for an infinite loop if we are comparing same values:
            if (MAX_LOOPS > 0 && ++cycles >= MAX_LOOPS)
               throw new ArgumentException("Looks like an infinite loop after " +
                                           cycles + " cycles.");

            var result = ProcessBlock(script);
            if (result.IsReturn || result.Type == Variable.VarType.Break)
            {
               script.Pointer = startWhileCondition;
               break;
            }
         }

         // The while condition is not true anymore: must skip the whole while
         // block before continuing with next statements.
         SkipBlock(script);
         return Variable.EmptyInstance;
      }

      internal Variable ProcessIf(ParsingScript script)
      {
         var startIfCondition = script.Pointer;

         var result = script.ExecuteTo(Constants.EndArg);
         var isTrue = Convert.ToBoolean(result.Value);

         if (isTrue)
         {
            result = ProcessBlock(script);

            if (result.IsReturn ||
                result.Type == Variable.VarType.Break ||
                result.Type == Variable.VarType.Continue)
            {
               // We are here from the middle of the if-block. Skip it.
               script.Pointer = startIfCondition;
               SkipBlock(script);
            }
            SkipRestBlocks(script);

            return result;
            //return Variable.EmptyInstance;
         }

         // We are in Else. Skip everything in the If statement.
         SkipBlock(script);

         var nextData = new ParsingScript(script);

         var nextToken = Utils.GetNextToken(nextData);

         if (Constants.ElseIfList.Contains(nextToken))
         {
            script.Pointer = nextData.Pointer + 1;
            result = ProcessIf(script);
         }
         else if (Constants.ElseList.Contains(nextToken))
         {
            script.Pointer = nextData.Pointer + 1;
            result = ProcessBlock(script);
         }

         if (result.IsReturn) return result;
         return Variable.EmptyInstance;
      }

      internal Variable ProcessTry(ParsingScript script)
      {
         var startTryCondition = script.Pointer - 1;
         var currentStackLevel = ParserFunction.GetCurrentStackLevel();
         Exception exception = null;

         Variable result = null;

         try
         {
            result = ProcessBlock(script);
         }
         catch (ArgumentException exc)
         {
            exception = exc;
         }

         if (exception != null || result.IsReturn ||
             result.Type == Variable.VarType.Break ||
             result.Type == Variable.VarType.Continue)
         {
            // We are here from the middle of the try-block either because
            // an exception was thrown or because of a Break/Continue. Skip it.
            script.Pointer = startTryCondition;
            SkipBlock(script);
         }

         var catchToken = Utils.GetNextToken(script);
         script.Forward(); // skip opening parenthesis
         // The next token after the try block must be a catch.
         if (!Constants.CatchList.Contains(catchToken))
            throw new ArgumentException("Expecting a 'catch()' but got [" +
                                        catchToken + "]");

         var exceptionName = Utils.GetNextToken(script);
         script.Forward(); // skip closing parenthesis

         if (exception != null)
         {
            var excStack = CreateExceptionStack(exceptionName, currentStackLevel);
            ParserFunction.InvalidateStacksAfterLevel(currentStackLevel);

            var excFunc = new GetVarFunction(new Variable(exception.Message + excStack));
            ParserFunction.AddGlobalOrLocalVariable(exceptionName, excFunc);

            result = ProcessBlock(script);
            ParserFunction.PopLocalVariable(exceptionName);
         }
         else SkipBlock(script);

         SkipRestBlocks(script);
         return result;
      }

      private static string CreateExceptionStack(string exceptionName, int lowestStackLevel)
      {
         var result = "";
         var stack = ParserFunction.ExecutionStack;
         var level = stack.Count;
         foreach (var stackLevel in stack)
         {
            if (level-- < lowestStackLevel) break;
            if (string.IsNullOrWhiteSpace(stackLevel.Name)) continue;
            result += Environment.NewLine + "  " + stackLevel.Name + "()";
         }

         if (!string.IsNullOrWhiteSpace(result)) result = " --> " + exceptionName + result;

         return result;
      }

      private Variable ProcessBlock(ParsingScript script)
      {
         var blockStart = script.Pointer;
         Variable result = null;

         while (script.StillValid())
         {
            var endGroupRead = script.GoToNextStatement();
            if (endGroupRead > 0) return result != null ? result : new Variable();

            if (!script.StillValid())
               throw new ArgumentException("Couldn't process block [" +
                                           script.Substr(blockStart, Constants.MaxCharsToShow) + "]");
            result = script.ExecuteTo();

            if (result.IsReturn ||
                result.Type == Variable.VarType.Break ||
                result.Type == Variable.VarType.Continue) return result;
         }
         return result;
      }

      private void SkipBlock(ParsingScript script)
      {
         var blockStart = script.Pointer;
         var startCount = 0;
         var endCount = 0;
         while (startCount == 0 || startCount > endCount)
         {
            if (!script.StillValid())
               throw new ArgumentException("Couldn't skip block [" +
                                           script.Substr(blockStart, Constants.MaxCharsToShow) + "]");
            var currentChar = script.CurrentAndForward();
            switch (currentChar)
            {
               case Constants.StartGroup:
                  startCount++;
                  break;
               case Constants.EndGroup:
                  endCount++;
                  break;
            }
         }

         if (startCount != endCount) throw new ArgumentException("Mismatched parentheses");
      }

      private void SkipRestBlocks(ParsingScript script)
      {
         while (script.StillValid())
         {
            var endOfToken = script.Pointer;
            var nextData = new ParsingScript(script);
            var nextToken = Utils.GetNextToken(nextData);
            if (!Constants.ElseIfList.Contains(nextToken) &&
                !Constants.ElseList.Contains(nextToken)) return;
            script.Pointer = nextData.Pointer;
            SkipBlock(script);
         }
      }
   }
}