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
using IronPython.Hosting;
using IronRuby;

namespace LanguageSpecificHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            CSHostsIronPython();
            PrintPythonLanguageSetup();
            CSHostsIronRuby();
            Console.ReadLine();
        }

        private static void CSHostsIronPython()
        {
            ScriptEngine engine = Python.CreateEngine();
            engine.Execute("print \"hello\"");
        }

        private static void CSHostsIronRuby()
        {
            ScriptEngine engine = Ruby.CreateEngine();
            engine.Execute("puts \"hello\"");
        }

        private static void PrintPythonLanguageSetup()
        {
            LanguageSetup pythonSetup = Python.CreateLanguageSetup(new Dictionary<string, object>());
            Console.WriteLine(pythonSetup.TypeName);
            LanguageSetup rubySetup = Ruby.CreateRubySetup();
        }
    }
}
