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
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using Microsoft.Scripting;

namespace HostingExamples
{
    class LevelTwoExamples
    {
        public static void LoadAssembliesIntoScriptRuntime()
        {
            Console.WriteLine("Running LoadAssembliesIntoScriptRuntime");
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            scriptRuntime.LoadAssembly(Assembly.Load("PresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
            scriptRuntime.LoadAssembly(Assembly.Load("PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
            ScriptScope scope = scriptRuntime.ExecuteFile(@"Python\simpleWpf.py");
        }

        public static void RunCompiledCodeExample()
        {
            Console.WriteLine("Running RunCompiledCodeExample");
            ScriptEngine pyEngine = ScriptRuntime.CreateFromConfiguration().GetEngine("python");
            ScriptSource source = pyEngine.CreateScriptSourceFromFile(@"Python\simple1.py");

            ScriptScope scope = pyEngine.CreateScope();
            scope.SetVariable("x", 2);
            scope.SetVariable("customer", new Customer("Bob", 30));

            CompiledCode compiledCode = source.Compile();
            compiledCode.Execute(scope);
        }

        public static void PassObjectsToPythonViaLanguageNeutralScope()
        {
            Console.WriteLine("Running PassObjectsToPythonViaLanguageNeutralScope");
            Customer customer = new Customer("Bob", 30);

            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            ScriptScope neutralScope = scriptRuntime.CreateScope();

            neutralScope.SetVariable("x", 2);
            neutralScope.SetVariable("customer", customer);

            ScriptEngine scriptEngine = scriptRuntime.GetEngine("python");

            //Changing the value of x in Python code will change the x in the neutral scope
            //even if x is a value type object. This is because x is put back into the neutral scope.
            ScriptScope resultScope = scriptEngine.ExecuteFile(@"Python\simple1.py", neutralScope);

            if (resultScope == neutralScope)
                Console.WriteLine("scope and neutral scope are the same object."); 

            Console.WriteLine("Items in the neutral scope: ");
            foreach (var item in neutralScope.GetItems())
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
        }

        public static void RunLanguageNeutralScopeExample()
        {
            Console.WriteLine("Running RunLanguageNeutralScopeExample");
            ScriptRuntime runtime = ScriptRuntime.CreateFromConfiguration();
            ScriptScope scope = runtime.CreateScope();
            if (!scope.ContainsVariable("__doc__"))
                Console.WriteLine("scope does not contain __doc__ variable.");
        }

        public static void RunPythonEngineScopeExample()
        {
            Console.WriteLine("Running RunPythonEngineScopeExample");
            ScriptRuntime runtime = ScriptRuntime.CreateFromConfiguration();
            ScriptEngine pyEngine = runtime.GetEngine("python");
            ScriptScope scope = pyEngine.CreateScope();

            if (scope.ContainsVariable("__doc__"))
                Console.WriteLine("scope contains __doc__ variable.");

            String docString = scope.GetVariable("__doc__");
            Console.WriteLine("doc string is {0}", docString);
        }

        public static void CreateInstanceOfPythonClassInCSharp()
        {
            Console.WriteLine("Running CreateInstanceOfPythonClassInCSharp");
            ScriptEngine pyEngine = ScriptRuntime.CreateFromConfiguration().GetEngine("python");
            string pyCode = @"class ClassA(object): pass";
            ScriptSource source = pyEngine.CreateScriptSourceFromString(pyCode,
                SourceCodeKind.Statements);
            ScriptScope scope = pyEngine.CreateScope();
            source.Execute(scope);
            dynamic ClassA = scope.GetVariable("ClassA");
            object objectA1 = pyEngine.Operations.CreateInstance(ClassA);
            object objectA2 = ClassA();
        }

        public static void CallPythonFunctionFromCSharpUsingCompiledCode()
        {
            Console.WriteLine("Running CallPythonFunctionFromCSharpUsingCompiledCode");
            ScriptEngine pyEngine = ScriptRuntime.CreateFromConfiguration().GetEngine("python");
            string pyFunc = @"def isodd(n): return 1 == n % 2;";
            ScriptSource source = pyEngine.CreateScriptSourceFromString(pyFunc,
                SourceCodeKind.Statements);
            ScriptScope scope = pyEngine.CreateScope();
            CompiledCode compiledCode = source.Compile();
            compiledCode.Execute(scope);
            Func<int, bool> IsOdd = scope.GetVariable<Func<int, bool>>("isodd");
            bool result = IsOdd(3);
            Console.WriteLine("Is 3 an odd number? {0}", result);
        }

        public static void CallPythonFunctionFromCSharpUsingScriptSource()
        {
            Console.WriteLine("Running CallPythonFunctionFromCSharpUsingScriptSource");
            ScriptEngine pyEngine = ScriptRuntime.CreateFromConfiguration().GetEngine("python");
            string pyFunc = @"def isodd(n): return 1 == n % 2;";
            ScriptSource source = pyEngine.CreateScriptSourceFromString(pyFunc,
                SourceCodeKind.Statements);
            ScriptScope scope = pyEngine.CreateScope();
            source.Execute(scope);
            Func<int, bool> IsOdd = scope.GetVariable<Func<int, bool>>("isodd");
            bool result = IsOdd(3);
            Console.WriteLine("Is 3 an odd number? {0}", result);
        }
    }
}
