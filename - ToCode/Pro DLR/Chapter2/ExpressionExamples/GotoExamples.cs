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
    class GotoExamples
    {
        public static void CSharpExample()
        {
            //C# cannot do this jump.
            //goto InnerBlock;

            {
            InnerBlock:
                Console.WriteLine("In inner block.");
                //jump to outer block
                goto OuterBlock;
                Console.WriteLine("This line is unreachable.");
            }

        OuterBlock:
            Console.WriteLine("In outer block.");
        }

        public static void LinqExample()
        {
            LabelTarget innerBlock = Expression.Label();
            LabelTarget outerBlock = Expression.Label();

            Expression<Action> lambda = Expression.Lambda<Action>(
                Expression.Block(
                //DLR can do this jump
                    Expression.Goto(innerBlock),
                    ExpressionHelper.Print("Unreachable"),
                    Expression.Block(Expression.Label(innerBlock),
                        ExpressionHelper.Print("In inner block."),
                        Expression.Goto(outerBlock),
                        ExpressionHelper.Print("Unreachable")
                    ),
                    Expression.Label(outerBlock),
                    ExpressionHelper.Print("In outer block.")));

            lambda.Compile()();
        }

    }
}
