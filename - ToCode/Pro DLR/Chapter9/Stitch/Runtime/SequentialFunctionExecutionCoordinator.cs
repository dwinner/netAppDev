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
using Stitch.Ast;

namespace Stitch
{
    class SequentialFunctionExecutionCoordinator : IFunctionExecutionCoordinator
    {
       
        public void RunScripts(IList<IFunction> functions, ILanguageRegistry registry)
        {
            IList<IScriptRunner> scriptRunners = this.CreateScriptRunners(functions, registry);

            foreach (var scriptRunner in scriptRunners)
                scriptRunner.Run();
        }
        
        private IList<IScriptRunner> CreateScriptRunners(IList<Ast.IFunction> functions, ILanguageRegistry registry)
        {
            IDictionary<String, object> scope = new Dictionary<string, object>();
            
            IDictionary<String, SequentialScriptRunner> returnToScriptRunner = new Dictionary<String, SequentialScriptRunner>();
            IList<IScriptRunner> scriptRunners = new List<IScriptRunner>();
                
            foreach (var function in functions)
            {
                IScript script = registry.CreateScript(function);
                SequentialScriptRunner scriptRunner = new SequentialScriptRunner(script, scope);
                scriptRunners.Add(scriptRunner);
            }

            return scriptRunners;
        }

    }
}
