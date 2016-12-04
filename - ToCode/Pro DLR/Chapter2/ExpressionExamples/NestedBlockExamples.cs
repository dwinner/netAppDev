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
    public class NestedBlockExamples
    {
        public static void BlockLexicalScopeCSharpExample()
        {
            Func<int> add = () =>
            {
                int x = 2;
                {
                    //int x = 1; //Compilation error. C# compiler does not allow this.
                    int y = 3;
                    return x + y;
                }
            };

            int result = add();
            Console.WriteLine("result is {0}", result);
        }

        public static void BlockLexicalScopeLinqExample()
        {
            ParameterExpression x = Expression.Variable(typeof(int), "x");
            ParameterExpression y = Expression.Variable(typeof(int), "y");

            Expression add = Expression.Block(
                    new ParameterExpression[] { x },
                    Expression.Assign(x, Expression.Constant(2)),
                    Expression.Block(
                        //If we replace the next line of code with new ParameterExpression[] {y}, 
                        //result will be 5.

                        //Unlike C#, DLR Expression allows the declaration of variable x in the inner block.
                        new ParameterExpression[] { x, y }, 
                        Expression.Assign(y, Expression.Constant(3)),
                        Expression.Add(x, y)
                    )
                );
            
            
            int result = Expression.Lambda<Func<int>>(add).Compile()();
            Console.WriteLine("result is {0}", result);
        }

        
        public static void OuterScopeVariableNotChangedByInnerScopeLinqExample()
        {
            ParameterExpression x = Expression.Variable(typeof(int), "x");
            
            Expression block = Expression.Block(
                    new ParameterExpression[] { x },
                    Expression.Assign(x, Expression.Constant(2)),
                    Expression.Block(
                        new ParameterExpression[] { x }, 
                        Expression.Assign(x, Expression.Constant(1))
                    ),
                    ExpressionHelper.Print(x)
                );

            Expression.Lambda<Action>(block).Compile()();
        }
    }
}
