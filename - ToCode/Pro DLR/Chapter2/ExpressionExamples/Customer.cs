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

namespace ExpressionExamples
{
    class Customer
    {
        int age = 10;

        public int getAge()
        {
            return age;
        }

        public int getAge2()
        {
            //This local variable age has no effect on the getAge() method.
            //The getAge() method will always refer to the age field of 
            //the Customer class.
            int age = 15;
            return getAge();
        }
    }
}
