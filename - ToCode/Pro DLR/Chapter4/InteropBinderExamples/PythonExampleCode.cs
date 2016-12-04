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

namespace InteropBinderExamples
{
    class PythonExampleCode
    {
        public static Func<dynamic, dynamic, int> AddPriceDelegate;
        public static Action<dynamic> PrintClassNameDelegate;
        public static dynamic Bob;
        
        static PythonExampleCode()
        {
            ScriptEngine pyEngine = IronPython.Hosting.Python.CreateEngine();
            ScriptScope scope = pyEngine.ExecuteFile("pythonExampleCode.py");
            AddPriceDelegate = scope.GetVariable("addPrice");
            PrintClassNameDelegate = scope.GetVariable("printClassName");
            Bob = scope.GetVariable("bob");
        }
    }
}
