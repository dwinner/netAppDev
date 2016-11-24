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
    class LambdaExamples
    {
        public static void LambdaCSharpExample()
        {
            Func<int, int, int> add = (x, y) => { return x + y; };
            int result = add(3, 5);
            Console.WriteLine("result is {0}", result);
        }

        public static void LambdaLinqExample()
        {
            ParameterExpression x = Expression.Parameter(typeof(int), "x");
            ParameterExpression y = Expression.Parameter(typeof(int), "y");

            Expression<Func<int, int, int>> add = Expression
                .Lambda<Func<int, int, int>>(Expression.Add(x, y), x, y);

            int result = add.Compile()(3, 5);
            Console.WriteLine("result is {0}", result);
        }

        public static void ClosureLexicalScopeCSharpExample()
        {
            int x = 2;
            int y = 1;

            //The 'add' delegate and variables x, y form a closure.
            Func<int> add = () => { return x + y; };

            int result;
            {
                //int y = 3; //C# compiler does not allow this.
                result = add();
            }

            Console.WriteLine("result is {0}", result);
        }

        public static void ClosureLexicalScopeLinqExample()
        {
            ParameterExpression x = Expression.Variable(typeof(int), "x");
            ParameterExpression y = Expression.Variable(typeof(int), "y");
            ParameterExpression add = Expression.Variable(typeof(Func<int>), "add");

            Expression addExpression = Expression.Block(
                    new ParameterExpression[] { x, add, y },
                    Expression.Assign(x, Expression.Constant(2)),
                    Expression.Assign(y, Expression.Constant(1)),
                    Expression.Assign(add, Expression.Lambda<Func<int>>(
                            Expression.Add(x, y))), //x, y here are bound to outer scope's x, y
                    Expression.Block(
                        new ParameterExpression[] { y },
                        Expression.Assign(y, Expression.Constant(3)),
                        Expression.Invoke(add) //invoke add in the inner scope.
                    )
                );


            int result = Expression.Lambda<Func<int>>(addExpression).Compile()();
            Console.WriteLine("result is {0}", result);
        }

    }
}
