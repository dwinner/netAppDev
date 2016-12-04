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
using System.Reflection;

namespace QueryProviderExamples
{
    public class QueryProvider<T> : IQueryProvider
    {
        private IList<T> records;

        public QueryProvider(IList<T> records)
        {
            this.records = records;
        }

        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            if (expression == null)
                return new Query<T>(this);
            else
                return new Query<T>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)this.Execute(expression);
        }

        public object Execute(Expression expression)
        {
            if (!(expression is MethodCallExpression))
                throw new NotSupportedException("The expression needs to be a MethodCallExpression");

            MethodCallExpression methodCallExpression = (MethodCallExpression) expression;
            if (methodCallExpression.Method.DeclaringType == typeof(Queryable)
                && methodCallExpression.Method.Name == "Where")
            {
                QueryExpressionVisitor<T> visitor = new QueryExpressionVisitor<T>();
                visitor.Visit(methodCallExpression);
                return records.Where<T>(visitor.Predicate);
            }
            else
                throw new NotSupportedException("The expression needs to be a call to the Where method of Queryable");
        }
    }
}
