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
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryProviderExamples
{
    public class DynamicDao<T> : DynamicObject
    {
        private IQueryProvider provider;

        public DynamicDao(IQueryProvider provider)
        {
            this.provider = provider;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, 
            out object result)
        {
            Func<Customer, bool> y = (Customer x) => x.FirstName.Equals("");

            String propertyName = binder.Name.Substring(6); //6 is the length of 'FindBy'
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(
                Expression.Call(
                    Expression.MakeMemberAccess(
                        parameter, 
                        propertyInfo),
                    propertyInfo.PropertyType.GetMethod("Equals", new Type[] {typeof(object)}),
                    Expression.Constant(args[0])),
                parameter);

            Query<T> query = new Query<T>(provider);
            result = query.Where<T>(predicate);
            return true;
        }
    }
}
