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
using Microsoft.Scripting.Hosting;

namespace HelloLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteHelloCode();
            RunHelloREPL(args);
        }

        private static void RunHelloREPL(string[] args)
        {
            (new HelloConsole()).Run(args);
        }

        private static void ExecuteHelloCode()
        {
            ScriptRuntime scriptRuntime = ScriptRuntime.CreateFromConfiguration();
            ScriptEngine engine = scriptRuntime.GetEngine("Hello");
            ScriptSource script = engine.CreateScriptSourceFromString("any text");
            script.Execute(engine.CreateScope());
            Console.ReadLine();
        }
    }
}
