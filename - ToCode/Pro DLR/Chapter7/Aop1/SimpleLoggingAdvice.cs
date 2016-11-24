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

namespace Aop1
{
    public class SimpleLoggingAdvice
    {
        //For simplicity, this method does not have any input parameter.
        public void BeforeInvoke()
        {
            Console.Out.WriteLine("Advice BeforeInvoke is called.");
        }

        //For simplicity, this method does not have any input parameter.
        public void AfterInvoke()
        {
            Console.Out.WriteLine("Advice AfterInvoke is called.");
        }
    }
}
