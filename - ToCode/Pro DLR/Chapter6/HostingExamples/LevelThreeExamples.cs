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
using System.Runtime.Remoting;

namespace HostingExamples
{
    class LevelThreeExamples
    {
        public static void RunScriptRuntimeSetupExample()
        {
            Console.WriteLine("Running RunScriptRuntimeSetupExample");
            
            //Use rubySetup if you want the script runtime to support Ruby.
            //LanguageSetup rubySetup = new LanguageSetup(
            //    typeName: "IronRuby.Runtime.RubyContext, IronRuby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", 
            //    displayName: "IronRuby", 
            //    names: new String[]{"IronRuby", "Ruby", "rb"}, 
            //    fileExtensions: new String[]{".rb"});
            //setup.LanguageSetups.Add(rubySetup);

            LanguageSetup pythonSetup = new LanguageSetup(
                typeName: "IronPython.Runtime.PythonContext,IronPython, Version=2.6.10920.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35",
                displayName: "IronPython",
                names: new String[] { "IronPython", "Python", "py" },
                fileExtensions: new String[] { ".py" });

            ScriptRuntimeSetup setup = new ScriptRuntimeSetup();
            setup.LanguageSetups.Add(pythonSetup);
            ScriptRuntime scriptRuntime = new ScriptRuntime(setup);
            ScriptScope scope = scriptRuntime.ExecuteFile(@"Python\hello.py");
        }

        public static void UseCustomScriptHost()
        {
            Console.WriteLine("Running UseCustomScriptHost");
            ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
            setup.HostType = typeof(SimpleHostType);
            ScriptRuntime scriptRuntime = new ScriptRuntime(setup);
            ScriptScope scope = scriptRuntime.ExecuteFile(@"hello.py");
        }

        //Useful for example when you are building a tool that provides intellisense for
        //Python objects.
        public static void GetDocStringOfPythonFunctionUsingObjectOperations()
        {
            Console.WriteLine("Running GetDocStringOfPythonFunctionUsingObjectOperations");
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            ScriptEngine pyEngine = scriptRuntime.GetEngine("python");
            ScriptScope scope = pyEngine.ExecuteFile(@"Python\helloFunc.py");
            object helloFunction = scope.GetVariable("hello");

            IList<String> helloFuncMembers = pyEngine.Operations.GetMemberNames(helloFunction);
            
            foreach (var item in helloFuncMembers)
                Console.WriteLine(item);
            
            Console.WriteLine(pyEngine.Operations.GetMember(helloFunction, "__doc__"));
        }

        public static void CreateRemoteScriptRuntime()
        {
            Console.WriteLine("Running CreateRemoteScriptRuntime");
            AppDomain appDomain = AppDomain.CreateDomain("ScriptDomain");
            ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateRemote(appDomain, setup);
            ScriptEngine pyEngine = scriptRuntime.GetEngine("python");
            ScriptScope scope = pyEngine.ExecuteFile(@"Python\helloFunc.py");
            ObjectHandle helloFunction = scope.GetVariableHandle("hello");
            pyEngine.Operations.Invoke(helloFunction, new object[]{});
        }
    }
}
