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
using System.Threading.Tasks;

namespace Stitch
{
    class SequentialScriptRunner : IScriptRunner
    {
        private IScript script;
        private IDictionary<String, object> scope;

        public SequentialScriptRunner(IScript script, IDictionary<string, object> scope)
        {
            this.script = script;
            this.scope = scope;
        }

        public void Run()
        {
            IDictionary<String, object> result = this.script.Execute(scope);
            foreach (var item in result)
            {
                if (!scope.ContainsKey(item.Key))
                    scope.Add(item.Key, item.Value);
            }
        }
    }
}
