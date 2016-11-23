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
using System.Runtime.CompilerServices;
using System.Linq.Expressions;

namespace ExpressionExamples
{
    class DynamicExamples
    {
        public static void CSharpExample()
        {
            dynamic x = 5;
            dynamic y = x + 2;
            Console.WriteLine("5 + 2 = {0}", y);
        }

        public static void CSharpSimpleBinderExample()
        {
            CallSiteBinder binder = new SimpleOperationBinder();

            CallSite<Func<CallSite, object, object, object>> site =
              CallSite<Func<CallSite, object, object, object>>.Create(binder);

            //This will invoke the binder to do the binding.
            object sum = site.Target(site, 5, 2);
            Console.WriteLine("Sum is {0}", sum);
        }

        public static void LinqExample()
        {
            //prepare a call site binder
            CallSiteBinder binder = new SimpleOperationBinder();

            DynamicExpression dynamicExpression = Expression.Dynamic(
                        binder, typeof(object), new[] { Expression.Constant(5), Expression.Constant(2) });

            Func<object> compiledDelegate = Expression.Lambda<Func<object>>(dynamicExpression).Compile();
            object result = compiledDelegate();
            Console.WriteLine("result is {0}", result);
        }
    }
}
