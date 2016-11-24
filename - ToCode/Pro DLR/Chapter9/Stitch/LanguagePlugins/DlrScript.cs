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
using Microsoft.Scripting;

namespace Stitch
{
    class DlrScript : IScript
    {
        private static ScriptRuntime runtime = ScriptRuntime.CreateFromConfiguration();

        private string lang;
        private string code;

        public DlrScript(string lang, string code)
        {
            this.lang = lang;
            this.code = code;
        }

        public IDictionary<String, object> Execute(IDictionary<String, object> dictionary)
        {
            ScriptScope scope = runtime.CreateScope();
            foreach (var item in dictionary)
                scope.SetVariable(item.Key, item.Value);

            ScriptEngine engine;
            lock (runtime)
            {
                engine = runtime.GetEngine(lang);
            }
            ScriptSource source = engine.CreateScriptSourceFromString(code,
                SourceCodeKind.Statements);
            source.Execute(scope);
            IDictionary<String, object> result = new Dictionary<String, object>();
            foreach (var item in scope.GetItems())
	            result.Add(item.Key, item.Value);

            return result;
        }
    }
}
