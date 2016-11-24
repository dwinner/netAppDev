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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace QueryProviderExamples
{
    internal class QueryExpressionVisitor<T> : ExpressionVisitor
    {
        public Func<T, bool> Predicate;
        
        internal QueryExpressionVisitor()
        {  }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
            {
                //The second argument of the method call expression is a lambda expression that serves 
                //as the predicate for the 'Where' clause.
                LambdaExpression lambda = (LambdaExpression)GetPastQuotes(m.Arguments[1]);
                Predicate = (Func<T, bool>) lambda.Compile();
            }

            return base.VisitMethodCall(m);
        }

        private Expression GetPastQuotes(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Quote)
                expression = ((UnaryExpression)expression).Operand;

            return expression;
        }
    }
}
