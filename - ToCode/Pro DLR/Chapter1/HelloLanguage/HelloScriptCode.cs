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
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;

namespace HelloLanguage
{
    public class HelloScriptCode : ScriptCode
    {
        public HelloScriptCode(SourceUnit sourceUnit) : base(sourceUnit)
        { }

        public override object Run(Scope scope)
        {
            Console.WriteLine("Hello");
            return null;
        }
    }
}
