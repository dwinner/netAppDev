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
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;

namespace DynamicObjectExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            RunDynamicObjectAsStaticAndDynamicExample();
            RunCustomerLateBindingInCSharpExamples();
            RunCustomerLateBindingInPythonExamples();
            RunDynamicObjectExample();
            Console.ReadLine();
        }

        private static void RunDynamicObjectExample()
        {
            dynamic employee = new Employee();
            Console.WriteLine("Employee's salary is {0} dollars.", employee.Salary);
            Console.WriteLine("Employee is {0} years old.", employee.Age);
            Console.WriteLine("Employee's bonus is {0} dollars.", employee.calculateBonus(2));
        }

        private static void RunDynamicObjectAsStaticAndDynamicExample()
        {
            dynamic bob = new Customer("Bob", 30);
            Console.WriteLine("Bob is {0} years old.", bob.Age);

            Customer bill = new Customer("Bob", 30);
            Console.WriteLine("Bill is {0} years old.", bill.Age);
        }

        private static void RunCustomerLateBindingInPythonExamples()
        {
            dynamic bob = new Customer("Bob", 30);
            ScriptEngine pyEngine = IronPython.Hosting.Python.CreateEngine();
            ScriptSource source = pyEngine.CreateScriptSourceFromFile("CustomerLateBinding.py");
            ScriptScope scope = pyEngine.CreateScope();
            scope.SetVariable("bob", bob);
            source.Execute(scope);
        }

        private static void RunCustomerLateBindingInCSharpExamples()
        {
            dynamic bob = new Customer("Bob", 30);
            Console.WriteLine("bob.Foo(100): {0}", bob.Foo(100)); //InvokeMember
            Console.WriteLine("bob(): {0}", bob()); //Invoke
            Console.WriteLine("bob[100]: {0}", bob[100]); //GetIndex
            Console.WriteLine("(bob[100] = 10): {0}", (bob[100] = 10)); //SetIndex
            Console.WriteLine("(int) bob: {0}", (int)bob); //Convert
            Console.WriteLine("(bob.Age = 40): {0}", (bob.Age = 40)); //SetMember
            Console.WriteLine("bob.Age: {0}", bob.Age);  //GetMember
            Console.WriteLine("bob + 100: {0}", bob + 100); //BinaryOperation
            Console.WriteLine("++bob: {0}", ++bob); //UnaryOperation
        }
    }
}
