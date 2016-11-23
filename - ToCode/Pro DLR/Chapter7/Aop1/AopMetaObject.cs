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

namespace Aop1
{
    public class AopMetaObject : DynamicMetaObject
    {
        //For simplicity, which advice we use is not dependent on configuration. 
        private SimpleLoggingAdvice advice = new SimpleLoggingAdvice(); 

        public AopMetaObject(Expression expression, object obj)
            : base(expression, BindingRestrictions.Empty, obj)
        { }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            return WeaveAspect(base.BindGetMember(binder));
        }

        //public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
        //{
        //    Console.WriteLine("Setting member {0}", binder.Name);
        //    return base.BindSetMember(binder, value);
        //}

        //public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
        //{
        //    Console.WriteLine("Invoking member {0}", binder.Name);
        //    return base.BindInvokeMember(binder, args);
        //}

        private DynamicMetaObject WeaveAspect(DynamicMetaObject originalObject)
        {
            Expression originalExpression = originalObject.Expression;
            var args = new Expression[0] {};
            ParameterExpression returnValue = Expression.Parameter(originalExpression.Type);
            
            var advisedObject = new DynamicMetaObject(
                Expression.Block(
                    new[] { returnValue },
                    new Expression[] { 
                        Expression.Call(
                            Expression.Constant(this.advice),
                            typeof(SimpleLoggingAdvice).GetMethod("BeforeInvoke"),
                            args),
                        
                        Expression.Assign(returnValue, originalExpression),

                        Expression.Call(
                            Expression.Constant(this.advice),
                            typeof(SimpleLoggingAdvice).GetMethod("AfterInvoke"),
                            args),

                        returnValue
                    }
                ),
                originalObject.Restrictions
            );

            return advisedObject;
        }
    }
}
