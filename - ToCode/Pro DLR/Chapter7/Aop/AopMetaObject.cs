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
using System.Linq.Expressions;
using System.Text;
using System.Dynamic;
using System.Reflection;
using System.Collections;
using AopAlliance.Aop;

namespace Aop
{
    public class AopMetaObject : DynamicMetaObject
    {
        public AopMetaObject(Expression expression, object obj)
            : base(expression, BindingRestrictions.Empty, obj)
        { }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            return WeaveAspect(binder.Name, base.BindGetMember(binder));
        }

        private DynamicMetaObject WeaveAspect(String name, DynamicMetaObject originalObject)
        {
            Expression originalExpression = originalObject.Expression;
            Type targetType = this.Value.GetType();
            PropertyInfo property = targetType.GetProperty(name);
            MethodInfo method = property.GetGetMethod();
            
            Expression nullExp = Expression.Constant(null);
            Expression targetExp = Expression.Constant(this.Value);
            Expression arrayExp = Expression.Constant(new Object[] { });
            Expression methodExp = Expression.Constant(method);

            IList<IAdvice> adviceList = AdvisorChainFactory.GetInterceptors(method, targetType);

            List<Expression> calls = new List<Expression>();
            List<Expression> afterCalls = new List<Expression>();
            foreach (var advice in adviceList)
            {
                if (!(advice is IDynamicAdvice))
                    continue;

                calls.Add(Expression.Call(
                            Expression.Constant(advice),
                            typeof(IDynamicAdvice).GetMethod("BeforeInvoke"),
                            new Expression[] { methodExp, arrayExp, targetExp }));

                afterCalls.Add(Expression.Call(
                            Expression.Constant(advice),
                            typeof(IDynamicAdvice).GetMethod("AfterInvoke"),
                            new Expression[] { nullExp, methodExp, arrayExp, targetExp }));
            }

            ParameterExpression returnValue = Expression.Parameter(originalExpression.Type);
            calls.Add(Expression.Assign(returnValue, originalExpression));
            afterCalls.Reverse();
            calls.AddRange(afterCalls);
            calls.Add(returnValue);

            var advisedObject = new DynamicMetaObject(
                Expression.Block(
                    new[] { returnValue },
                    calls.ToArray()
                ),
                originalObject.Restrictions
            );

            return advisedObject;
        }
    }
}
