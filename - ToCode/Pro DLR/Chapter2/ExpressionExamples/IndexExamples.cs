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
using System.Linq.Expressions;

namespace ExpressionExamples
{
    class IndexExamples
    {
        public static void CSharpExample()
        {
            int[] numbers = { 7, 2, 4 };
            numbers[1] = 6;
            Console.WriteLine("{0}, {1}, {2}", numbers[0], numbers[1], numbers[2]);
        }

        public static void LinqExample()
        {
            int[] numbers = { 7, 2, 4 };
            
            IndexExpression indexExpression = Expression.ArrayAccess(
                Expression.Constant(numbers), 
                Expression.Constant(1));
             
            Expression arrayIndexExp = Expression.Assign(indexExpression, Expression.Constant(6));

            Action arrayIndexDelegate = Expression.Lambda<Action>(arrayIndexExp).Compile();
            arrayIndexDelegate();
            Console.WriteLine("{0}, {1}, {2}", numbers[0], numbers[1], numbers[2]);
        }
    }
}
