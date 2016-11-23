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
    class LexicalScopeExamples
    {
        static void addToY(int x)
        {
            Console.WriteLine(x + y); //refers to the outer y
        }

        static void add4(int x)
        {
            //You can uncomment the first line below and comment out the second line below to
            //see the effects of lexical scoping.
            //int y = 4;  //This y is a local y, not the outer y. 
            y = 4; //This y refers to the outer y.
            addToY(x);
        }

        static int y = 2; //outer y

        public static void RunLexicalScopeExamples()
        {
            addToY(5);
            add4(5);
        }
    }
}
