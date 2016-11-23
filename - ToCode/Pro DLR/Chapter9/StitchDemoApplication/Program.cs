/*
 * Copyright 2010 Chaur Wu.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Stitch.Ast;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Scripting.Hosting;
using PowerShellStitchPlugin;
using Antlr.Runtime;
using Stitch;

namespace StitchDemoApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTestScript1UsingDlrHostingAPI();
            RunTaskDependencyExample();
            RunParserExample();
            RunTestScript1();
            RunTestScript2();
            RunTestScript3();
            RunTestScript4();
            Console.ReadLine();
        }

        private static void RunTestScript1()
        {
            StitchScriptEngine engine = new StitchScriptEngine(
                ExecutionMode.Sequential, new ILanguagePlugin[] { new PowerShellPlugin() });

            engine.RunScriptFile(@"Scripts\testScript1.st");
        }

        private static void RunTestScript2()
        {
            StitchScriptEngine engine = new StitchScriptEngine(
                ExecutionMode.Sequential, new ILanguagePlugin[] { new PowerShellPlugin() });

            engine.RunScriptFile(@"Scripts\testScript2.st");
        }

        private static void RunTestScript3()
        {
            StitchScriptEngine engine = new StitchScriptEngine(
                ExecutionMode.Sequential, new ILanguagePlugin[] { new PowerShellPlugin() });

            engine.RunScriptFile(@"Scripts\testScript3.st");
        }

        private static void RunTestScript4()
        {
            StitchScriptEngine engine = new StitchScriptEngine(
                    ExecutionMode.Sequential, new ILanguagePlugin[] { new PowerShellPlugin() });

            Stopwatch stopwatch = Stopwatch.StartNew();
            engine.RunScriptFile(@"Scripts\testScript4.st");
            stopwatch.Stop();

            Console.WriteLine("Sequential runner takes {0} milliseconds.", stopwatch.ElapsedMilliseconds);

            engine = new StitchScriptEngine(
                    ExecutionMode.ParallelWaitAll, new ILanguagePlugin[] { new PowerShellPlugin() });

            stopwatch = Stopwatch.StartNew();
            engine.RunScriptFile(@"Scripts\testScript4.st");
            stopwatch.Stop();

            Console.WriteLine("Parallel runner takes {0} milliseconds.", stopwatch.ElapsedMilliseconds); 
        }

        private static void RunTestScript1UsingDlrHostingAPI()
        {
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            ScriptEngine engine = scriptRuntime.GetEngine("Stitch");
            ScriptSource script = engine.CreateScriptSourceFromFile(@"Scripts\testScript1.st");
            script.Execute(engine.CreateScope());
        }

        private static void RunTaskDependencyExample()
        {
            Task task1 = Task.Factory.StartNew(() => { Console.WriteLine("task 1"); });
            Task task2 = Task.Factory.StartNew(() => { Console.WriteLine("task 2"); });
            Task task3 = Task.Factory.StartNew(() => { Console.WriteLine("task 3"); });

            Task task4 = Task.Factory.ContinueWhenAll(new[] {task1}, 
                tasks => { Console.WriteLine("task 4"); });
            Task task5 = Task.Factory.ContinueWhenAll(new[] { task2 }, 
                tasks => { Console.WriteLine("task 5"); });
            Task task6 = Task.Factory.ContinueWhenAll(new[] { task4 }, 
                tasks => { Console.WriteLine("task 6"); });
            Task task7 = Task.Factory.ContinueWhenAll(new[] { task2, task4, task5 }, 
                tasks => { Console.WriteLine("task 7"); });
            Task task8 = Task.Factory.ContinueWhenAll(new[] { task3, task5 }, 
                tasks => { Console.WriteLine("task 8"); });
        }

        static void RunParserExample()
        {
            ICharStream input = new ANTLRFileStream(@"Scripts\testScript1.st");
            StitchLexer lexer = new StitchLexer(input);
            ITokenStream tokenStream = new CommonTokenStream(lexer);
            StitchParser parser = new StitchParser(tokenStream);
            IList<IFunction> functions = parser.program();
            Console.WriteLine("There are {0} scripts in the source file.", functions.Count);
        }
    }
}
