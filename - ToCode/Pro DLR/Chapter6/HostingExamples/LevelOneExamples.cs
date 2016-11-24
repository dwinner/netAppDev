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
using Microsoft.Scripting.Hosting;

namespace HostingExamples
{
    class LevelOneExamples
    {
        public static void GetPythonFunctionUsingScriptRuntime()
        {
            Console.WriteLine("Running GetPythonFunctionUsingScriptRuntime");
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            ScriptScope scope = scriptRuntime.ExecuteFile(@"Python\helloFunc.py");
            Action hello = scope.GetVariable<Action>("hello");
            hello();
            scriptRuntime.Shutdown();
        }

        public static void UseScriptScopeAsDynamicObject()
        {
            Console.WriteLine("Running UseScriptScopeAsDynamicObject");
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            dynamic scope = scriptRuntime.ExecuteFile(@"Python\helloFunc.py");
            Action hello = scope.hello;
            hello();
            scriptRuntime.Shutdown();
        }

        public static void PassObjectsToPythonViaGlobals()
        {
            Console.WriteLine("Running PassObjectsToPythonViaGlobals");
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            Customer customer = new Customer("Bob", 30);

            scriptRuntime.Globals.SetVariable("x", 2);
            scriptRuntime.Globals.SetVariable("customer", customer);

            //Remember to set Simple.py as 'Content, copy always'.
            ScriptScope resultScope = scriptRuntime.ExecuteFile(@"Python\simple2.py");
    
            //Changing the value of x in Python code will not change the x in Globals.
            //This is because x is a value type and also the new x in Python code 
            //is not put back into Globals.
            int x = scriptRuntime.Globals.GetVariable("x");
            Console.WriteLine("x is {0}", x);
            Console.WriteLine("Bob's age is {0}", customer.Age);

            Console.WriteLine("Items in global scope: ");
            DumpScriptScope(scriptRuntime.Globals);
            
            Console.WriteLine("Items in the returned scope: ");
            DumpScriptScope(resultScope);
            scriptRuntime.Shutdown();
        }

        private static void DumpScriptScope(ScriptScope scope)
        {
            foreach (var item in scope.GetItems())
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
        }

        public static void PassObjectModelToScript()
        {
            Console.WriteLine("Running PassObjectModelToScript");
            Customer customer = new Customer("Bob", 30);
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            scriptRuntime.Globals.SetVariable("customer", customer);
            ScriptScope scope = scriptRuntime.ExecuteFile(@"Python\simpleWpf2.py");
        }
    }
}
