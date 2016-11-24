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
using Microsoft.Scripting.Hosting;

namespace InteropBinderExamples
{
    class RubyExampleCode
    {
        private static dynamic productClass;
        private static ScriptEngine rbEngine;

        static RubyExampleCode()
        {
            rbEngine = IronRuby.Ruby.CreateEngine();
            ScriptScope scope = rbEngine.ExecuteFile("RubyProduct.rb");
            productClass = rbEngine.Runtime.Globals.GetVariable("RubyProduct");
        }

        public static dynamic CreateRubyProduct(String name, int price)
        {
            return rbEngine.Operations.CreateInstance(productClass, name, price);
        }
    }
}
