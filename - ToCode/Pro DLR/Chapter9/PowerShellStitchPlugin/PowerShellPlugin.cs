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
using Stitch;
using Stitch.Ast;

namespace PowerShellStitchPlugin
{
    public class PowerShellPlugin : ILanguagePlugin
    {
        private IList<string> fileExtensions = new List<string>(new string[] { ".ps" });
        private IList<string> languageNames = new List<string>(new string[] { "PowerShell" });

        public IList<string> FileExtensions
        {
            get { return fileExtensions; }
        }

        public IList<string> LanguageNames
        {
            get { return languageNames; }
        }

        public IScript CreateScript(IFunction function)
        {
            String returnValue = null;
            if (function.ReturnValues.Count > 0)
                returnValue = function.ReturnValues[0];

            return new PowerShellScript(function.Code, returnValue);
        }
    }
}
