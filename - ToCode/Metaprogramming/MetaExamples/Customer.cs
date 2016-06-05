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
using System.Dynamic;
using MetaLib;

namespace MetaExamples
{
    public class Customer : ClassMetaObject
    {
        private static ExpandoClass _class = new ExpandoClass();

        public static dynamic CLASS
        {
            get { return _class; }
        }

        protected override ExpandoClass Class
        {
            get { return _class; }
        }

        private string name;
        private int age;

        public Customer(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
