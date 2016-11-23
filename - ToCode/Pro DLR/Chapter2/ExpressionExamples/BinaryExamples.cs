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
    class BinaryExamples
    {
        public static void CSharpExample()
        {
            double result = (10d + 20d) / 3d;
            Console.WriteLine(result);
        }

        public static void LinqExample()
        {
            Expression binaryExpression = Expression.Divide(
                Expression.Add(Expression.Constant(10d),
                               Expression.Constant(20d)),
                Expression.Constant(3d)
            );

            Func<double> binaryDelegate = Expression.Lambda<Func<double>>(binaryExpression).Compile();
            Console.WriteLine(binaryDelegate());
        }
    }
}
