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

namespace QueryProviderExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            RunCustomerDaoExample();
            RunDynamicDaoExample();
            RunGeneratedDaoExample();
            Console.ReadLine();
        }
        
        private static void RunCustomerDaoExample()
        {
            ICustomerDao customerDao = new CustomerDao(DataStore.GetCustomerQueryProvider());
            IEnumerable<Customer> customers = customerDao.FindByFirstName("Bob");

            foreach (var item in customers)
                Console.WriteLine(item);

            customers = customerDao.FindByLastName("Jones");

            foreach (var item in customers)
                Console.WriteLine(item);
        }

        private static void RunDynamicDaoExample()
        {
            dynamic customerDao = new DynamicDao<Customer>(DataStore.GetCustomerQueryProvider());
            IEnumerable<Customer> customers = customerDao.FindByFirstName("Bob");

            foreach (var item in customers)
                Console.WriteLine(item);

            customers = customerDao.FindByLastName("Jones");

            foreach (var item in customers)
                Console.WriteLine(item);
        }

        private static void RunGeneratedDaoExample()
        {
            dynamic customerDao = new GeneratedDao<Customer>(DataStore.GetCustomerQueryProvider());

            IEnumerable<Customer> customers = customerDao.FindByFirstName("Bob");
            foreach (var item in customers)
                Console.WriteLine(item);

            customers = customerDao.FindByLastName("Jones");
            foreach (var item in customers)
                Console.WriteLine(item);
        }
    }
}
