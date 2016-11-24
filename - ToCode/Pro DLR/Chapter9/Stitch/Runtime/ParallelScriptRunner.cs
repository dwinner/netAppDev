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
    class ParallelScriptRunner : IScriptRunner
    {
        private IScript script;
        private IList<ParallelScriptRunner> prerequisites = new List<ParallelScriptRunner>();
        private Task<IDictionary<String, Object>> task;
        private IList<string> inputParameters;

        public ParallelScriptRunner(IScript script, IList<string> inputParameters)
        {
            this.script = script;
            this.inputParameters = inputParameters;
        }

        internal Task<IDictionary<String, Object>> RunnerTask
        {
            get { return task; }
        }

        internal void AddPrerequisite(ParallelScriptRunner prerequisite)
        {
            if (!prerequisites.Contains(prerequisite))
                prerequisites.Add(prerequisite);
        }

        Task<IDictionary<String, Object>> StartTask()
        {
            if (task != null)
                return task;

            if (prerequisites.Count == 0)
            {
                task = Task.Factory.StartNew<IDictionary<String, Object>>(() =>
                {
                    IDictionary<String, object> scope = new Dictionary<String, object>();
                    return this.script.Execute(scope);
                });

                return task;
            }

            List<Task<IDictionary<String, Object>>> taskList = new List<Task<IDictionary<string, object>>>();
            foreach (var prerequisite in prerequisites)
                taskList.Add(prerequisite.StartTask());

            task = Task.Factory.ContinueWhenAll(taskList.ToArray(),
                    (tasks) =>
                    {
                        IDictionary<String, object> scope = new Dictionary<String, object>();
                        foreach (var prerequisiteTask in tasks)
                        {
                            foreach (var item in prerequisiteTask.Result)
                            {
                                if (!scope.ContainsKey(item.Key) &&
                                this.inputParameters.Contains(item.Key))
                                    scope.Add(item);
                            }
                        }

                        return this.script.Execute(scope);
                    });

            return task;
        }

        public void Run()
        {
            StartTask();
        }
    }
}
