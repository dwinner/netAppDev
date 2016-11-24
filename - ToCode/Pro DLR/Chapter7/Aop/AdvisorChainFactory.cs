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
using Spring.Context;
using Spring.Context.Support;
using System.Collections;
using System.Reflection;
using Spring.Aop;
using Spring.Aop.Support;
using Spring.Core;
using AopAlliance.Aop;

namespace Aop
{
    class AdvisorChainFactory
    {
        private static IApplicationContext context;

        public static IApplicationContext Context 
        {
            set { context = value; }
        }
        
    public static IList<IAdvice> GetInterceptors(MethodInfo method, Type targetType)
    {
        IList<IAdvice> adviceList = new List<IAdvice>();
        IDictionary advisors = context.GetObjectsOfType(typeof(IPointcutAdvisor));

        ArrayList advisorList = new ArrayList(advisors.Values);
        advisorList.Sort(new OrderComparator());

        foreach (IPointcutAdvisor advisor in advisorList)
        {
            if (advisor.Pointcut.MethodMatcher.Matches(method, targetType))
                adviceList.Add(advisor.Advice);
        }

        return adviceList;
    }
    }
}
