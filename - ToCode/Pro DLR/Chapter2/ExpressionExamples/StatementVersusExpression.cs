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
using System.Reflection;
using System.Linq.Expressions;

namespace ExpressionExamples
{
    class StatementVersusExpression
    {
        public static void AssignIfThenElseExpressionToVariableCSharpExample()
        {
            String x = "Hello";
            //String y = if (true) x.ToLower();  //This causes compilation error.
        }

        public static void AssignIfThenElseExpressionToVariableLinqExample()
        {
            MethodInfo method = typeof(String).GetMethod("ToLower", new Type[] { });
            ParameterExpression x = Expression.Variable(typeof(String), "x");
            ParameterExpression y = Expression.Variable(typeof(String), "y");
            Expression blockExpression = Expression.Block(
                new ParameterExpression[] { x, y },
                Expression.Assign(x, Expression.Constant("Hello")),
                Expression.Assign(y,
                    Expression.Condition(Expression.Constant(true),
                        Expression.Call(x, method),
                        Expression.Default(typeof(String)),
                        typeof(String)))
            );

            Func<String> blockDelegate = Expression.Lambda<Func<String>>(blockExpression).Compile();
            String result = blockDelegate();
            Console.WriteLine(result);
        }
    }
}
