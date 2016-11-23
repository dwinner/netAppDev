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
using Stitch.Ast;
using System.Threading.Tasks;

namespace Stitch
{
    class ParallelFunctionExecutionCoordinator : IFunctionExecutionCoordinator
    {
        private bool waitAll = false;

        public ParallelFunctionExecutionCoordinator(bool waitAll)
        {
            this.waitAll = waitAll;
        }

        public void RunScripts(IList<IFunction> functions, ILanguageRegistry registry)
        {
            IList<ParallelScriptRunner> scriptRunners = this.CreateScriptRunners(functions, registry);

            foreach (var scriptRunner in scriptRunners)
                scriptRunner.Run();

            IEnumerable<Task<IDictionary<String, Object>>> tasks = 
                from scriptRunner in scriptRunners select scriptRunner.RunnerTask;

            if (waitAll)
                Task.WaitAll(tasks.ToArray());
        }

        private IList<ParallelScriptRunner> CreateScriptRunners(IList<Ast.IFunction> functions, ILanguageRegistry registry)
        {
            IDictionary<String, ParallelScriptRunner> returnValueToRunnerDict = new Dictionary<String, ParallelScriptRunner>();
            IList<ParallelScriptRunner> scriptRunners = new List<ParallelScriptRunner>();

            foreach (var function in functions)
            {
                IScript script = registry.CreateScript(function);
                ParallelScriptRunner scriptRunner = new ParallelScriptRunner(script, function.InputParameters);
                scriptRunners.Add(scriptRunner);
                foreach (var returnValue in function.ReturnValues)
                {
                    returnValueToRunnerDict.Add(returnValue, scriptRunner);
                }
            }

            for (int i = 0; i < functions.Count; i++)
            {
                ParallelScriptRunner scriptRunner = scriptRunners[i];
                foreach (var item in functions[i].InputParameters)
                {
                    if (returnValueToRunnerDict.ContainsKey(item))
                        scriptRunner.AddPrerequisite(returnValueToRunnerDict[item]);
                }
            }

            return scriptRunners;
        }
    }
}
