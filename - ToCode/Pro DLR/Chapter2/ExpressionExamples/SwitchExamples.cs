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
    class SwitchExamples
    {
        public static void CSharpExample()
        {
            switch (1)
            {
                case 1:
                    Console.WriteLine("case 1");
                    break;
                case 2:
                case 3:
                    Console.WriteLine("case 2 and 3");
                    break;
            }
        }

        public static void LinqExample()
        {
            SwitchExpression switchExpression = Expression.Switch(
                Expression.Constant(1),
                new SwitchCase[] {
                    Expression.SwitchCase(
                        ExpressionHelper.Print("case 1"),
                        Expression.Constant(1)
                    ),
                    Expression.SwitchCase(
                        ExpressionHelper.Print("case 2 and 3"),
                        Expression.Constant(2),
                        Expression.Constant(3)
                    )
                }
            );

            Action switchDelegate = Expression.Lambda<Action>(switchExpression).Compile();
            switchDelegate();
        }
    }
}
