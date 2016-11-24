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

namespace Stitch.Ast
{
    class FunctionSignature
    {
        private string name;
        private IList<String> inputArguments;
        private IList<String> returnValues;

        public FunctionSignature(string name, IList<string> inputArguments, IList<string> returnValues)
        {
            this.name = name;
            this.inputArguments = inputArguments==null? new List<String>() : inputArguments;
            this.returnValues = returnValues == null ? new List<String>() : returnValues;
        }

        public IList<string> Parameters 
        { 
            get { return inputArguments; } 
        }

        public IList<string> ReturnValues 
        { 
            get { return returnValues; } 
        }
    }
}
