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
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting;
using System.Reflection;

namespace Stitch
{
    public class StitchContext : LanguageContext
    {
        private IList<ILanguagePlugin> plugins;
        private ExecutionMode executionMode;

        /// <summary>
        /// We will use App.config to set up our language context. DLR expects a constructor like this
        /// when it uses reflection to create an instance of our language context.
        /// </summary>
        /// <param name="domainManager"></param>
        /// <param name="options"></param>
        public StitchContext(ScriptDomainManager domainManager, IDictionary<string, object> options)
            : base(domainManager)
        {
            plugins = new List<ILanguagePlugin>();
            foreach (var option in options)
            {
                if (option.Key.Equals("plugin")) 
                    plugins.Add(LoadPlugin((string) option.Value));
                else if (option.Key.Equals("executionMode"))
                    executionMode = (ExecutionMode) Enum.Parse(typeof(ExecutionMode), (string) option.Value);
            }
        }

        public override ScriptCode CompileSourceCode(SourceUnit sourceUnit, CompilerOptions options, ErrorSink errorSink)
        {
            return new StitchScriptCode(sourceUnit, plugins, executionMode);
        }

        private ILanguagePlugin LoadPlugin(String assemblyTypeName) 
        {
            int i = assemblyTypeName.IndexOf(",");
            string typeName = assemblyTypeName.Substring(0, i).Trim();
            string assemblyName = assemblyTypeName.Substring(i + 1).Trim();
            var assembly = Assembly.Load(assemblyName);
            Type type = assembly.GetType(typeName);
            return (ILanguagePlugin)Activator.CreateInstance(type);
        }
    }
}
