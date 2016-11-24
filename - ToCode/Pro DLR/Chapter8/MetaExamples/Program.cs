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

namespace MetaExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            RunRubyMetaExamples();
            RunPythonMetaExamples();
            RunMetaLibExample();
            Console.ReadLine();
        }

        private static void RunPythonMetaExamples()
        {
            ScriptEngine engine = IronPython.Hosting.Python.CreateEngine();
            engine.ExecuteFile(@"Python\metaExamples.py");
        }

        private static void RunRubyMetaExamples()
        {
            ScriptEngine engine = IronRuby.Ruby.CreateEngine();
            engine.ExecuteFile(@"Ruby\metaExamples.rb");
        }

        private static void RunMetaLibExample()
        {
            dynamic customerClass = Customer.CLASS;
            dynamic bob = new Customer("Bob", 26);
            dynamic mary = new Customer("Mary", 30);

            Action<dynamic, dynamic> SetSpouse = (self, spouse) =>
            {
                if (self.Spouse != spouse)
                {
                    self.Spouse = spouse;
                    spouse.Spouse = self;
                }
            };

            //This can take place before or after john and mary are created.
            customerClass.Spouse = null;
            customerClass.SetSpouse = SetSpouse;
            bob.SetSpouse(bob, mary);

            Console.WriteLine("Bob's spouse is {0}.", bob.Spouse);
            Console.WriteLine("Mary's spouse is {0}.", mary.Spouse);

            bob.CalculateLateFee = (Func<int>) (() => { return 200; });
            mary.CalculateLateFee = (Func<int>)(() => { return 100; });

            Console.WriteLine("Bob's late fee is {0}.", bob.CalculateLateFee());
            Console.WriteLine("Mary's late fee is {0}.", mary.CalculateLateFee());
        }
    }
}
