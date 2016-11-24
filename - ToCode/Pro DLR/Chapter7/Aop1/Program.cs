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
using Spring.Context;
using Spring.Context.Support;

namespace Aop1
{
    class Program
    {
        static void Main(string[] args)
        {
            RunDynamicObjectExample();
            //RunStaticObjectExample();
            Console.ReadLine();
        }

        private static void RunStaticObjectExample()
        {
            IApplicationContext context = new XmlApplicationContext("application-config.xml");
            IEmployee employee = (IEmployee)context["employeeBob"];
            Console.WriteLine("Employee is {0} years old.", employee.Age);
        }

        private static void RunDynamicObjectExample()
        {
            dynamic customer = new Customer();
            Console.WriteLine("Customer is {0} years old.", customer.Age);
        }
    }
}
