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

namespace Aop
{
    public class Employee : IEmployee
    {
        private int age;
        private String name;

        public int Age 
        { 
            get 
            { 
                Console.WriteLine("Employee Age getter is called."); 
                return age; 
            }

            set { age = value; }
        }

        public String Name
        {
            get
            {
                Console.WriteLine("Employee Name getter is called.");
                return name;
            }

            set { name = value; }
        }
    }
}
