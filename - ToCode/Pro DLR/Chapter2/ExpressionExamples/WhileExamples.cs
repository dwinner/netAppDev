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
    class WhileExamples
    {
        public static void CSharpExample()
        {
            int i = 0;
            while (i < 3)
                i++;

            Console.WriteLine("i is {0}", i);
        }

        public static void CSharpGotoExample()
        {
            int i = 0;

        WhileLabel:

            if (i < 3)
            {
                i++;
                goto WhileLabel;
            }

            Console.WriteLine("i is {0}", i);
        }

        public static void LinqExample()
        {
            LabelTarget whileLabel = Expression.Label();
            ParameterExpression i = Expression.Variable(typeof(int), "i");

            Expression<Func<int>> lambda = Expression.Lambda<Func<int>>(
                Expression.Block(
                    new ParameterExpression[] { i },
                    Expression.Label(whileLabel),
                    Expression.IfThen(Expression.LessThan(i, Expression.Constant(3)),
                    Expression.Block(
                        Expression.PostIncrementAssign(i),
                        Expression.Goto(whileLabel))),
                    i));

            int result = lambda.Compile()();
            Console.WriteLine("i is {0}", result);
        }
    }
}
