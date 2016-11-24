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
using AopAlliance.Aop;
using AopAlliance.Intercept;
using System.Reflection;

namespace Aop
{
    public class LoggingAdvice : IDynamicAdvice, IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            BeforeInvoke(invocation.Method, invocation.Arguments, invocation.Target);
            object returnValue = invocation.Proceed();
            AfterInvoke(returnValue, invocation.Method, invocation.Arguments, invocation.Target);
            return returnValue; 
        }
    
        #region IDynamicAdvice Members

        public void BeforeInvoke(MethodInfo method, object[] args, object target)
        {
            Console.Out.WriteLine("Advice BeforeInvoke is called. Intercepted method is {0}.", method.Name);
        }

        public void AfterInvoke(object returnValue, MethodInfo method, object[] args, object target)
        {
            Console.Out.WriteLine("Advice AfterInvoke is called. Intercepted method is {0}.", method.Name);
        }

        #endregion
    }
}
