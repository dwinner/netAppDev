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

namespace HelloLanguage
{
    public class HelloContext : LanguageContext
    {
        /// <summary>
        /// We use App.config to set up our language context. DLR expects a constructor like this
        /// when it uses reflection to create an instance of our language context.
        /// </summary>
        /// <param name="domainManager"></param>
        /// <param name="options"></param>
        public HelloContext(ScriptDomainManager domainManager, IDictionary<string, object> options)
            : base(domainManager)
        { }

        public override ScriptCode CompileSourceCode(SourceUnit sourceUnit, CompilerOptions options, ErrorSink errorSink)
        {
            return new HelloScriptCode(sourceUnit);
        }
    }
}
