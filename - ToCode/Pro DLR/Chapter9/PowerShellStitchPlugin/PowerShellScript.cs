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
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Collections.ObjectModel;
using Stitch;

namespace PowerShellStitchPlugin
{
    class PowerShellScript : IScript
    {
        private string code;
        private string returnValue;

        public PowerShellScript(string code, string returnValue)
        {
            this.code = code;
            this.returnValue = returnValue;
        }

        public IDictionary<String, object> Execute(IDictionary<String, object> scope)
        {
            //need to pass objects in scope to power shell
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(code);
            Collection<PSObject> results = pipeline.Invoke();
            runspace.Close();
            IDictionary<String, object> result = new Dictionary<String, object>();
            result.Add(returnValue, results);
            return result;
        }
    }
}
