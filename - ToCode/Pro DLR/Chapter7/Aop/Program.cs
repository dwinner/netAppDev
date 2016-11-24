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
using Spring.Context.Support;
using Spring.Context;
using Spring.Aop;
using System.Collections;
using System.Reflection;
using AopAlliance.Aop;

namespace Aop
{
    class Program
    {
        private static IApplicationContext context;
        
        static void Main(string[] args)
        {
            init();
            //RunAdvisorChainFactoryExample();
            RunAopExample();
            Console.ReadLine();
        }

        private static void init()
        {
            context = new XmlApplicationContext("application-config.xml");
            AdvisorChainFactory.Context = context;
        }

        private static void RunAdvisorChainFactoryExample()
        {
            Type targetType = typeof(Customer);
            MethodInfo method = targetType.GetProperty("Age").GetGetMethod();
            IList<IAdvice> interceptors = AdvisorChainFactory.GetInterceptors(method, targetType);
            foreach (var interceptor in interceptors)
                Console.WriteLine("type of matching interceptor is {0}", interceptor.GetType().Name);
        }

        private static void RunAopExample()
        {
            dynamic customer = new Customer("John", 5);
            Console.WriteLine("Customer {0} is {1} years old.\n", customer.Name, customer.Age);

            IEmployee employee = (IEmployee)context["employeeBob"];
            Console.WriteLine("Employee {0} is {1} years old.", employee.Name, employee.Age);

            //dynamic employee2 = new Employee();
            //employee2.Name = "Bill";
            //employee2.Age = 20;
            //Console.WriteLine("Employee {0} is {1} years old.", employee2.Name, employee2.Age);
        }
    }
}
