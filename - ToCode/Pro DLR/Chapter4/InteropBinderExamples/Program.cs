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
using System.Runtime.CompilerServices;
using System.Dynamic;

namespace InteropBinderExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            RunRubyClassInstantiationExample();
            RunAddPriceExample();
            RunPrintClassNameExample();
            RunGetMemberBinderAndStaticObjectExample();
            RunGetMemberBinderAndDynamicObjectExample();
            Console.ReadLine();
        }

        #region GetMemberBinder Examples

        private static void RunGetMemberBinderAndDynamicObjectExample()
        {
            CallSiteBinder binder = new SimpleGetMemberBinder("Name", false);

            CallSite<Func<CallSite, object, object>> site =
              CallSite<Func<CallSite, object, object>>.Create(binder);

            object name = site.Target(site, PythonExampleCode.Bob);
            Console.WriteLine("Customer name is {0}.", name);
        }

        private static void RunGetMemberBinderAndStaticObjectExample()
        {
            CallSiteBinder binder = new SimpleGetMemberBinder("name", false);

            CallSite<Func<CallSite, object, object>> site =
              CallSite<Func<CallSite, object, object>>.Create(binder);

            object handClapper = new CSharpProduct("Hand Clapper", 6);
            object name = site.Target(site, handClapper);
            Console.WriteLine("Product name is {0}.", name);
        }
        #endregion

        private static void RunRubyClassInstantiationExample()
        {
            dynamic stretchString = RubyExampleCode.CreateRubyProduct("Stretch String", 7);
            Console.WriteLine("Product {0} is {1} dollars.", stretchString.name, stretchString.price);
        }

        private static void RunPrintClassNameExample()
        {
            dynamic stretchString = RubyExampleCode.CreateRubyProduct("Stretch String", 7);
            PythonExampleCode.PrintClassNameDelegate(stretchString);
            
            dynamic handClapper = new CSharpProduct("Hand Clapper", 6);
            PythonExampleCode.PrintClassNameDelegate(handClapper);
        }

        private static void RunAddPriceExample()
        {
            dynamic stretchString = RubyExampleCode.CreateRubyProduct("Stretch String", 7);
            dynamic handClapper = new CSharpProduct("Hand Clapper", 6);
            int total = PythonExampleCode.AddPriceDelegate(stretchString, handClapper);
            Console.WriteLine("Total is {0}.", total);
        }
    }
}
